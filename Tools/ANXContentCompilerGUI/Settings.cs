using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

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
        public static List<String> RecentProjects { get; set; } 

        public static void Defaults()
        {
            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                         "ANX Content Compiler" + Path.DirectorySeparatorChar + "4.0" + Path.DirectorySeparatorChar);
            RecentProjects = new List<string>();
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
            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "ANX Content Compiler" + Path.DirectorySeparatorChar + "4.0" + Path.DirectorySeparatorChar);
            RecentProjects = new List<string>();
            if (!File.Exists(path)) return;
            XmlReader reader = new XmlTextReader(path);
            while (!reader.EOF)
            {
                switch (reader.Name)
                {
                    case "MainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            MainColor = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "DarkMainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            DarkMainColor = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "LightMainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            LightMainColor = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "ForeColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            ForeColor = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor2":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor2 = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor3":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor3 = GetColorFromXmlCompatible(reader.ReadElementContentAsString());
                        break;
                    case "Path":
                        if (reader.NodeType == XmlNodeType.Element)
                            RecentProjects.Add(reader.ReadElementContentAsString());
                        break;
                    default:
                        reader.Read();
                        break;
                }
            }
            reader.Close();
        }

        public static void Save(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            XmlWriter writer = new XmlTextWriter(path, Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("CCompiler4Settings");
            writer.WriteStartElement("MainColor");
            writer.WriteValue(GetXmlCompatibleColor(MainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("DarkMainColor");
            writer.WriteValue(GetXmlCompatibleColor(DarkMainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("LightMainColor");
            writer.WriteValue(GetXmlCompatibleColor(LightMainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("ForeColor");
            writer.WriteValue(GetXmlCompatibleColor(ForeColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor");
            writer.WriteValue(GetXmlCompatibleColor(AccentColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor2");
            writer.WriteValue(GetXmlCompatibleColor(AccentColor2));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor3");
            writer.WriteValue(GetXmlCompatibleColor(AccentColor3));
            writer.WriteFullEndElement();
            writer.WriteStartElement("RecentProjects");
            foreach (var recentProject in RecentProjects)
            {
                writer.WriteStartElement("Path");
                writer.WriteString(recentProject);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private static string GetXmlCompatibleColor(Color color)
        {
            return color.R + "," + color.G + "," + color.B;
        }

        private static Color GetColorFromXmlCompatible(string xml)
        {
            var s = xml.Split(new[] {','}, 3);
            return Color.FromArgb(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), Convert.ToInt32(s[2]));
        }
    }
}
