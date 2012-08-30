using System;
using System.Threading;

namespace ANX.Framework.NonXNA
{
	internal static class ThreadHelper
	{
		public static void Sleep(int milliseconds)
		{
#if WINDOWSMETRO
			// TODO: search replacement
#else
			Thread.Sleep(milliseconds);
#endif
		}
	}
}
