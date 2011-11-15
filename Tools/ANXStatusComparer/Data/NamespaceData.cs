using System;
using System.Collections.Generic;

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

namespace ANXStatusComparer.Data
{
	/// <summary>
	/// Data holder for an assembly namespace.
	/// </summary>
	public class NamespaceData
	{
		#region Public
		/// <summary>
		/// True if the namespace contains any public types.
		/// </summary>
		public bool IsPublic
		{
			get;
			private set;
		}

		/// <summary>
		/// The name of the namespace.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// All types contained in this namespace.
		/// </summary>
		public List<Type> AllTypes
		{
			get;
			private set;
		}

		/// <summary>
		/// All interfaces of this namespace.
		/// </summary>
		public Dictionary<string, BaseObject> Interfaces
		{
			get;
			private set;
		}

		/// <summary>
		/// All classes of this namespace.
		/// </summary>
		public Dictionary<string, BaseObject> Classes
		{
			get;
			private set;
		}

		/// <summary>
		/// All structures of this namespace.
		/// </summary>
		public Dictionary<string, BaseObject> Structs
		{
			get;
			private set;
		}

		/// <summary>
		/// All enumerations of this namespace.
		/// </summary>
		public Dictionary<string, EnumData> Enums
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new namespace data holder.
		/// </summary>
		/// <param name="setName">The name of the namespace.</param>
		public NamespaceData(string setName)
		{
			Name = setName;
			AllTypes = new List<Type>();

			Interfaces = new Dictionary<string, BaseObject>();
			Structs = new Dictionary<string, BaseObject>();
			Classes = new Dictionary<string, BaseObject>();
			Enums = new Dictionary<string, EnumData>();
		}
		#endregion

		#region ParseTypes
		/// <summary>
		/// Parse all the added types. This is done after parsing all assemblies
		/// is finished and we're sure all types are available in here.
		/// </summary>
		public void ParseTypes()
		{
			IsPublic = false;
			foreach (Type type in AllTypes)
			{
				if (type.IsPublic)
				{
					IsPublic = true;

					if (type.IsInterface)
					{
						Interfaces.Add(type.Name, new BaseObject(type));
					}

					if (type.IsEnum)
					{
						Enums.Add(type.Name, new EnumData(type));
					}

					if (type.IsClass)
					{
						Classes.Add(type.Name, new BaseObject(type));
					}

					if (type.IsValueType &&
						type.IsEnum == false &&
						type.IsPrimitive == false)
					{
						Structs.Add(type.Name, new BaseObject(type));
					}
				}
			}
		}
		#endregion
	}
}
