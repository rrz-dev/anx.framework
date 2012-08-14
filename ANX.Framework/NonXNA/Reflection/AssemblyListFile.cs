using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ANX.Framework.NonXNA.Windows8;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.Reflection
{
	/// <summary>
	/// The AssemblyListFile is a data holder of all available assembly names.
	/// This is necessary cause on some platforms we can't get all the *.dll files
	/// in the current directory. So we simply load this file from the
	/// content/assets/etc. and call Assembly.Load with the names.
	/// </summary>
	internal class AssemblyListFile
	{
		#region Constants
		private const string Filename = "AssemblyList.bin";
		#endregion

		#region Private
		private List<string> allAssemblyNames;
		#endregion

		#region Constructor
		public AssemblyListFile()
		{
			allAssemblyNames = new List<string>();
		}
		#endregion

		#region GetAllAssemblyNames
		public string[] GetAllAssemblyNames()
		{
			return allAssemblyNames.ToArray();
		}
		#endregion

		#region SetAllAssemblyNames
		public void SetAllAssemblyNames(Assembly[] assemblies)
		{
			allAssemblyNames.Clear();
			foreach (Assembly assembly in assemblies)
			{
				allAssemblyNames.Add(assembly.FullName);
			}
		}
		#endregion

		#region Load
		public void Load()
		{
			Stream assemblyListStream = null;

#if WINDOWSMETRO
			assemblyListStream = LoadStreamFromMetroAssets();
#endif

			if (assemblyListStream != null)
			{
				LoadFromStream(assemblyListStream);
			}
		}
		#endregion

		#region LoadStreamFromMetroAssets
#if WINDOWSMETRO
		private Stream LoadStreamFromMetroAssets()
		{
			Stream result = AssetsHelper.LoadStreamFromAssets("Assets\\" + Filename);
			
			if(result == null)
			{
				result = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(result);
				writer.Write(5);
				writer.Write("ANX.PlatformSystem.Metro");
				writer.Write("ANX.RenderSystem.Windows.Metro");
				writer.Write("ANX.InputSystem.Standard");
				writer.Write("ANX.MediaSystem.Windows.OpenAL");
				writer.Write("ANX.SoundSystem.Windows.XAudio");

				result.Position = 0;
			}

			return result;
		}
#endif
		#endregion

		#region LoadFromStream
		private void LoadFromStream(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);

			int numberOfAssemblyNames = reader.ReadInt32();
			for (int assemblyIndex = 0; assemblyIndex < numberOfAssemblyNames;
				assemblyIndex++)
			{
				allAssemblyNames.Add(reader.ReadString());
			}
		}
		#endregion

		#region Save
		public void Save(Stream stream)
		{
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(allAssemblyNames.Count);
			foreach (string assemblyName in allAssemblyNames)
			{
				writer.Write(assemblyName);
			}
		}
		#endregion
	}
}
