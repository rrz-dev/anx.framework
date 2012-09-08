using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace AnxSampleBrowser
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnxSampleBrowser());
        }
    }

    static class Style
    {
        public static System.Drawing.Color BG { get { return System.Drawing.Color.FromArgb(44, 44, 44); } }
        public static System.Drawing.Color FG { get { return System.Drawing.Color.FromArgb(64, 64, 64); } }
        public static System.Drawing.Color Text { get { return System.Drawing.Color.FromArgb(255, 255, 255); } }
        public static Font Font16 { get { return new System.Drawing.Font("Segoe", 16F); } }
        public static Font Font12 { get { return new System.Drawing.Font("Segoe",12F); } }
    }
}
