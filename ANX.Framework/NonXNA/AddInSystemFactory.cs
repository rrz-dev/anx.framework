using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.NonXNA.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class AddInSystemFactory
	{
		#region Constants
		internal static readonly Type[] ValidAddInCreators =
			{
				typeof(IInputSystemCreator),
				typeof(IRenderSystemCreator),
				typeof(ISoundSystemCreator),
				typeof(IMediaSystemCreator),
				typeof(IPlatformSystemCreator),
			};
		#endregion

		#region Private
		private Dictionary<String, ICreator> creators;
		private static AddInSystemFactory instance;
		private bool initialized;
		private Dictionary<Type, ICreator> defaultCreators;
		private Dictionary<AddInType, AddInTypeCollection> addinSystems;
		#endregion

		#region Public
		public static AddInSystemFactory Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new AddInSystemFactory();
					Logger.Info("Created AddInSystemFactory instance");
				}

				return instance;
			}
		}

		internal static IPlatformSystemCreator DefaultPlatformCreator
		{
			get
			{
				return Instance.GetDefaultCreator<IPlatformSystemCreator>();
			}
		}
		#endregion

		#region Constructor
		private AddInSystemFactory()
		{
			defaultCreators = new Dictionary<Type, ICreator>();
			creators = new Dictionary<string, ICreator>();
			addinSystems = new Dictionary<AddInType, AddInTypeCollection>();

			addinSystems.Add(AddInType.InputSystem, new AddInTypeCollection());
			addinSystems.Add(AddInType.MediaSystem, new AddInTypeCollection());
			addinSystems.Add(AddInType.RenderSystem, new AddInTypeCollection());
			addinSystems.Add(AddInType.SoundSystem, new AddInTypeCollection());
			addinSystems.Add(AddInType.PlatformSystem, new AddInTypeCollection());

			Logger.Info("Operating System: {0} ({1})", OSInformation.GetVersionString(),
				OSInformation.GetVersion().ToString());
		}
		#endregion

		#region Initialize
		public void Initialize()
		{
			if (initialized == false)
			{
				initialized = true;

				Logger.Info("[ANX] Initializing ANX.Framework AddInSystemFactory...");

				CreateAllAddIns();
				SortAddIns();
			}
		}
		#endregion

		#region CreateAllAddIns
		private void CreateAllAddIns()
		{
			Assembly[] allAssemblies = AssemblyLoader.GetAllAssemblies();

			foreach (Assembly assembly in allAssemblies)
			{
				AddIn addin = new AddIn(assembly);
				if (addin.IsValid && addin.IsSupported)
				{
					addinSystems[addin.Type].AvailableSystems.Add(addin);
					Logger.Info("[ANX] successfully loaded addin...");
				}
				else
				{
					Logger.Info("[ANX] skipped loading file because it is not supported or not a valid AddIn");
				}
			}
		}
		#endregion

		#region AddCreator
		public void AddCreator(ICreator creator)
		{
			string creatorName = creator.Name.ToLowerInvariant();

			if (creators.ContainsKey(creatorName))
			{
				throw new Exception("Duplicate creator found. A creator with the name '" +
					creator.Name + "' was already registered.");
			}

			creators.Add(creatorName, creator);

			Logger.Info("added creator '{0}'. Total count of registered creators is now {1}.", creatorName, creators.Count);
		}
		#endregion

		#region HasFramework
		public bool HasFramework(String name)
		{
			return creators.ContainsKey(name.ToLowerInvariant());
		}
		#endregion

		#region GetCreator
		public T GetCreator<T>(String name) where T : class, ICreator
		{
			Initialize();

			ICreator creator = null;
			creators.TryGetValue(name.ToLowerInvariant(), out creator);
			return creator as T;
		}
		#endregion

		#region GetCreators
		public IEnumerable<T> GetCreators<T>() where T : class, ICreator
		{
			Initialize();

			foreach (ICreator creator in creators.Values)
			{
				Type[] interfaces = TypeHelper.GetInterfacesFrom(creator.GetType());
                foreach (Type t in interfaces)
                {
                    if (t.Name.Equals( typeof(T).Name ))
                    {
                        yield return creator as T;
                    }
                }
			}
		}
		#endregion

		#region GetDefaultCreator
		public T GetDefaultCreator<T>() where T : class, ICreator
		{
			Initialize();

			AddInType addInType = GetAddInType(typeof(T));

			AddInTypeCollection info = addinSystems[addInType];
			if (String.IsNullOrEmpty(info.PreferredName))
			{
				if (info.AvailableSystems.Count > 0)
				{
					return info.AvailableSystems[0].Instance as T;
				}

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} because there are no " +
					"registered {0}s available! Make sure you referenced a {0} library " +
					"in your project or one is laying in your output folder!", addInType));
			}
			else
			{
				foreach (AddIn addin in info.AvailableSystems)
				{
					if (addin.Name.Equals(info.PreferredName,
						StringComparison.CurrentCultureIgnoreCase))
					{
						return addin.Instance as T;
					}
				}

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} '{1}' because it was not found in the " +
					"list of registered creators!", addInType, info.PreferredName));
			}

			throw new AddInLoadingException(String.Format(
				"Couldn't find a DefaultCreator of type '{0}'!", typeof(T).FullName));
		}
		#endregion

		#region SortAddIns
		public void SortAddIns()
		{
			foreach (AddInTypeCollection info in addinSystems.Values)
			{
				info.AvailableSystems.Sort();
			}

			creators = creators.OrderBy(x => x.Value.Priority).ToDictionary(
				x => x.Key, x => x.Value);
		}
		#endregion

		#region GetPreferredSystem
		public string GetPreferredSystem(AddInType addInType)
		{
			return addinSystems[addInType].PreferredName;
		}
		#endregion

		#region SetPreferredSystem
		public void SetPreferredSystem(AddInType addInType, string preferredName)
		{
			if (addinSystems[addInType].PreferredLocked)
			{
				throw new AddInLoadingException(String.Format(
					"Can't set preferred {0} because a {0} is alread in use.", addInType));
			}

			addinSystems[addInType].PreferredName = preferredName;
		}
		#endregion

		#region PreventSystemChange
		public void PreventSystemChange(AddInType addInType)
		{
			addinSystems[addInType].PreferredLocked = true;
		}
		#endregion

		#region GetAddInType
		internal static AddInType GetAddInType(Type t)
		{
			foreach (Type creatorType in ValidAddInCreators)
			{
				if (TypeHelper.IsTypeAssignableFrom(creatorType, t))
				{
					return (AddInType)Enum.Parse(typeof(AddInType),
						creatorType.Name.Substring(1, creatorType.Name.Length - 8));
				}
			}

			return AddInType.Unknown;
		}
		#endregion
	}
}
