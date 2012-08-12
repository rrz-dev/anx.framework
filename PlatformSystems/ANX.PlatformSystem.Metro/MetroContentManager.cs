using System;
using System.IO;
using ANX.Framework.NonXNA.PlatformSystem;

namespace ANX.PlatformSystem.Metro
{
	public class MetroContentManager : INativeContentManager
	{
		public string MakeRootDirectoryAbsolute(string relativePath)
		{
			return relativePath;
		}

		public Stream OpenStream(string filepath)
		{
			return null;
		}
	}
}
