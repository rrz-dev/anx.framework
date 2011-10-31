using System;
using System.Collections.Generic;
using System.Reflection;

namespace ANXStatusComparer.Data
{
	/// <summary>
	/// Assemblies data holder which preparses the loaded assemblies, like
	/// collecting the namespaces, etc.
	/// </summary>
	public class AssembliesData
	{
		#region Public
		/// <summary>
		/// All namespaces of the assemblies.
		/// </summary>
		public Dictionary<string, NamespaceData> Namespaces
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new Assembly data holder from the specified filepath and
		/// assembly type.
		/// </summary>
		/// <param name="filepaths">Filepaths to the assemblies to load.</param>
		public AssembliesData(string[] filepaths)
		{
			Namespaces = new Dictionary<string, NamespaceData>();

			foreach (string file in filepaths)
			{
				Assembly assembly = Assembly.LoadFile(file);

				Type[] types = new Type[0];
				try
				{
					// First of all collect all types from the assembly.
					types = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					// If loading all types failed, we can still get all the types
					// that were already sucessfully extracted.
					types = ex.Types;
				}

				// Now collect the namespaces from the types.
				foreach (Type type in types)
				{
					if (String.IsNullOrEmpty(type.Namespace))
					{
						continue;
					}

					if (Namespaces.ContainsKey(type.Namespace) == false)
					{
						Namespaces.Add(type.Namespace, new NamespaceData(type.Namespace));
					}
					Namespaces[type.Namespace].AllTypes.Add(type);
				}
			}

			foreach (string key in Namespaces.Keys)
			{
				Namespaces[key].ParseTypes();
			}
		}
		#endregion
	}
}
