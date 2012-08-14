using System;
using System.IO;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroTitleContainer : INativeTitleContainer
	{
		public Stream OpenStream(string name)
		{
			throw new NotImplementedException();
		}

		public string GetCleanPath(string path)
		{
			// TODO
			return path;
		}
	}
}
