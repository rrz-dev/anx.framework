using System;
using System.Collections.Generic;
using ANXStatusComparer.Data;

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
