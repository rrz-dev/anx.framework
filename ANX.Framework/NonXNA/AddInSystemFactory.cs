#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using System.Runtime.InteropServices;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.NonXNA
{
    public class AddInSystemFactory
    {
        #region Private Members
        private Dictionary<String, ICreator> creators;
        private static AddInSystemFactory instance;
        private bool initialized;
        private Dictionary<Type, ICreator> defaultCreators = new Dictionary<Type, ICreator>();
#if !WIN8
        private OperatingSystem operatingSystem;
#else
        private String operatingSystem;
#endif
        private Version operatingSystemVersion;
		private Dictionary<AddInType, AddInSystemInfo> addinSystems;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion // Private Members

		#region Public
		public static AddInSystemFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddInSystemFactory();
                    logger.Debug("Created AddInSystemFactory instance");
                }

                return instance;
            }
	    }

#if !WIN8
		public OperatingSystem OperatingSystem
		{
			get
			{
				return this.operatingSystem;
			}
		}
#else
		public String OperatingSystem
		{
			get
			{
				return this.operatingSystem;
			}
		}
#endif
        public Version OperatingSystemVersion
		{
			get
			{
				return this.operatingSystemVersion;
			}
		}

        #endregion

		#region Constructor
		private AddInSystemFactory()
        {
		    addinSystems = new Dictionary<AddInType, AddInSystemInfo>();
		    addinSystems.Add(AddInType.InputSystem, new AddInSystemInfo());
		    addinSystems.Add(AddInType.MediaSystem, new AddInSystemInfo());
		    addinSystems.Add(AddInType.RenderSystem, new AddInSystemInfo());
		    addinSystems.Add(AddInType.SoundSystem, new AddInSystemInfo());
					
            this.creators = new Dictionary<string, ICreator>();

#if !WIN8
            this.operatingSystem = Environment.OSVersion;
            this.operatingSystemVersion = this.operatingSystem.Version;
            logger.Info("Operating System: {0} ({1})", operatingSystem.VersionString, operatingSystemVersion.ToString());
#else
            this.operatingSystem = "Win8";
            this.operatingSystemVersion = new Version(RuntimeEnvironment.GetSystemVersion());
            logger.Info("Operating System: {0} ({1})", "Win8", operatingSystemVersion.ToString());
#endif
        }
		#endregion

		#region Initialize
		public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                logger.Info("[ANX] Initializing ANX.Framework AddInSystemFactory...");

                String executingAssembly = Assembly.GetExecutingAssembly().Location;

                foreach (String file in Directory.EnumerateFiles(Path.GetDirectoryName(executingAssembly), "*.dll", SearchOption.TopDirectoryOnly))
                {
                    if (file.Equals(executingAssembly) == false)
                    {
                        logger.Info("[ANX] trying to load '{0}'...", file);

                        AddIn addin = new AddIn(file);
                        if (addin.IsValid && addin.IsSupported)
                        {
														addinSystems[addin.Type].AvailableSystems.Add(addin);
                            logger.Info("[ANX] successfully loaded addin...");
                        }
                        else
                        {
                            logger.Info("[ANX] skipped loading file because it is not supported or not a valid AddIn");
                        }
                    }
                }

                SortAddIns();
            }
        }
		#endregion

		#region AddCreator
		public void AddCreator(ICreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();

            if (creators.ContainsKey(creatorName))
            {
                throw new Exception("Duplicate creator found. A creator with the name '" + creator.Name + "' was already registered.");
            }

            creators.Add(creatorName, creator);

            logger.Debug("added creator '{0}'. Total count of registered creators is now {1}.", creatorName, creators.Count);
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
            if (!initialized)
            {
                Initialize();
            }

            ICreator creator = null;
            creators.TryGetValue(name.ToLowerInvariant(), out creator);
            return creator as T;
	    }
	    #endregion

	    #region GetCreators
	    public IEnumerable<T> GetCreators<T>() where T : class, ICreator
        {
            if (!initialized)
            {
                Initialize();
            }

            Type t = typeof(T);

            foreach (ICreator creator in this.creators.Values.Where(p =>
#if WIN8
                        p.GetType().GetTypeInfo().ImplementedInterfaces.First<Type>().Equals(t)))
#else
                        p.GetType().GetInterfaces()[0].Equals(t)))
#endif
            {
                yield return creator as T;
            }
        }
		#endregion

		#region GetDefaultCreator
		public T GetDefaultCreator<T>() where T : class, ICreator
        {
            if (initialized == false)
            {
                Initialize();
            }

            Type type = typeof(T);
            AddInType addInType = GetAddInType(type);

			AddInSystemInfo info = addinSystems[addInType];
			if (String.IsNullOrEmpty(info.PreferredName))
			{
				if (info.AvailableSystems.Count > 0)
				{
					return info.AvailableSystems[0].Instance as T;
				}

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} because there are no " +
					"registered {0}s available!", addInType));
			}
			else
			{
				foreach (AddIn addin in info.AvailableSystems)
				{
					if (addin.Name.Equals(info.PreferredName, StringComparison.CurrentCultureIgnoreCase))
					{
						return addin.Instance as T;
					}
				}

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} '{1}' because it was not found in the " +
					"list of registered creators!", addInType, info.PreferredName));
			}

            throw new AddInLoadingException(String.Format(
							"Couldn't find a DefaultCreator of type '{0}'!", type.FullName));
        }
		#endregion

		#region SortAddIns
		public void SortAddIns()
        {
			foreach (AddInSystemInfo info in addinSystems.Values)
			{
				info.AvailableSystems.Sort();
			}

            this.creators = this.creators.OrderBy(x => x.Value.Priority).ToDictionary(x => x.Key, x => x.Value);
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
            if (IsAssignable(typeof(IRenderSystemCreator), t))
            {
                return AddInType.RenderSystem;
            }
            else if (IsAssignable(typeof(IInputSystemCreator), t))
            {
                return AddInType.InputSystem;
            }
            else if (IsAssignable(typeof(ISoundSystemCreator), t))
            {
                return AddInType.SoundSystem;
			}
			else if (IsAssignable(typeof(IMediaSystemCreator), t))
			{
				return AddInType.MediaSystem;
			}
            else
            {
                return AddInType.Unknown;
            }
		}
		#endregion

        private static bool IsAssignable(System.Type lt, System.Type rt)
        {
#if WIN8
            return lt.GetTypeInfo().IsAssignableFrom(rt.GetTypeInfo());
#else
            return lt.IsAssignableFrom(rt);
#endif
        }
	}
}
