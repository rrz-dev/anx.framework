using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ANX.Framework.NonXNA.InputSystem;

namespace ANX.Framework.NonXNA.Reflection
{
	internal static class AssemblyLoader
	{
		#region Private
		private static List<Assembly> allAssemblies;
		#endregion

		#region Public
		public static List<Type> CreatorTypes
		{
			get;
			private set;
		}

		public static List<Type> SupportedPlatformsTypes
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		static AssemblyLoader()
		{
			allAssemblies = new List<Assembly>();
			CreatorTypes = new List<Type>();
			SupportedPlatformsTypes = new List<Type>();

			LoadAllAssemblies();
			SearchForValidAddInTypes();
		}
		#endregion

		#region LoadAllAssemblies
		private static void LoadAllAssemblies()
		{
			LoadAssembliesFromFile();
			LoadAssembliesFromNames();

#if !WINDOWSMETRO // TODO: find way for metro
			// Also load the current assembly. This is needed when we run on android or win8 with merged assemblies.
			allAssemblies.Add(Assembly.GetEntryAssembly());
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
				if (file.EndsWith("OpenTK.dll") ||
					file.EndsWith("OpenTK.GLControl.dll") ||
					file.EndsWith("OpenTK.Compatibility.dll") ||
					file.EndsWith("SharpDX.dll") ||
					file.EndsWith("SharpDX.Direct3D11.dll") ||
					file.EndsWith("SharpDX.Direct3D10.dll") ||
					file.EndsWith("SharpDX.D3DCompiler.dll") ||
					file.EndsWith("SharpDX.DXGI.dll") ||
					file.EndsWith("SharpDX.XInput.dll") ||
					file.EndsWith("SharpDX.DirectInput.dll") ||
					file.EndsWith("WaveUtils.dll") ||
					file.EndsWith("SharpDX.XAudio2.dll"))
				{
					continue;
				}

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
			allAssemblyNames.Add("ANX.InputDevices.Windows.XInput");
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
			{
				SearchForValidAddInTypesInAssembly(assembly);
			}
		}
		#endregion

		#region SearchForValidAddInTypesInAssembly
		private static void SearchForValidAddInTypesInAssembly(Assembly assembly)
		{
			Type[] allTypes = TypeHelper.SafelyExtractTypesFrom(assembly);

			foreach (Type type in allTypes)
			{
				if (type == null)
					continue;

				bool isTypeCreatable = TypeHelper.IsAbstract(type) == false && TypeHelper.IsInterface(type) == false;

				bool isSupportedPlatformsImpl = TypeHelper.IsTypeAssignableFrom(typeof(ISupportedPlatforms), type);
				if (isSupportedPlatformsImpl && isTypeCreatable)
				{
					SupportedPlatformsTypes.Add(type);
					continue;
				}

				bool isTypeValidCreator = TypeHelper.IsAnyTypeAssignableFrom(AddInSystemFactory.ValidAddInCreators, type);
				if (isTypeValidCreator && isTypeCreatable)
				{
					CreatorTypes.Add(type);
					continue;
				}

				bool isTypeValidInputDevice = TypeHelper.IsAnyTypeAssignableFrom(InputDeviceFactory.ValidInputDeviceCreators,
					type);
				if (isTypeValidInputDevice && isTypeCreatable)
				{
					var inputCreator = TypeHelper.Create<IInputDeviceCreator>(type);
					InputDeviceFactory.Instance.AddCreator(type, inputCreator);
				}
			}
		}
		#endregion
	}
}
