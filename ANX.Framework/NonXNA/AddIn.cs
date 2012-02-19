#region Using Statements
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Resources;
using System.Collections;
using System.Linq;
using ANX.Framework.NonXNA.InputSystem;

#endregion

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
    public class AddIn : IComparable<AddIn>
    {
        #region Private Members
        private string fileName;
        private List<string> platforms = new List<string>();
        private Assembly assembly;
        private Type creatorType;
        private ICreator instance;
        private AddInType addInType;

        #endregion // Private Members

        #region Constructor

        public AddIn(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            if (File.Exists(fileName) == false)
            {
                throw new InvalidOperationException(
									String.Format("The AddIn '{0}' does not exist.", fileName));
            }

            this.fileName = fileName;

            try
            {
                this.assembly = Assembly.LoadFrom(fileName);
            }
            catch { }

            if (this.assembly != null)
            {
							Type[] allTypes;
							try
							{
								allTypes = this.assembly.GetTypes();
							}
							catch (ReflectionTypeLoadException ex)
							{
								allTypes = ex.Types;
							}

                foreach (Type t in allTypes.Where(p =>
									typeof(IInputSystemCreator).IsAssignableFrom(p) ||
									typeof(IRenderSystemCreator).IsAssignableFrom(p) ||
									typeof(ISoundSystemCreator).IsAssignableFrom(p) ||
									typeof(IMediaSystemCreator).IsAssignableFrom(p)))
                {
                    this.creatorType = t;
										this.addInType = AddInSystemFactory.GetAddInType(t);
                    break;
                }

                if (this.creatorType != null)
                {
                    try
                    {
                        this.platforms.AddRange(FetchSupportedPlattforms(this.assembly));
                    }
                    catch { }
                }

                //
                // Scan the addin for InputDeviceCreators and register them
                //

                foreach (Type t in allTypes.Where(p =>
									typeof(IGamePadCreator).IsAssignableFrom(p)))
                {
                    InputDeviceFactory.Instance.AddCreator(Activator.CreateInstance(t) as IGamePadCreator);
                }

                foreach (Type t in allTypes.Where(p =>
									typeof(IKeyboardCreator).IsAssignableFrom(p)))
                {
                    InputDeviceFactory.Instance.AddCreator(Activator.CreateInstance(t) as IKeyboardCreator);
                }

                foreach (Type t in allTypes.Where(p =>
									typeof(IMouseCreator).IsAssignableFrom(p)))
                {
                    InputDeviceFactory.Instance.AddCreator(Activator.CreateInstance(t) as IMouseCreator);
                }

#if XNAEXT
                foreach (Type t in allTypes.Where(p =>
									typeof(IMotionSensingDeviceCreator).IsAssignableFrom(p)))
                {
                    InputDeviceFactory.Instance.AddCreator(Activator.CreateInstance(t) as IMotionSensingDeviceCreator);
                }
#endif
            }
        }

        #endregion // Constructor

        #region Puplic Methods


        #endregion // Public Methods

        #region Properties

        /// <summary>
        /// Returns whether this is a valid AddIn of the ANX.Framework.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.assembly != null && this.creatorType != null;
            }
        }

        /// <summary>
        /// The filename of the current AddIn assembly
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Returns true if this AddIn is supported by the current Platform
        /// </summary>
        public bool IsSupported
        {
            get
            {
                if (this.assembly != null && this.platforms != null && this.platforms.Count > 0)
                {
                    foreach (string platform in this.platforms)
                    {
                        if (string.Equals(Environment.OSVersion.Platform.ToString(), platform, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns the version of the AddIn
        /// </summary>
        public Version Version
        {
            get
            {
                if (this.assembly != null)
                {
                    return this.assembly.GetName().Version;
                }

                return null;
            }
        }

        public string Name
        {
            get
            {
                if (this.assembly != null)
                {
                    return this.Instance.Name;
                }

                return String.Empty;
            }
        }

        public int Priority
        {
            get
            {
                if (this.assembly != null)
                {
                    return this.Instance.Priority;
                }

                return int.MaxValue;
            }
        }

        public AddInType Type
        {
            get
            {
                return this.addInType;
            }
        }

        /// <summary>
        /// Returns the concrete instance of the creator of this AddIn. The instance is cached and
        /// only created on first access.
        /// </summary>
        /// <remarks>
        /// If this AddIn is not supported on this platform the instance will be null.
        /// </remarks>
        public ICreator Instance
        {
            get
            {
                if (instance == null && IsSupported)
                {
                    this.instance = this.assembly.CreateInstance(this.creatorType.FullName) as ICreator;
                    if (this.instance != null)
                    {
                        this.instance.RegisterCreator(AddInSystemFactory.Instance);
                    }
                }

                return this.instance;
            }
        }

        #endregion // Properties

        #region Private Helpers

				#region FetchSupportedPlattforms
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
				#endregion

				#endregion // Private Helpers

				public int CompareTo(AddIn other)
        {
            return this.Priority.CompareTo(other.Priority);
        }
    }
}
