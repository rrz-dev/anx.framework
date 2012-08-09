using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeTitleContainer
	{
		Stream OpenStream(string name);
		
		string GetCleanPath(string path);
	}
}
