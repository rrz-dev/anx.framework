using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ANX.Framework.NonXNA.Reflection
{
	internal static class AssemblyLoader
	{
		#region Private
		private static List<Assembly> allAssemblies;
		#endregion

		#region Constructor
		static AssemblyLoader()
		{
			allAssemblies = new List<Assembly>();
			LoadAssembliesFromFile();
			LoadAssembliesFromTypeList();
		}
		#endregion

		#region GetAllAssemblies
		public static Assembly[] GetAllAssemblies()
		{
			return allAssemblies.ToArray();
		}
		#endregion

		#region LoadAssembliesFromFile
		private static void LoadAssembliesFromFile()
		{
#if !ANDROID && !WINDOWSMETRO
			string executingAssemblyFilepath = Assembly.GetExecutingAssembly().Location;
			string basePath = Path.GetDirectoryName(executingAssemblyFilepath);

			string[] allAssembliesFiles = Directory.GetFiles(basePath, "*.dll",
				SearchOption.TopDirectoryOnly);

			foreach (string file in allAssembliesFiles)
			{
				if (file.Equals(executingAssemblyFilepath) == false)
				{
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
			}
#endif
		}
		#endregion

		#region LoadAssembliesFromTypeList
		private static void LoadAssembliesFromTypeList()
		{
			AssemblyListFile typeListFile = new AssemblyListFile();
			typeListFile.Load();

			string[] allAssemblyNames = typeListFile.GetAllAssemblyNames();
			foreach (string assemblyName in allAssemblyNames)
			{
				try
				{
#if WINDOWSMETRO
					Assembly assembly = Assembly.Load(new AssemblyName(assemblyName));
#else
					Assembly assembly = Assembly.Load(assemblyName);
#endif
					allAssemblies.Add(assembly);
				}
				catch
				{
				}
			}
		}
		#endregion
	}
}
