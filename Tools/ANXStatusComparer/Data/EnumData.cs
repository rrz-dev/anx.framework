using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANXStatusComparer.Data
{
	public class EnumData
	{
		#region Public
		/// <summary>
		/// The type of the enumeration.
		/// </summary>
		public Type Handle
		{
			get;
			private set;
		}

		public List<string> Names
		{
			get;
			private set;
		}

		public string[] Values
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new enumeration data holder.
		/// </summary>
		/// <param name="setType">Type of the enumeration.</param>
		public EnumData(Type setType)
		{
			Handle = setType;
			Array enumValues = Enum.GetValues(Handle);
			Type underlyingType = Enum.GetUnderlyingType(Handle);

			Values = new string[enumValues.Length];
			Names = new List<string>();
			for (int index = 0; index < Values.Length; index++)
			{
				object value = enumValues.GetValue(index);
				Names.Add(value.ToString());
				Values[index] = Convert.ChangeType(value, underlyingType).ToString();
			}
		}
		#endregion
	}
}
