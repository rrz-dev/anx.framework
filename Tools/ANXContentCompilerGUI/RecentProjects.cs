using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class RecentProjects : List<String>
    {
        private static readonly string Path =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            System.IO.Path.DirectorySeparatorChar + "ANX Content Compiler" + System.IO.Path.DirectorySeparatorChar +
            "recentProjects.ees";

        public RecentProjects()
            : base(10)
        {
        }

        public void Save()
        {
            XmlWriter writer = XmlWriter.Create(Path,
                                                new XmlWriterSettings
                                                    {
                                                        Encoding = Encoding.UTF8,
                                                        Indent = true,
                                                        NewLineHandling = NewLineHandling.Entitize
                                                    });
            writer.WriteStartDocument();
            writer.WriteStartElement("RecentProjects");
            foreach (string project in this)
            {
                writer.WriteStartElement("ContentProject");
                writer.WriteValue(project);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public static RecentProjects Load()
        {
            var instance = new RecentProjects();
            if (File.Exists(Path))
            {
                XmlReader reader = XmlReader.Create(Path);
                while (!reader.EOF)
                {
                    if (reader.Name == "ContentProject")
                        instance.Add(reader.ReadElementContentAsString());
                    reader.Read();
                }
                reader.Close();
            }
            return instance;
        }
    }
}