using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("AstrorEnales")]
	public static class TitleContainer
	{
		private static INativeTitleContainer nativeImplementation;

		static TitleContainer()
		{
            try
            {
                nativeImplementation = PlatformSystem.Instance.CreateTitleContainer();
            }
            catch (PlatformSystemInstanceException ex)
            {
                //TODO: error handling
            }
		}
		
		public static Stream OpenStream(string name)
		{
            if (nativeImplementation == null)
            {
                //TODO: error handling
                return null;
            }

			return nativeImplementation.OpenStream(name);
		}

		internal static string GetCleanPath(string path)
		{
            if (nativeImplementation == null)
            {
                //TODO: error handling
                return null;
            }

			return nativeImplementation.GetCleanPath(path);
		}
	}
}
