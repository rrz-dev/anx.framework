using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.Development
{
	public class PercentageCompleteAttribute : Attribute
	{
		#region Public
		public int Percentage
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new percentage complete attribute.
		/// </summary>
		/// <param name="setPercentage">The percentage [0-100] value defining how
		/// "complete" the class is.</param>
		public PercentageCompleteAttribute(int setPercentage)
		{
			Percentage = setPercentage;
		}
		#endregion
	}
}
