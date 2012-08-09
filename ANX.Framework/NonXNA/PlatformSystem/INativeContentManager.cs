using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeContentManager
	{
		string MakeRootDirectoryAbsolute(string relativePath);

		Stream OpenStream(string filepath);
	}
}
