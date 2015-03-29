using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using ANX.Framework.NonXNA.InputSystem;

namespace ANX.Framework.NonXNA.Reflection
{
    internal static class AssemblyLoader
    {
        #region Constants
        private static readonly string[] IgnoreAssemblies =
        {
            "OpenTK.dll",
            "OpenTK.GLControl.dll",
            "OpenTK.Compatibility.dll",
            "sharpdx_direct3d11_effects_x86.dll",
            "sharpdx_direct3d11_effects_x64.dll",
            "SharpDX.dll",
            "SharpDX.Direct3D11.dll",
            "SharpDX.Direct3D10.dll",
            "SharpDX.D3DCompiler.dll",
            "SharpDX.DXGI.dll",
            "SharpDX.XInput.dll",
            "SharpDX.DirectInput.dll",
            "WaveUtils.dll",
            "SharpDX.XAudio2.dll",
            "System.dll",
            "System.Core.dll",
            "System.Xml.dll",
            "System.Xml.Linq.dll",
            "mscorlib.dll",
            "Sce.PlayStation.Core.dll",
            "wrap_oal.dll",
            "OpenAL32.dll",
            "nunit.framework.dll",
            "OggUtils.dll",
            "Microsoft.Research.Kinect.dll",
            "Microsoft.Xna.Framework.Graphics.dll",
            "Microsoft.Xna.Framework.Game.dll",
            "Microsoft.Xna.Framework.Content.Pipeline.dll",
            "OggUtils.dll",
            "OggUtils.dll",
        };
        #endregion

        #region Private
        private static List<Assembly> allAssemblies = new List<Assembly>();
        private static bool initialized = false;
        private static List<Type> creatorTypes = new List<Type>();
        private static List<Type> supportedPlatformsTypes = new List<Type>();
        #endregion

        #region Public
        public static List<Type> CreatorTypes
        {
            get
            {
                InitializeIfNotInitializedYet();
                return creatorTypes;
            }
        }

        public static List<Type> SupportedPlatformsTypes
        {
            get
            {
                InitializeIfNotInitializedYet();
                return supportedPlatformsTypes;
            }
        }
        #endregion

        private static void InitializeIfNotInitializedYet()
        {
            if (initialized)
                return;

            LoadAllAssemblies();
            SearchForValidAddInTypes();
            initialized = true;
        }

        #region LoadAllAssemblies
        private static void LoadAllAssemblies()
        {
            LoadAssembliesFromFile();
            LoadAssembliesFromNames();

            // Also load the current assembly. This is needed when we run on android or win8 with merged assemblies.
#if !WINDOWSMETRO
            var entryAssembly = Assembly.GetEntryAssembly();
            //Entry assembly could be null if the managed code was called directly from native code without going through a Main entry point.
            //Which would for example happen when the tests are run via NUnit.
            if (entryAssembly != null)
                allAssemblies.Add(entryAssembly);
#else
            // TODO: a lot of testing is required!
            allAssemblies.Add(typeof(AssemblyLoader).GetTypeInfo().Assembly);
#endif
        }
        #endregion

