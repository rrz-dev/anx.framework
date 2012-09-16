using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
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
        public static bool ShowFirstStartScreen { get; set; }

        public static void Defaults()
        {
            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                              "ANX Content Compiler" + Path.DirectorySeparatorChar + "4.0" +
                                              Path.DirectorySeparatorChar);
            RecentProjects = new List<string>();
            MainColor = Color.FromArgb(64, 64, 64);
            //MainColor = Color.Goldenrod;
            DarkMainColor = Color.FromArgb(42, 42, 42);
            //DarkMainColor = Color.DarkOrange;
            LightMainColor = Color.Gray;
            //LightMainColor = Color.Gold;
            ForeColor = Color.White;
            //ForeColor = Color.DarkRed;
            AccentColor = Color.FromArgb(0, 192, 0);
            //AccentColor = Color.HotPink;
            AccentColor2 = Color.LimeGreen;
            //AccentColor2 = Color.IndianRed;
            AccentColor3 = Color.Green;
        }

        public static void Load(string path)
        {
            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                              "ANX Content Compiler" + Path.DirectorySeparatorChar + "4.0" +
                                              Path.DirectorySeparatorChar);
            RecentProjects = new List<string>();
            if (!File.Exists(path)) return;
            XmlReader reader = new XmlTextReader(path);
            while (!reader.EOF)
            {
                switch (reader.Name)
                {
                    case "MainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            MainColor = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "DarkMainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            DarkMainColor = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "LightMainColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            LightMainColor = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "ForeColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            ForeColor = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor2":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor2 = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "AccentColor3":
                        if (reader.NodeType == XmlNodeType.Element)
                            AccentColor3 = GetColorFromString(reader.ReadElementContentAsString());
                        break;
                    case "Path":
                        if (reader.NodeType == XmlNodeType.Element)
                            RecentProjects.Add(reader.ReadElementContentAsString());
                        break;
                    case "ShowFirstStartScreen":
                        if (reader.NodeType == XmlNodeType.Element)
                            ShowFirstStartScreen = reader.ReadElementContentAsBoolean();
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

            XmlWriter writer = XmlWriter.Create(path,
                                                new XmlWriterSettings
                                                    {
                                                        Encoding = Encoding.UTF8,
                                                        Indent = true,
                                                        NewLineHandling = NewLineHandling.Entitize
                                                    });
            writer.WriteStartDocument();
            writer.WriteStartElement("CCompiler4Settings");
            writer.WriteStartElement("MainColor");
            writer.WriteValue(GetStringFromColor(MainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("DarkMainColor");
            writer.WriteValue(GetStringFromColor(DarkMainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("LightMainColor");
            writer.WriteValue(GetStringFromColor(LightMainColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("ForeColor");
            writer.WriteValue(GetStringFromColor(ForeColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor");
            writer.WriteValue(GetStringFromColor(AccentColor));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor2");
            writer.WriteValue(GetStringFromColor(AccentColor2));
            writer.WriteFullEndElement();
            writer.WriteStartElement("AccentColor3");
            writer.WriteValue(GetStringFromColor(AccentColor3));
            writer.WriteFullEndElement();
            writer.WriteStartElement("RecentProjects");
            foreach (string recentProject in RecentProjects)
            {
                writer.WriteStartElement("Path");
                writer.WriteString(recentProject);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteStartElement("ShowFirstStartScreen");
            writer.WriteValue(ShowFirstStartScreen);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private static string GetStringFromColor(Color color)
        {
            return color.R + "," + color.G + "," + color.B;
        }

        private static Color GetColorFromString(string xml)
        {
            string[] s = xml.Split(new[] {','}, 3);
            return Color.FromArgb(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), Convert.ToInt32(s[2]));
        }
    }
}