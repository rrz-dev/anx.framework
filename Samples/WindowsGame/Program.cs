using System;
using ANX.Framework.NonXNA;
using System.Windows.Forms;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            AddInSystemFactory.Instance.Initialize();

            AddInSelector selector = new AddInSelector();
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Game1 game = new Game1(selector.cbRenderSystem.Text, selector.cbInputSystems.CheckedItems[0].ToString(), selector.cbAudioSystem.Text))
                {
                    game.Run();
                }
            }
        }
    }
#endif
}

