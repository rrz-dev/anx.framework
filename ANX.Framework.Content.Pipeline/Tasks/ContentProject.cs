#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    //comment by author: if you find any mistakes in my language, go fix them ;-)
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public class ContentProject
    {
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
        public int VersionMinor { get { return 2; } } //before you commit your changes, please increase this value by one (and if you added stuff, please check the version before you read anything out of a file).

        /// <summary>
        /// The directory where the compiled output will be placed
        /// </summary>
        public String OutputDirectory { get; set; }

        /// <summary>
        /// The Source root directory where the majority of files is located in.
        /// </summary>
        public String InputDirectory { get; set; }

        /// <summary>
        /// The Content Root Directory. Default value is "Content".
        /// </summary>
        public String ContentRoot { get; set; }

        /// <summary>
        /// A list containing all build items of this project
        /// </summary>
        public List<BuildItem> BuildItems { get; private set; }

        /// <summary>
        /// A custom directory to look for custom importers/processors
        /// </summary>
        public String ReferenceIncludeDirectory { get; set; }

        /// <summary>
        /// List which holds Assemblies that contain custom importers/processors
        /// </summary>
        public List<String> References { get; set; }

        /// <summary>
        /// Name of the tool that generated the resulting file. Might be useful for tracking down
        /// bugs in that particular application.
        /// </summary>
        public String Creator { get; set; }

        /// <summary>
        /// Target Graphics Profile
        /// </summary>
        public GraphicsProfile Profile { get; set; }

        /// <summary>
        /// The configuration. Can be "Debug" or "Release".
        /// </summary>
        [TypeConverter(typeof(BuildModeConverter))]
        public String Configuration { get; set; }

        /// <summary>
        /// The platform the content will be compiled for.
        /// </summary>
        public TargetPlatform Platform { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of a <see cref="ContentProject"/>
        /// </summary>
        /// <param name="name">Name of the project</param>
        public ContentProject(String name)
        {
            Name = name;
            OutputDirectory = "bin";
            BuildItems = new List<BuildItem>();
            References = new List<string>();
        }

        #region Save
        /// <summary>
        /// Saves the project into a file. Should be a .cproj extension.
        /// </summary>
        /// <param name="path">The path to save the project</param>
        public void Save(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            XmlWriter writer = XmlTextWriter.Create(path, new XmlWriterSettings() {Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize} );
            writer.WriteStartDocument();

            // <ContentProject Version="4.0">
            writer.WriteStartElement("ContentProject"); 
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

            //<Configuration>Debug</Configuration>
            writer.WriteStartElement("Configuration");
            writer.WriteValue(Configuration);
            writer.WriteEndElement();

            //<Profile>Reach</Profile>
            writer.WriteStartElement("Profile");
            writer.WriteValue(Profile.ToString());
            writer.WriteEndElement();

            //<Platform>Windows</Platform>
            writer.WriteStartElement("Platform");
            writer.WriteValue(Platform.ToString());
            writer.WriteEndElement();

            //<OutputPath>A:\Somewhere</OutputPath>
            writer.WriteStartElement("OutputPath");
            writer.WriteValue(OutputDirectory);
            writer.WriteEndElement();

            //<InputPath>A:\Somewhere</InputPath>
            writer.WriteStartElement("InputPath");
            writer.WriteValue(InputDirectory);
            writer.WriteEndElement();

            //<ContentRoot>Content</ContentRoot>
            writer.WriteStartElement("ContentRoot");
            writer.WriteValue(ContentRoot);
            writer.WriteEndElement();

            //<References IncludeDir="B:\Pipeline">
            //  <Reference>ANX.Framework.Content.Pipeline.SomewhatImporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=blah, ProcessorArch=MSIL</Reference>
            //</References>
            writer.WriteStartElement("References");
            writer.WriteStartAttribute("IncludeDir");
            writer.WriteString(ReferenceIncludeDirectory);
            writer.WriteEndAttribute();
            foreach (var reference in References)
            {
                writer.WriteStartElement("Reference");
                writer.WriteValue(reference);
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
            //</BuildItems
            writer.WriteStartElement("BuildItems");
            foreach (var buildItem in BuildItems)
            {
                writer.WriteStartElement("BuildItem");
                writer.WriteStartAttribute("AssetName");
                writer.WriteValue(buildItem.AssetName);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("OutputFilename");
                writer.WriteValue(buildItem.OutputFilename);
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
        }
        #endregion

        #region Load
        private static BuildItem lastBuildItem = null;
        public static ContentProject Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("The content project you tried to load does not exist: ", path);

            var reader = XmlTextReader.Create(path);
            ContentProject project = null;
            String creator = null;
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
                    case "Configuration":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                project.Configuration = reader.ReadElementContentAsString();
                        break;
                    case "Profile":
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (versionMinor < 2)
                                project.Profile = GraphicsProfile.Reach;
                            else
                            {
                                string profileElement = reader.ReadElementContentAsString();
                                GraphicsProfile profile;
                                if (Enum.TryParse<GraphicsProfile>(profileElement, true, out profile))
                                {
                                    project.Profile = profile;
                                }
                            }
                        }
                        break;
                    case "Platform":
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (versionMajor == 1 && versionMinor >= 0)
                            {
                                string platformElement = reader.ReadElementContentAsString();
                                TargetPlatform targetPlatform;
                                if (Enum.TryParse<TargetPlatform>(platformElement, true, out targetPlatform))
                                {
                                    project.Platform = targetPlatform;
                                }
                            }
                        }
                        break;
                    case "OutputPath":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                project.OutputDirectory = reader.ReadElementContentAsString();
                        break;
                    case "InputPath":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                project.InputDirectory = reader.ReadElementContentAsString();
                        break;
                    case "ContentRoot":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                project.ContentRoot = reader.ReadElementContentAsString();
                        break;
                    case "IncludeDir":
                        if (reader.NodeType == XmlNodeType.Attribute)
                            project.ReferenceIncludeDirectory = reader.ReadContentAsString();
                        break;
                    case "Reference":
                        if (reader.NodeType == XmlNodeType.Element)
                            if (versionMajor == 1 && versionMinor >= 0)
                                project.References.Add(reader.ReadElementContentAsString());
                        break;
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
                                    case "OutputFilename":
                                        buildItem.OutputFilename = reader.ReadContentAsString();
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
                                    case "":
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
                    case "ProcessorParameter":
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
            //Check for features that were added in format version 1.1
            if (project.InputDirectory == null)
                project.InputDirectory = "";
            if (project.ReferenceIncludeDirectory == null)
                project.ReferenceIncludeDirectory = "";
            
            return project;
        }
        #endregion

    }
}
