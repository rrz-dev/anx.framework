using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class MediaSource
	{
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public MediaSourceType MediaSourceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IList<MediaSource> GetAvailableMediaSources()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}
	}
}
