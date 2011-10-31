using System;

namespace ANXStatusComparer
{
	/// <summary>
	/// The type of comparing the assemblies.
	/// </summary>
	public enum CheckType
	{
		All,
		Namespaces,
		Classes,
		Structs,
		Interfaces,
		Enumerations,
	}
}
