using System;
using ANX.Framework.NonXNA;

namespace ModelSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
		{
			AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX10");
			//AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX11");
			//AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "OpenGL3");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

