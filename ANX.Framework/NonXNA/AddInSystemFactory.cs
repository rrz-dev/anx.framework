#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.Input;
using NLog;
using System.Collections;
using System.Resources;

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
        private Dictionary<String, ICreator> creators;
        private static AddInSystemFactory instance;
        private bool initialized;
        private Dictionary<Type, ICreator> defaultCreators = new Dictionary<Type, ICreator>();
        private OperatingSystem operatingSystem;
        private Version operatingSystemVersion;

        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        private AddInSystemFactory()
        {
            this.creators = new Dictionary<string, ICreator>();

            this.operatingSystem = Environment.OSVersion;
            this.operatingSystemVersion = this.operatingSystem.Version;
            logger.Info("Operating System: {0} ({1})", operatingSystem.VersionString, operatingSystemVersion.ToString());
        }

        public void Initialize()
        {
            if (!initialized)
            {
                initialized = true;

                logger.Info("[ANX] Initializing ANX.Framework AddInSystemFactory...");

                String executingAssembly = Assembly.GetExecutingAssembly().Location;

                foreach (String file in Directory.EnumerateFiles(Path.GetDirectoryName(executingAssembly), "*.dll", SearchOption.TopDirectoryOnly))
                {
                    if (!file.Equals(executingAssembly))
                    {
                        logger.Info("[ANX] trying to load '{0}'...", file);

                        Assembly part = null;

                        try
                        {
                            part = Assembly.LoadFile(file);
                        }
                        catch (Exception ex)
                        {
                            logger.Debug("error calling Assembly.LoadFile({0}) Exception: {1}", file, ex.Message);
                        }

                        if (part != null)
                        {
                            logger.Info("[ANX] scanning for ANX interfaces...");

                            foreach (Type t in part.GetTypes().Where(p => typeof(IInputSystemCreator).IsAssignableFrom(p) ||
                                                                          typeof(IRenderSystemCreator).IsAssignableFrom(p) ||
                                                                          typeof(ISoundSystemCreator).IsAssignableFrom(p)
                                                                    ))
                            {
                                logger.Info("[ANX] testing if assembly is supported on current platform");
                                string[] platforms = FetchSupportedPlattforms(part);
                                bool supportedPlatform = false;

                                foreach (string platform in platforms)
                                {
                                    if (string.Equals(OperatingSystem.Platform.ToString(), platform, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        supportedPlatform = true;
                                        break;
                                    }
                                }

                                if (supportedPlatform)
                                {
                                    logger.Info("[ANX] registering instance of '{0}'...", t.FullName);

                                    var instance = part.CreateInstance(t.FullName);
                                    ((ICreator)instance).RegisterCreator(this);
                                }
                                else
                                {
                                    logger.Info("[ANX] current platform '{0}' is not supported by '{1}'", OperatingSystem.Platform.ToString(), t.FullName);
                                }
                            }
                        }
                    }
                }

                SetDefaultCreators();
            }
        }

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

        public bool HasFramework(String name)
        {
            return creators.ContainsKey(name.ToLowerInvariant());
        }

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

        public IEnumerable<T> GetCreators<T>() where T : class, ICreator
        {
            if (!initialized)
            {
                Initialize();
            }

            Type t = typeof(T);

            foreach (ICreator creator in this.creators.Values)
            {
                if (creator.GetType().GetInterfaces()[0].Equals(t))
                {
                    yield return creator as T;
                }
            }
        }

        public T GetDefaultCreator<T>() where T : class, ICreator
        {
            if (!initialized)
            {
                Initialize();
            }

            Type type = typeof(T);

            if (defaultCreators.ContainsKey(type))
            {
                return defaultCreators[type] as T;
            }

            logger.Error(String.Format("couldn't find a DefaultCreator of type '{0}'. Listing all registered creators: ", type.FullName));
            foreach (KeyValuePair<Type, ICreator> kvp in defaultCreators)
            {
                logger.Error(kvp.Key);
            }

            throw new AddInLoadingException(String.Format("couldn't find a DefaultCreator of type '{0}'", type.FullName));
        }

        public void SetDefaultCreator<T>(T creator) where T : class, ICreator
        {
            Type t = typeof(T);
            logger.Debug("setting DefaultCreator by type: {0}", t.FullName);
            defaultCreators[t] = creator;
        }

        public void SetDefaultCreator(string creatorName)
        {
            if (!initialized)
            {
                Initialize();
            }

            ICreator creator = null;
            creators.TryGetValue(creatorName.ToLowerInvariant(), out creator);
            if (creator != null)
            {
                Type t = creator.GetType().GetInterfaces()[0];
                if (t == typeof(ICreator))
                {
                    //TODO: exception handling
                    t = creator.GetType().GetInterfaces()[1];
                }
                logger.Debug("setting DefaultCreator by name: '{0}'. Resolved type: '{1}'. ", creatorName, t.FullName);
                defaultCreators[t] = creator;
            }
            else
            {
                throw new AddInLoadingException(String.Format("couldn't set DefaultCreator by name: '{0}'. ", creatorName));
            }
        }

        public OperatingSystem OperatingSystem
        {
            get
            {
                return this.operatingSystem;
            }
        }

        public Version OperatingSystemVersion
        {
            get
            {
                return this.operatingSystemVersion;
            }
        }

        private void SetDefaultCreators()
        {
            foreach (ICreator creator in this.creators.Values)
            {
                string type = creator.GetType().GetInterfaces()[0].ToString();

                switch (type)
                {
                    case "ANX.Framework.NonXNA.IRenderSystemCreator":
                        IRenderSystemCreator renderSystemCreator = creator as IRenderSystemCreator;
                        IRenderSystemCreator defaultRenderSystemCreator = null;
                        if (defaultCreators.ContainsKey(typeof(IRenderSystemCreator)))
                        {
                            renderSystemCreator = defaultCreators[typeof(IRenderSystemCreator)] as IRenderSystemCreator;
                        }

                        if (renderSystemCreator != null && (defaultRenderSystemCreator == null || defaultRenderSystemCreator.Priority > renderSystemCreator.Priority))
                        {
                            SetDefaultCreator<IRenderSystemCreator>(renderSystemCreator);
                        }
                        break;
                    case "ANX.Framework.NonXNA.ISoundSystemCreator":
                        ISoundSystemCreator soundSystemCreator = creator as ISoundSystemCreator;
                        ISoundSystemCreator defaultSoundSystemCreator = null;
                        if (defaultCreators.ContainsKey(typeof(ISoundSystemCreator)))
                        {
                            defaultSoundSystemCreator = defaultCreators[typeof(ISoundSystemCreator)] as ISoundSystemCreator;
                        }

                        if (soundSystemCreator != null && (defaultSoundSystemCreator == null || defaultSoundSystemCreator.Priority > soundSystemCreator.Priority))
                        {
                            SetDefaultCreator<ISoundSystemCreator>(soundSystemCreator);
                        }
                        break;
                    case "ANX.Framework.NonXNA.IInputSystemCreator":
                        IInputSystemCreator inputSystemCreator = creator as IInputSystemCreator;
                        IInputSystemCreator defaultInputSystemCreator = null;
                        if (defaultCreators.ContainsKey(typeof(IInputSystemCreator)))
                        {
                            defaultInputSystemCreator = defaultCreators[typeof(IInputSystemCreator)] as IInputSystemCreator;
                        }

                        if (inputSystemCreator != null && (defaultInputSystemCreator == null || defaultInputSystemCreator.Priority > inputSystemCreator.Priority))
                        {
                            SetDefaultCreator<IInputSystemCreator>(inputSystemCreator);
                        }
                        break;
                    case "ANX.Framework.NonXNA.ICreator":
                        break;
                    default:
                        throw new InvalidOperationException(String.Format("unable to set a default system for creator of type '{0}'", type));
                }
            }
        }

        private string[] FetchSupportedPlattforms(Assembly assembly)
        {
            string[] platforms = null;
            string[] res = assembly.GetManifestResourceNames();

            if (res != null)
            {
                foreach (string ressource in res)
                {
                    Stream manifestResourceStream = assembly.GetManifestResourceStream(ressource);

                    if (manifestResourceStream != null)
                    {
                        using (ResourceReader resourceReader = new ResourceReader(manifestResourceStream))
                        {
                            IDictionaryEnumerator dict = resourceReader.GetEnumerator();
                            while (dict.MoveNext())
                            {
                                if (string.Equals(dict.Key.ToString(), "SupportedPlatforms", StringComparison.InvariantCultureIgnoreCase) &&
                                    dict.Value.GetType() == typeof(string))
                                {
                                    platforms = dict.Value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    return platforms;
                                }
                            }
                            resourceReader.Close();
                        }
                    }
                }
            }

            return platforms;
        }

    }
}
