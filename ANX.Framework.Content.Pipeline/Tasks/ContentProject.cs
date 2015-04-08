#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using ANX.Framework.NonXNA.Development;
using System.Runtime.Versioning;
using ANX.Framework.Content.Pipeline.Tasks.References;
using ANX.Framework.Graphics;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    //comment by author: if you find any mistakes in my language, go fix them ;-)
    [Serializable]
    public class ContentProject
    {
        public const string XmlNamespace = "https://anxframework.codeplex.com/AnxContentProject.xsd";

        #region Properties
        /// <summary>
        /// Name of the Content Project
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Major version of the project format
        /// </summary>
        [Browsable(false)]
        public int VersionMajor { get { return 1; } } 

        /// <summary>
        /// Minor version of the project format.
        /// Used to keep backwards compatibility
        /// </summary>
        [Browsable(false)]
        public int VersionMinor { get { return 3; } } //before you commit your changes, please increase this value by one (and if you added stuff, please check the version before you read anything out of a file).
        //And update the project template files, if necessary.

        /// <summary>
        /// A list containing all build items of this project
        /// </summary>
        public List<BuildItem> BuildItems { get; private set; }

        /// <summary>
        /// A list containing all configurations of this project.
        /// </summary>
        public ConfigurationCollection Configurations { get; private set; }

        /// <summary>
        /// List which holds Assemblies that contain custom importers/processors
        /// </summary>
        public List<Reference> References { get; private set; }

        /// <summary>
        /// Name of the tool that generated the resulting file. Might be useful for tracking down
        /// bugs in that particular application.
        /// </summary>
        public String Creator { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of a <see cref="ContentProject"/>
        /// </summary>
        /// <param name="name">Name of the project</param>
        public ContentProject(String name)
        {
            Name = name;
            Configurations = new ConfigurationCollection();
            BuildItems = new List<BuildItem>();
            References = new List<Reference>();
        }

        #region Save
        /// <summary>
        /// Saves the project into a file. Should be a .cproj extension.
        /// </summary>
        /// <param name="path">The path to save the project</param>
        public void Save(string path)
        {
            //TODO: check file extension and throw exception when wrong (or silently change it?)

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            //Write only in memory first, to make sure we don't lose the file if something went wrong while saving.
            using (var stream = new MemoryStream())
            {
                XmlWriter writer = XmlTextWriter.Create(stream, new XmlWriterSettings() { Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize, CloseOutput = false });
                writer.WriteStartDocument();

                // <ContentProject Version="4.0">
                writer.WriteStartElement("ContentProject", XmlNamespace);
                writer.WriteStartAttribute("Version");
                writer.WriteValue(VersionMajor + "." + VersionMinor);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("Creator");
                writer.WriteValue(Creator);
                writer.WriteEndAttribute();

                //<ProjectName>Name</ProjectName>
                writer.WriteStartElement("ProjectName");
                writer.WriteValue(Name);
                writer.WriteEndElement();

                writer.WriteStartElement("Configurations");
                foreach (var config in Configurations)
                {
                    //<Configuration Name="Debug" Platform="Windows">
                    writer.WriteStartElement("Configuration");

                    writer.WriteStartAttribute("Name");
                    writer.WriteString(config.Name);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("Platform");
                    writer.WriteValue(config.Platform.ToString());
                    writer.WriteEndAttribute();

                    //<Profile>Reach</Profile>
                    writer.WriteStartElement("Profile");
                    writer.WriteValue(config.Profile.ToString());
                    writer.WriteEndElement();

                    //<OutputPath>A:\Somewhere</OutputPath>
                    writer.WriteStartElement("OutputPath");
                    writer.WriteValue(config.OutputDirectory);
                    writer.WriteEndElement();

                    //<CompressContent>False</CompressContent>
                    writer.WriteStartElement("CompressContent");
                    writer.WriteValue(config.CompressContent);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                //<References IncludeDir="B:\Pipeline">
                //  <Reference>ANX.Framework.Content.Pipeline.SomewhatImporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=blah, ProcessorArch=MSIL</Reference>
                //</References>
                writer.WriteStartElement("References");
                foreach (var reference in References)
                {
                    if (reference is GACReference)
                        writer.WriteStartElement("AssemblyCacheReference");
                    else if (reference is AssemblyReference)
                        writer.WriteStartElement("AssemblyReference");
                    else if (reference is ProjectReference)
                        writer.WriteStartElement("ProjectReference");

                    reference.Write(writer);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                //<BuildItems>
                //  <BuildItem AssetName="Content/MyPicture" OutputFilename="Content/MyPicture.xnb" Importer="TextureImporter" Processor="TextureProcessor">
                //      <SourceFile>A:\MyPicture.png</SourceFile>
                //      <ProcessorParams>
                //          <Parameter Name="DoThis">True</Parameter>
                //      <ProcessorParams>
                //  </BuildItem>
                //</BuildItems>
                writer.WriteStartElement("BuildItems");
                foreach (var buildItem in BuildItems)
                {
                    writer.WriteStartElement("BuildItem");
                    writer.WriteStartAttribute("AssetName");
                    writer.WriteValue(buildItem.AssetName);
                    writer.WriteEndAttribute();
                    writer.WriteStartAttribute("Importer");
                    writer.WriteValue(buildItem.ImporterName);
                    writer.WriteEndAttribute();
                    writer.WriteStartAttribute("Processor");
                    writer.WriteValue(buildItem.ProcessorName);
                    writer.WriteEndAttribute();
                    writer.WriteStartElement("SourceFile");
                    writer.WriteValue(buildItem.SourceFilename);
                    writer.WriteEndElement();
                    writer.WriteStartElement("ProcessorParams");
                    foreach (var pair in buildItem.ProcessorParameters)
                    {
                        writer.WriteStartElement("Parameter");
                        writer.WriteStartAttribute("Name");
                        writer.WriteValue(pair.Key);
                        writer.WriteEndAttribute();
                        writer.WriteValue(pair.Value);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.Flush();
                writer.Close();

                using (FileStream file = new FileStream(path, FileMode.OpenOrCreate))
                {
                    file.Position = 0;
                    file.SetLength(0);
                    stream.WriteTo(file);
                    file.Flush();
                }
            }
        }
        #endregion

        #region Load
        public static ContentProject Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("The content project you tried to load does not exist: ", path);

            BuildItem lastBuildItem = null;
            var reader = XmlTextReader.Create(path);
            ContentProject project = null;
            String creator = null;
            Configuration currentConfig = new Configuration("Debug", TargetPlatform.Windows);
            int versionMajor = 0;
            int versionMinor = 0;
            while (!reader.EOF)
            {
                var readerName = reader.Name;
                switch (readerName)
                {
                    case "ProjectName":
                        project = new ContentProject(reader.ReadElementContentAsString());
                        break;
                    case "ContentProject":
                        reader.MoveToAttribute("Version");
                        if (reader.NodeType == XmlNodeType.Attribute)
                        {
                            versionMajor = Convert.ToInt32(reader.ReadContentAsString().Split('.')[0]);
                            versionMinor = Convert.ToInt32(reader.ReadContentAsString().Split('.')[1]);
                        }
                        break;
                    case "Creator":
                        if (reader.NodeType == XmlNodeType.Attribute)
                            creator = reader.ReadContentAsString();
                        break;
                        //<Configurations>
                    case "Configuration":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1)
                            {
                                string name;
                                string platform;
                                if (versionMinor < 3)
                                {
                                    name = reader.ReadElementContentAsString();
                                    platform = "Windows"; //With the current struct, we just can't load the platform the old way because Configuration is written before Platform.
                                }
                                else
                                {
                                    name = reader.GetAttribute("Name");
                                    platform = reader.GetAttribute("Platform");
                                }

                                if (!currentConfig.IsEmpty)
                                {
                                    project.Configurations.Add(currentConfig);
                                }

                                currentConfig = new Configuration(name, (TargetPlatform)Enum.Parse(typeof(TargetPlatform), platform, true));
                            }
                        break;
                    case "Profile":
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (versionMinor < 2)
                                currentConfig.Profile = GraphicsProfile.Reach;
                            else
                            {
                                string text = reader.ReadElementContentAsString();
                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    currentConfig.Profile = (GraphicsProfile)Enum.Parse(typeof(GraphicsProfile), text, true);
                                }
                                else
                                    currentConfig.Profile = GraphicsProfile.HiDef;
                            }
                        }
                        break;
                    case "OutputPath":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                currentConfig.OutputDirectory = reader.ReadElementContentAsString();
                        break;
                    case "CompressContent":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 3)
                                currentConfig.CompressContent = reader.ReadElementContentAsBoolean();
                        break;
                        //</Configurations>
                        //<References>
                    case "Reference":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1)
                            {
                                //From version 1.3 onwards, references are divided into different types depending on the element name.
                                if (versionMinor < 3)
                                {
                                    project.References.Add(new AssemblyReference() { AssemblyPath = reader.ReadElementContentAsString() });
                                }
                            }
                        break;
                    case "AssemblyReference":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 3)
                            {
                                Reference reference = new AssemblyReference();
                                reference.Load(reader);

                                project.References.Add(reference);
                            }
                        break;
                    case "ProjectReference":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 3)
                            {
                                Reference reference = new ProjectReference();
                                reference.Load(reader);

                                project.References.Add(reference);
                            }
                        break;
                    case "AssemblyCacheReference":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 3)
                            {
                                Reference reference = new GACReference();
                                reference.Load(reader);

                                project.References.Add(reference);
                            }
                        break;
                        //</References>
                    case "BuildItem":
                        if (versionMajor == 1 && versionMinor >= 0)
                        {
                            if (reader.NodeType != XmlNodeType.Element)
                                break;
                            var buildItem = new BuildItem();
                            lastBuildItem = buildItem;
                            reader.MoveToFirstAttribute();
                            while (reader.NodeType == XmlNodeType.Attribute)
                            {
                                switch (reader.Name)
                                {
                                    case "AssetName":
                                        buildItem.AssetName = reader.ReadContentAsString();
                                        reader.MoveToNextAttribute();
                                        break;
                                    case "Importer":
                                        buildItem.ImporterName = reader.ReadContentAsString();
                                        reader.MoveToNextAttribute();
                                        break;
                                    case "Processor":
                                        buildItem.ProcessorName = reader.ReadContentAsString();
                                        if (buildItem.AssetName == null)
                                            reader.MoveToNextAttribute();
                                        else
                                            reader.Read();
                                        break;
                                    default:
                                        reader.Read();
                                        break;
                                }
                            }
                            project.BuildItems.Add(buildItem);
                        }
                        break;
                    case "SourceFile":
                        if (versionMajor == 1 && versionMinor >= 0)
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                                lastBuildItem.SourceFilename = reader.ReadElementContentAsString();
                        }
                        break;
                    case "Parameter":
                        if (versionMajor == 1 && versionMinor >= 0)
                        {
                            string key;
                            object value;
                            reader.MoveToNextAttribute();
                            key = reader.ReadContentAsString();
                            reader.MoveToContent();
                            value = reader.ReadElementContentAsObject();
                            lastBuildItem.ProcessorParameters.Add(key, value);
                        }
                        break;
                }
                reader.Read();
            }
            reader.Close();

            project.Configurations.Add(currentConfig);

            return project;
        }
        #endregion

    }
}
