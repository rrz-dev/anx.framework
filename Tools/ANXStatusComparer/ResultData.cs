using System;
using System.Collections.Generic;
using ANXStatusComparer.Data;

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

namespace ANXStatusComparer
{
	/// <summary>
	/// The result of the comparison.
	/// </summary>
	public class ResultData
	{
		#region WrongObjectPair (helper class)
		/// <summary>
		/// A pair of two base objects and two lists of wrong methods.
		/// </summary>
		public class WrongObjectPair
		{
			public BaseObject XnaObject;
			public List<BaseObjectElement> XnaElements =
				new List<BaseObjectElement>();

			public BaseObject AnxObject;
			public List<BaseObjectElement> AnxElements =
				new List<BaseObjectElement>();

			public List<string> MissingParents = new List<string>();
			public List<string> WrongAccesses = new List<string>();
		}
		#endregion

		#region Public
		#region Namespace Results
		/// <summary>
		/// List of missing namespaces in ANX.
		/// </summary>
		public List<string> MissingNamespaces
		{
			get;
			private set;
		}

		/// <summary>
		/// List of implemented namespaces in ANX.
		/// </summary>
		public List<string> ImplementedNamespaces
		{
			get;
			private set;
		}
		#endregion

		#region Class Results
		/// <summary>
		/// List of missing classes.
		/// </summary>
		public List<BaseObject> MissingClasses
		{
			get;
			private set;
		}

		/// <summary>
		/// List of wrong class implementations.
		/// </summary>
		public List<WrongObjectPair> WrongClasses
		{
			get;
			private set;
		}

		/// <summary>
		/// List of implemented classes.
		/// </summary>
		public List<BaseObject> ImplementedClasses
		{
			get;
			private set;
		}
		#endregion

		#region Interface Results
		/// <summary>
		/// List of missing interfaces.
		/// </summary>
		public List<BaseObject> MissingInterfaces
		{
			get;
			private set;
		}

		/// <summary>
		/// List of wrong interface implementations.
		/// </summary>
		public List<WrongObjectPair> WrongInterfaces
		{
			get;
			private set;
		}

		/// <summary>
		/// List of implemented interfaces.
		/// </summary>
		public List<BaseObject> ImplementedInterfaces
		{
			get;
			private set;
		}
		#endregion

		#region Struct Results
		/// <summary>
		/// List of missing structs.
		/// </summary>
		public List<BaseObject> MissingStructs
		{
			get;
			private set;
		}

		/// <summary>
		/// List of wrong struct implementations.
		/// </summary>
		public List<WrongObjectPair> WrongStructs
		{
			get;
			private set;
		}

		/// <summary>
		/// List of implemented structs.
		/// </summary>
		public List<BaseObject> ImplementedStructs
		{
			get;
			private set;
		}
		#endregion

		#region Enum Results
		/// <summary>
		/// List of missing enumerations.
		/// </summary>
		public List<EnumData> MissingEnums
		{
			get;
			private set;
		}

		/// <summary>
		/// List of implemented enumerations.
		/// </summary>
		public List<EnumData> ImplementedEnums
		{
			get;
			private set;
		}

		/// <summary>
		/// List of wrong enumerations.
		/// </summary>
		public List<KeyValuePair<EnumData, EnumData>> WrongEnums
		{
			get;
			private set;
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new result data holder.
		/// </summary>
		public ResultData()
		{
			MissingNamespaces = new List<string>();
			ImplementedNamespaces = new List<string>();

			MissingEnums = new List<EnumData>();
			ImplementedEnums = new List<EnumData>();
			WrongEnums = new List<KeyValuePair<EnumData, EnumData>>();

			MissingStructs = new List<BaseObject>();
			ImplementedStructs = new List<BaseObject>();
			WrongStructs = new List<WrongObjectPair>();

			MissingInterfaces = new List<BaseObject>();
			ImplementedInterfaces = new List<BaseObject>();
			WrongInterfaces = new List<WrongObjectPair>();

			MissingClasses = new List<BaseObject>();
			ImplementedClasses = new List<BaseObject>();
			WrongClasses = new List<WrongObjectPair>();
		}
		#endregion
	}
}
