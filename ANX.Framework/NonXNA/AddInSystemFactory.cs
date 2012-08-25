using System;
using System.Collections.Generic;
using System.Linq;
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
		private Dictionary<string, ICreator> creators;
		private static AddInSystemFactory instance;
		private bool initialized;
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
			creators = new Dictionary<string, ICreator>();
			addinSystems = new Dictionary<AddInType, AddInTypeCollection>();

			foreach (AddInType type in Enum.GetValues(typeof(AddInType)))
				addinSystems.Add(type, new AddInTypeCollection());

			Logger.Info("Operating System: {0} ({1})", OSInformation.GetVersionString(), OSInformation.GetVersion().ToString());
		}
		#endregion

		#region Initialize
		public void Initialize()
		{
			if (initialized)
				return;

			initialized = true;
			Logger.Info("[ANX] Initializing ANX.Framework AddInSystemFactory...");
			CreateAllAddIns();
			SortAddIns();
		}
		#endregion

		#region CreateAllAddIns
		private void CreateAllAddIns()
		{
			foreach (Type creatorType in AssemblyLoader.CreatorTypes)
			{
				Type matchingSupportedPlatformsType = FindSupportedPlatformsTypeByNamespace(creatorType);
				if (matchingSupportedPlatformsType == null)
					matchingSupportedPlatformsType = FindSupportedPlatformsTypeByAssembly(creatorType);

				AddIn addin = new AddIn(creatorType, matchingSupportedPlatformsType);
				if (addin.IsValid && addin.IsSupported)
				{
				    addinSystems[addin.Type].Add(addin);
				    Logger.Info("[ANX] successfully loaded addin...");
				}
				else
				    Logger.Info("[ANX] skipped loading file because it is not supported or not a valid AddIn");
			}
		}
		#endregion

		#region FindSupportedPlatformsTypeByNamespace
		private Type FindSupportedPlatformsTypeByNamespace(Type creatorType)
		{
			foreach (Type spType in AssemblyLoader.SupportedPlatformsTypes)
				if (spType.Namespace == creatorType.Namespace)
					return spType;
			
			return null;
		}
		#endregion

		#region FindSupportedPlatformsTypeByAssembly
		private Type FindSupportedPlatformsTypeByAssembly(Type creatorType)
		{
			foreach (Type spType in AssemblyLoader.SupportedPlatformsTypes)
				if (spType.Assembly == creatorType.Assembly)
					return spType;

			return null;
		}
		#endregion

		#region AddCreator
		internal void AddCreator(ICreator creator)
		{
			string creatorName = creator.Name.ToLowerInvariant();

			if (creators.ContainsKey(creatorName))
				throw new Exception("Duplicate creator found. A creator with the name '" + creator.Name +
					"' was already registered.");

			creators.Add(creatorName, creator);

			Logger.Info("added creator '{0}'. Total count of registered creators is now {1}.", creatorName, creators.Count);
		}
		#endregion

		#region HasFramework
		public bool HasFramework(string name)
		{
			return creators.ContainsKey(name.ToLowerInvariant());
		}
		#endregion

		#region GetCreator
		public T GetCreator<T>(string name) where T : class, ICreator
		{
			Initialize();

			if (creators.ContainsKey(name.ToLowerInvariant()))
				return creators[name.ToLowerInvariant()] as T;

			return null;
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
					if (t.Name.Equals(typeof(T).Name))
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
			return addinSystems[addInType].GetDefaultCreator<T>(addInType);
		}
		#endregion

		#region SortAddIns
		public void SortAddIns()
		{
			foreach (AddInTypeCollection info in addinSystems.Values)
				info.Sort();

			creators = creators.OrderBy(x => x.Value.Priority).ToDictionary(x => x.Key, x => x.Value);
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
				throw new AddInLoadingException(String.Format("Can't set preferred {0} because a {0} is alread in use.", addInType));

			addinSystems[addInType].PreferredName = preferredName;
		}
		#endregion

		#region PreventSystemChange
		public void PreventSystemChange(AddInType addInType)
		{
			addinSystems[addInType].Lock();
		}
		#endregion

		#region GetAddInType
		internal static AddInType GetAddInType(Type t)
		{
			foreach (Type creatorType in ValidAddInCreators)
			{
				if (TypeHelper.IsTypeAssignableFrom(creatorType, t))
				{
					string addInTypeName = creatorType.Name.Substring(1, creatorType.Name.Length - 8);
					return (AddInType)Enum.Parse(typeof(AddInType), addInTypeName);
				}
			}

			return AddInType.Unknown;
		}
		#endregion
	}
}
