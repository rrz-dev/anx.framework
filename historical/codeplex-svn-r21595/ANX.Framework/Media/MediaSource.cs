using System.Collections.Generic;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class MediaSource
	{
	    public string Name { get; private set; }
	    public MediaSourceType MediaSourceType { get; private set; }

		internal MediaSource(string setName, MediaSourceType setType)
		{
			Name = setName;
			MediaSourceType = setType;
		}

		public static IList<MediaSource> GetAvailableMediaSources()
		{
			return PlatformSystem.Instance.GetAvailableMediaSources();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
