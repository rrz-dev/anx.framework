using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.Development
{
	public class DeveloperAttribute : Attribute
	{
		#region Public
		public string Developer
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new developer attribute.
		/// </summary>
        /// <param name="developer">Developer who is responsible for this class</param>
		public DeveloperAttribute(string developer)
		{
			Developer = developer;
		}
		#endregion
	}
}