        #region LoadAssembliesFromFile
        private static void LoadAssembliesFromFile()
        {
#if !ANDROID && !WINDOWSMETRO
            string executingAssemblyFilepath = Assembly.GetExecutingAssembly().Location;
            string basePath = Path.GetDirectoryName(executingAssemblyFilepath);

            List<string> assembliesInPath = new List<string>();
            assembliesInPath.AddRange(Directory.GetFiles(basePath, "*.dll", SearchOption.TopDirectoryOnly));
            assembliesInPath.AddRange(Directory.GetFiles(basePath, "*.exe", SearchOption.TopDirectoryOnly));

            foreach (string file in assembliesInPath)
            {
                bool ignore = false;
                foreach (string ignoreName in IgnoreAssemblies)
                {
                    if (file.EndsWith(ignoreName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                    continue;

                Logger.Info("[ANX] trying to load '" + file + "'...");
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    allAssemblies.Add(assembly);
                }
                catch
                {
                }
            }
#endif
        }
        #endregion

        #region LoadAssembliesFromNames
        private static void LoadAssembliesFromNames()
        {
            List<string> allAssemblyNames = new List<string>();

#if WINDOWSMETRO
            allAssemblyNames.Add("ANX.PlatformSystem.Metro");
            allAssemblyNames.Add("ANX.RenderSystem.Windows.Metro");
            allAssemblyNames.Add("ANX.InputSystem.Standard");
            allAssemblyNames.Add("ANX.InputDevices.Windows.ModernUI");
            allAssemblyNames.Add("ANX.SoundSystem.Windows.XAudio");
#endif

            foreach (string assemblyName in allAssemblyNames)
            {
                Assembly loadedAssembly = LoadAssemblyByName(assemblyName);
                if (loadedAssembly != null)
                    allAssemblies.Add(loadedAssembly);
            }
        }
        #endregion

        #region LoadAssemblyByName
        private static Assembly LoadAssemblyByName(string assemblyName)
        {
            try
            {
#if WINDOWSMETRO
                return Assembly.Load(new AssemblyName(assemblyName));
#else
                return Assembly.Load(assemblyName);
#endif
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region SearchForValidAddInTypes
        private static void SearchForValidAddInTypes()
        {
            foreach (Assembly assembly in allAssemblies)
                SearchForValidAddInTypesInAssembly(assembly);
        }
        #endregion

        #region SearchForValidAddInTypesInAssembly
        private static void SearchForValidAddInTypesInAssembly(Assembly assembly)
        {
            var assemblyAttributes = assembly.GetCustomAttributes<SupportedPlatformsAttribute>();

            //This step before we are iterating over the types makes the startup faster, around 2 seconds faster.
            var supportedPlatforms = assemblyAttributes.SelectMany((x) => x.Platforms).ToArray();
            if (supportedPlatforms.Length == 0)
            {
#if !WINDOWSMETRO
                var ownAssemblyName = TypeHelper.GetAssemblyFrom(typeof(SupportedPlatformsAttribute)).GetName();
                Version otherVersion = null;
                //If another version is referenced, we can't load our custom attribute.
                //Unfortunately it's not possible to check in WinRT if actually the same assembly is referenced.

                if (assembly.GetReferencedAssemblies().Any((x) =>
                {
                    otherVersion = x.Version;
                    return x.Name == ownAssemblyName.Name && x.Version != ownAssemblyName.Version;
                }))
                {
                    Logger.Warning(string.Format("Assembly \"{0}\" can't be correctly loaded because it's referencing another ANX.Framework version than the executing assembly. Current: {1}, Foreign: {2}", assembly.FullName, ownAssemblyName.Version, otherVersion), false);
                }
                else
#endif
                {
                    Logger.Info(string.Format("Skipping assembly \"{0}\" because no supported platforms are specified in the assembly meta data.", assembly.FullName));
                }
                return;
            }

            if (!supportedPlatforms.Contains(OSInformation.GetName()))
            {
                Logger.Info(string.Format("Skipping assembly \"{0}\" because it doesn't support the current platform.", assembly.FullName));
                return;
            }

            Logger.Info("checking assembly \"{0}\".", assembly.FullName);
            Type[] allTypes = TypeHelper.SafelyExtractTypesFrom(assembly);

            foreach (Type type in allTypes)
            {
                //Can happen if we have types that are incompatible with our Runtime version.
                //TODO: Maybe we should instead throw an error?
                //Would be a very annoying error to find for someone who uses the code.
                if (type == null)
                    continue;

                bool isTypeCreatable = TypeHelper.IsAbstract(type) == false && TypeHelper.IsInterface(type) == false;
                if (isTypeCreatable)
                {
                    bool isTypeValidCreator = TypeHelper.IsTypeAssignableFrom(typeof(ICreator), type);
                    if (isTypeValidCreator)
                    {
                        creatorTypes.Add(type);
                        continue;
                    }

                    bool isInputCreator = TypeHelper.IsTypeAssignableFrom(typeof(IInputDeviceCreator), type);
                    if (isInputCreator)
                    {
                        var inputCreator = TypeHelper.Create<IInputDeviceCreator>(type);
                        InputDeviceFactory.Instance.AddCreator(type, inputCreator);
                    }
                }
            }
        }
        #endregion
    }
}
