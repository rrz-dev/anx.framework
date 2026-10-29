using System;
using ANX.Framework.NonXNA.PlatformSystem;
using System.IO;

namespace ANX.PlatformSystem.Windows
{
	public class WindowsTitleContainer : INativeTitleContainer
	{
		#region OpenStream
		public Stream OpenStream(string name)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetCleanPath
		public string GetCleanPath(string path)
		{
			// TODO: implement
			return path;
		}
		#endregion
	}
}
