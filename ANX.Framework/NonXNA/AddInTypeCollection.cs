using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	internal class AddInTypeCollection
	{
		#region Public
		#region PreferredName
		public string PreferredName
		{
			get;
			set;
		}
		#endregion

		#region PreferredLocked
		public bool PreferredLocked
		{
			get;
			set;
		}
		#endregion

		#region AvailableSystems
		public List<AddIn> AvailableSystems
		{
			get;
			private set;
		}
		#endregion
		#endregion

		#region Constructor
		public AddInTypeCollection()
		{
			AvailableSystems = new List<AddIn>();
		}
		#endregion
	}
}
