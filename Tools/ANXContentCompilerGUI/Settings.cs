using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI
{
    public static class Settings
    {
        public static String DefaultProjectPath { get; set; }
        public static Color MainColor { get; set; }
        public static Color DarkMainColor { get; set; }
        public static Color LightMainColor { get; set; }
        public static Color ForeColor { get; set; }
        public static Color AccentColor { get; set; }
        public static Color AccentColor2 { get; set; }
        public static Color AccentColor3 { get; set; }

        public static void Defaults()
        {

            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                         "ANX Content Compiler" + Path.DirectorySeparatorChar + "4.0" + Path.DirectorySeparatorChar);
            MainColor = Color.FromArgb(64, 64, 64);
            //MainColor = Color.Goldenrod;
            DarkMainColor = Color.FromArgb(42, 42, 42);
            //DarkMainColor = Color.DarkOrange;
            LightMainColor = Color.Gray;
            //LightMainColor = Color.Gold;
            ForeColor = Color.White;
            //ForeColor = Color.DarkRed;
            AccentColor =  Color.FromArgb(0, 192, 0);
            //AccentColor = Color.HotPink;
            AccentColor2 = Color.LimeGreen;
            //AccentColor2 = Color.IndianRed;
            AccentColor3 = Color.Green;
        }

        public static void Load(string path)
        {
            
        }

        public static void Save(string path)
        {
            
        }
    }
}
