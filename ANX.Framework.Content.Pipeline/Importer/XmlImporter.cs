using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ANX.Framework.Content.Pipeline.Serialization;
using ANX.Framework.NonXNA.Development;

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter("ANX XML Importer", ".xml")]
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
                    throw new InvalidContentException("The given XML file does not contain Xna or Anx readable content! Did you forget the \"<XnaContent>\" or \"<AnxContent>\"?");

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
            xml.ReadStartElement("Asset");
            xml.MoveToAttribute("Type");
            logger.LogMessage("Trying to read a type from the Xml file");
            var type = Type.GetType(xml.ReadContentAsString());
            if (type == null)
                throw new InvalidContentException("Error during deserialization: Type is null. Is there a valid Type attribute in the Asset section of your Xml file?");
            logger.LogImportantMessage("Type is" + type);
            return type;
        }

        private object ReadObject(XmlReader reader, Type type, ContentBuildLogger logger)
        {
            //TODO: Get all other elements from the xml file and add them to a dictionary.
            var props = new Dictionary<string, object>();
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (!reader.HasValue && !reader.IsEmptyElement) //we have an element without value and that is not empty, that's suspicious!
                    {
                        //Is it really empty? Maybe it's a list or a dictionary?
                        System.Diagnostics.Debugger.Break();
                    }
                    else
                        props.Add(reader.Name, reader.ReadElementContentAsObject());
                }
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
    }
}
