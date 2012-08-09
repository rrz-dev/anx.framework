using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.MediaSystem.Windows.OpenAL
{
	public class Creator : IMediaSystemCreator
	{
		#region Public
		#region Name
		public string Name
		{
			get
			{
				return "Media.OpenAL";
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
				//TODO: this is just a very basic version of test for support
				PlatformName os = OSInformation.GetName();
				return OSInformation.IsWindows ||
					os == PlatformName.Linux ||
					os == PlatformName.MacOSX;
			}
		}
		#endregion
		#endregion

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
		#endregion
	}
}
