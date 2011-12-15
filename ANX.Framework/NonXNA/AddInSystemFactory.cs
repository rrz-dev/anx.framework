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
using System.Xml.Linq;

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
        private OperatingSystem operatingSystem;
        private Version operatingSystemVersion;

        private string preferredRenderSystem;
        private bool preferredRenderSystemLocked;
        private string preferredInputSystem;
        private bool preferredInputSystemLocked;
        private string preferredSoundSystem;
        private bool preferredSoundSystemLocked;

        private List<AddIn> renderSystems;
        private List<AddIn> inputSystems;
        private List<AddIn> soundSystems;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion // Private Members

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
            this.renderSystems = new List<AddIn>();
            this.inputSystems = new List<AddIn>();
            this.soundSystems = new List<AddIn>();

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

                        AddIn addin = new AddIn(file);
                        if (addin.IsValid && addin.IsSupported)
                        {
                            switch (addin.Type)
                            {
                                case AddInType.InputSystem:
                                    this.inputSystems.Add(addin);
                                    break;
                                case AddInType.RenderSystem:
                                    this.renderSystems.Add(addin);
                                    break;
                                case AddInType.SoundSystem:
                                    this.soundSystems.Add(addin);
                                    break;
                            }
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
            AddInType addInType = GetAddInType(type);

            switch (addInType)
            {
                case AddInType.InputSystem:
                    if (string.IsNullOrEmpty(preferredInputSystem))
                    {
                        if (inputSystems.Count > 0)
                        {
                            return inputSystems[0].Instance as T;
                        }

                        throw new AddInLoadingException("couldn't get default input system because there are no registered input systems available");
                    }
                    else
                    {
                        foreach (AddIn addin in this.inputSystems)
                        {
                            if (addin.Name.Equals(preferredInputSystem, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return addin.Instance as T;
                            }
                        }

                        throw new AddInLoadingException(String.Format("couldn't get default input system '{0}' because it was not found in the list of registered creators", preferredInputSystem));
                    }
                case AddInType.RenderSystem:
                    if (string.IsNullOrEmpty(preferredRenderSystem))
                    {
                        if (renderSystems.Count > 0)
                        {
                            return renderSystems[0].Instance as T;
                        }

                        throw new AddInLoadingException("couldn't get default render system because there are no registered render systems available");
                    }
                    else
                    {
                        foreach (AddIn addin in this.renderSystems)
                        {
                            if (addin.Name.Equals(preferredRenderSystem, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return addin.Instance as T;
                            }
                        }

                        throw new AddInLoadingException(String.Format("couldn't get default render system '{0}' because it was not found in the list of registered creators", preferredRenderSystem));
                    }
                case AddInType.SoundSystem:
                    if (string.IsNullOrEmpty(preferredSoundSystem))
                    {
                        if (soundSystems.Count > 0)
                        {
                            return soundSystems[0].Instance as T;
                        }

                        throw new AddInLoadingException("couldn't get default sound system because there are no registered sound systems available");
                    }
                    else
                    {
                        foreach (AddIn addin in this.soundSystems)
                        {
                            if (addin.Name.Equals(preferredSoundSystem, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return addin.Instance as T;
                            }
                        }

                        throw new AddInLoadingException(String.Format("couldn't get default sound system '{0}' because it was not found in the list of registered creators", preferredSoundSystem));
                    }

            }

            throw new AddInLoadingException(String.Format("couldn't find a DefaultCreator of type '{0}'", type.FullName));
        }

        public void SortAddIns()
        {
            this.inputSystems.Sort();
            this.renderSystems.Sort();
            this.soundSystems.Sort();

            this.creators = this.creators.OrderBy(x => x.Value.Priority).ToDictionary(x => x.Key, x => x.Value);
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

        public string PreferredRenderSystem
        {
            get
            {
                return this.preferredRenderSystem;
            }
            set
            {
                if (this.preferredRenderSystemLocked)
                {
                    throw new AddInLoadingException("can't set preferred RenderSystem because a RenderSystem is alread in use.");
                }

                this.preferredRenderSystem = value;
            }
        }

        public string PreferredInputSystem
        {
            get
            {
                return this.preferredInputSystem;
            }
            set
            {
                if (this.preferredInputSystemLocked)
                {
                    throw new AddInLoadingException("can't set preferred InputSystem because a InputSystem is alread in use.");
                }

                this.preferredInputSystem = value;
            }
        }

        public string PreferredSoundSystem
        {
            get
            {
                return this.preferredSoundSystem;
            }
            set
            {
                if (this.preferredSoundSystemLocked)
                {
                    throw new AddInLoadingException("can't set preferred SoundSystem because a SoundSystem is alread in use.");
                }

                this.preferredSoundSystem = value;
            }
        }

        public void PreventRenderSystemChange()
        {
            this.preferredRenderSystemLocked = true;
        }

        public void PreventInputSystemChange()
        {
            this.preferredInputSystemLocked = true;
        }

        public void PreventSoundSystemChange()
        {
            this.preferredSoundSystemLocked = true;
        }

        private AddInType GetAddInType(Type t)
        {
            if (typeof(IRenderSystemCreator).IsAssignableFrom(t))
            {
                return AddInType.RenderSystem;
            }
            else if (typeof(IInputSystemCreator).IsAssignableFrom(t))
            {
                return AddInType.InputSystem;
            }
            else if (typeof(ISoundSystemCreator).IsAssignableFrom(t))
            {
                return AddInType.SoundSystem;
            }
            else
            {
                return AddInType.Unknown;
            }
        }
    }
}
