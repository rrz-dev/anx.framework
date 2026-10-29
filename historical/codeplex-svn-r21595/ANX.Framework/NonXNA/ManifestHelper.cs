using System;
using System.IO;
using ANX.Framework.NonXNA.Reflection;

namespace ANX.Framework.NonXNA
{
	internal static class ManifestHelper
	{
		public static Stream GetManifestResourceStream(Game game, string name)
		{
			return TypeHelper.GetAssemblyFrom(game.GetType()).GetManifestResourceStream(name);
		}
	}
}
