#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.Input;
using NLog;

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
        }

        public void Initialize()
        {
            if (!initialized)
            {
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
                                logger.Info("[ANX] registering instance of '{0}'...", t.FullName);

                                var instance = part.CreateInstance(t.FullName);
                                ((ICreator)instance).RegisterCreator(this);
                            }
                        }
                    }
                }

                initialized = true;
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
        }

        public bool HasFramework(String name)
        {
            return creators.ContainsKey(name.ToLowerInvariant());
        }

        public T GetCreator<T>(String name) where T : class, ICreator
        {
            ICreator creator = null;
            creators.TryGetValue(name.ToLowerInvariant(), out creator);
            return creator as T;
        }

        public IEnumerable<T> GetCreators<T>() where T : class, ICreator
        {
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
            Type type = typeof(T);

            if (defaultCreators.ContainsKey(type))
            {
                return defaultCreators[type] as T;
            }

            return default(T);
        }

        public void SetDefaultCreator<T>(T creator) where T : class, ICreator
        {
            defaultCreators[typeof(T)] = creator;
        }

        public void SetDefaultCreator(string creatorName)
        {
            ICreator creator = null;
            creators.TryGetValue(creatorName.ToLowerInvariant(), out creator);
            defaultCreators[creator.GetType().GetInterfaces()[0]] = creator;
        }

    }
}
