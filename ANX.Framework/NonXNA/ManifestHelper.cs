using System;
using System.IO;
using System.Reflection;

namespace ANX.Framework.NonXNA
{
	internal static class ManifestHelper
	{
		public static Stream GetManifestResourceStream(Game game, string name)
		{
			Type gameType = game.GetType();
#if WINDOWSMETRO
				return gameType.GetTypeInfo().Assembly.GetManifestResourceStream(name);
#else
				return gameType.Assembly.GetManifestResourceStream(name);
#endif
		}
	}
}
