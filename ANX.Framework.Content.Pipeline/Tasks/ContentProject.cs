#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    //comment by author: if you find any mistakes in my language, go fix it ;-)
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(70)]
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
        public int VersionMajor { get { return 1; } }

        /// <summary>
        /// Minor version of the project format.
        /// Used to keep backwards compatibility
        /// </summary>
        public int VersionMinor { get { return 0; } } //before you commit your changes, please increase this value by one (and if you added stuff, please check the version before you read anything out of a file).

        /// <summary>
        /// The directory where the compiled output will be placed
        /// </summary>
        public String OutputDirectory { get; set; }

        /// <summary>
        /// The Content Root Directory. Default value is "Content".
        /// </summary>
        public String ContentRoot { get; set; }

        /// <summary>
        /// A list containing all build items of this project
        /// </summary>
        public List<BuildItem> BuildItems { get; private set; }

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
        /// The configuration. Can be "Debug" or "Release".
        /// </summary>
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

            //<Platform>Windows</Platform>
            writer.WriteStartElement("Platform");
            writer.WriteValue(Platform.ToString());
            writer.WriteEndElement();

            //<OutputPath>A:\Somewhere</OutputPath>
            writer.WriteStartElement("OutputPath");
            writer.WriteValue(OutputDirectory);
            writer.WriteEndElement();

            //<ContentRoot>Content</ContentRoot>
            writer.WriteStartElement("ContentRoot");
            writer.WriteValue(ContentRoot);
            writer.WriteEndElement();

            //<References>
            //  <Reference>ANX.Framework.Content.Pipeline.SomewhatImporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=blah, ProcessorArch=MSIL</Reference>
            //</References>
            writer.WriteStartElement("References");
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
            }
            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
        }
        #endregion

        #region Load
        public static ContentProject Load(string path)
        {
            var project = new ContentProject("Blubb");
            //TODO: Implement loading mechanism
            return project;
        }
        #endregion

    }
}
