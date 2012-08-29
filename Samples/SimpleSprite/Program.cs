#define USE_GL3

using System;
using ANX.Framework.NonXNA;

namespace WindowsGame1
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
#if USE_GL3
			AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "OpenGL3");
#else
			AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX10");
#endif

			using (Game1 game = new Game1())
			{
				game.Run();
			}
		}
	}
}

