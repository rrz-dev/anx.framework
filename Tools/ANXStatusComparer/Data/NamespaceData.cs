using System;
using System.Collections.Generic;

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
