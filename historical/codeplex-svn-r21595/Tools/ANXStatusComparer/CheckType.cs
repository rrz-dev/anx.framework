using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
