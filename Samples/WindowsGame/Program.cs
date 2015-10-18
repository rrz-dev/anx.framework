using System;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();

            AddInSystemFactory.Instance.Initialize();

            AddInSelector selector = new AddInSelector();
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, selector.cbRenderSystem.Text);
                AddInSystemFactory.Instance.SetPreferredSystem(AddInType.SoundSystem, selector.cbAudioSystem.Text);
                InputDeviceFactory.Instance.PrefferedProvider = selector.cbInputSystem.Text;

                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }
        }
    }
}

