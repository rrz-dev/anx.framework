using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using ANX.Framework.Content.Pipeline.Serialization;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.Reflection;

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new string[] {".xml"})]
    [Developer("SilentWarrior / Eagle Eye Studios")]
    [PercentageComplete(20)]
    public class XmlImporter : ContentImporter<object>
    {
        public override object Import(string filename, ContentImporterContext context)
        {
            object result;
            context.Logger.LogMessage("Checking if file exists.");
            if (!File.Exists(filename))
                throw new InvalidContentException("The XML file \"" + filename + "\" could not be found.");

            context.Logger.LogMessage("Starting analysis of the XML file.");
            using (XmlReader xml = XmlReader.Create(filename))
            {
                //Check if XML contains XnaContent or AnxContent root element
                context.Logger.LogMessage("Checking for root element.");
                if (!xml.CheckForElement("XnaContent") && !xml.CheckForElement("AnxContent"))
                    throw new InvalidContentException(
                        "The given XML file does not contain Xna or Anx readable content! Did you forget the \"<XnaContent>\" or \"<AnxContent>\"?");

                //Check which type is beeing described in the XML
                var type = GetType(xml, context.Logger);

                //Create an instance of that type and fill it with the appropriate stuff
                result = ReadObject(xml, type, context.Logger);
            }
            return result;
        }

        private Type GetType(XmlReader xml, ContentBuildLogger logger)
        {
            logger.LogMessage("Moving reader to type attribute of the first asset node.");
            xml.ReadStartElement();
            if (xml.MoveToContent() == XmlNodeType.Element && xml.Name == "Asset")
            {
                xml.MoveToAttribute("Type");
                logger.LogMessage("Trying to read a type from the Xml file");
                //Check every assembly in the current working dir for type
                foreach (
                    var file in
                        Directory.GetFiles(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "*.dll"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(file);
                        foreach (Type type in TypeHelper.SafelyExtractTypesFrom(assembly))
                        {
                            if (type.FullName == xml.ReadContentAsString())
                            {
                                logger.LogImportantMessage("Type is" + type);
                                return type;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debugger.Break(); //Go and check whats wrong
                    }
                }
            }
            throw new InvalidContentException(
                "Error during deserialization: Type is null. Is there a valid Type attribute in the Asset section of your Xml file and can the assembly conataining the type be found?");
        }

        private object ReadObject(XmlReader reader, Type type, ContentBuildLogger logger)
        {
            //TODO: Get all other elements from the xml file and add them to a dictionary.
            var props = new Dictionary<string, object>();
            string lastNode = "";
            bool isDict = false;
            Dictionary<string, object> dict = null;
            string attrib = "";
            while (!reader.EOF)
            {
                    if (reader.Name != "Item" && reader.Name != "Key" && reader.Name != "Value" && reader.NodeType == XmlNodeType.Element)
                    {
                        if (isDict)
                            isDict = false;
                        if (!reader.IsEmptyElement)
                        {
                            try
                            {
                                props.Add(reader.Name, reader.ReadElementContentAsObject());
                            }
                            catch (Exception ex)
                            {
                                logger.LogWarning("", null, "Could not add element to properties.", ex);
                            }
                        }
                    }
                    else
                    {
                        if (reader.Name == "Item" && reader.NodeType == XmlNodeType.Element)
                        {
                            if (!isDict)
                            {
                                isDict = true;
                                dict = new Dictionary<string, object>();
                                props.Add(lastNode, dict);
                            }
                            reader.ReadStartElement();
                            reader.ReadStartElement();
                            var key = reader.ReadContentAsString();
                            reader.ReadEndElement();
                            var typeBlah = ResolveType(attrib);
                            var value = reader.ReadContentAs(typeBlah, null);
                            reader.ReadEndElement();
                            dict.Add(key, value);
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "Type")
                            attrib = reader.ReadContentAsString();
                    }
                    lastNode = reader.Name;
                reader.Read();
            }

            System.Diagnostics.Debugger.Break();
            //TODO: Get all public properties of the class via reflection

            //Activate instance
            var result = Activator.CreateInstance(type);

            //TODO: Check if there is a pendant for every entry

            //TODO: Match every property with its value

            //Return the whole construct
            return result;
        }

        private Type ResolveType(string type)
        {
            return Type.GetType(type, true);
        }

    }
}
