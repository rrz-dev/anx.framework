using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.MediaSystem.PsVita
{
	public class Creator : IMediaSystemCreator
	{
		#region Public
		#region Name
		public string Name
		{
			get
			{
				return "Media.PsVita";
			}
		}
		#endregion

		#region Priority
		public int Priority
		{
			get
			{
				return 10;
			}
		}
		#endregion

		#region IsSupported
		public bool IsSupported
		{
			get
			{
				return OSInformation.GetName() == PlatformName.PSVita;
			}
		}
		#endregion
		#endregion
	}
}
