using System;
using System.Collections.Generic;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class MediaSource
	{
		#region Public
		public string Name
		{
			get;
			private set;
		}

		public MediaSourceType MediaSourceType
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		internal MediaSource(string setName, MediaSourceType setType)
		{
			Name = setName;
			MediaSourceType = setType;
		}
		#endregion

		#region Constructor
		public static IList<MediaSource> GetAvailableMediaSources()
		{
			return PlatformSystem.Instance.GetAvailableMediaSources();
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return Name;
		}
		#endregion
	}
}
