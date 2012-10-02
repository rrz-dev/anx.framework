using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using ANX.Framework.Content.Pipeline.Serialization;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.Reflection;

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new string[] {".xml"})]
    [Developer("SilentWarrior / Eagle Eye Studios")]
    [PercentageComplete(50)]
    public class XmlImporter : ContentImporter<object>
    {
        private XDocument _doc;
        public override object Import(string filename, ContentImporterContext context)
        {
            object result;
            context.Logger.LogMessage("Checking if file exists.");
            if (!File.Exists(filename))
                throw new InvalidContentException("The XML file \"" + filename + "\" could not be found.");

            context.Logger.LogMessage("Starting analysis of the XML file.");
            using (XmlReader xml = XmlReader.Create(filename))
            {
                _doc = XDocument.Load(filename);
                //Check if XML contains XnaContent or AnxContent root element
                context.Logger.LogMessage("Checking for root element.");
                var tag = _doc.Element("XnaContent");
                if (tag == null)
                {
                    tag = _doc.Element("AnxContent");
                    if (tag == null)
                        throw new InvalidContentException(
                            "The Xml file does not contain a valid XnaContent or AnxContent tag.");
                }
                //Check which type is beeing described in the XML by grabbing the Asset node
                var assetNode = _doc.Descendants("Asset").ToArray()[0];
                if (assetNode == null)
                    throw new InvalidContentException("Xml document does not contain an asset definition.");
                Type type = null;
                foreach (var attrib in assetNode.Attributes().Where(attrib => attrib.Name == "Type"))
                {
                    type = GetType(attrib.Value, context.Logger);
                }
                if (type == null)
                    throw new InvalidContentException("There is no assembly within the search path that contains such type.");
                //Create an instance of that type and fill it with the appropriate stuff
                result = ReadObject(type, context.Logger);
            }
            return result;
        }

        private Type GetType(string typeString, ContentBuildLogger logger)
        {
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
                        if (type.FullName == typeString)
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
            return null;
        }

        private object ReadObject(Type type, ContentBuildLogger logger)
        {
            var props = new Dictionary<string, object>();

            var assetNode = _doc.Descendants("Asset").ToArray()[0]; //Get first node again, can not be null cause we checked it before
            var nodes = assetNode.Descendants();

            foreach (var xElement in nodes.Where(xElement => xElement.Name != "Item" && xElement.Name != "Key" && xElement.Name != "Value")) //Reserved keywords
            {
                if (!xElement.HasElements) //Normal node
                {
                    if (xElement.HasAttributes) //Not a string, we need to convert.
                    {
                        string key = xElement.Name.LocalName;
                        object value = XmlContentToObject(xElement.Value);
                        props.Add(key, value);
                    }
                    else //String, just pass it.
                    {
                        string key = xElement.Name.LocalName;
                        object value = xElement.Value;
                        props.Add(key, value);
                    }
                }
                else //List or Dictionary
                {
                    if (xElement.Descendants().Contains(new XElement("Item")))
                    {
                        if (xElement.Descendants("Item").ToArray()[0].Descendants().Contains(new XElement("Key")) && xElement.Descendants("Item").ToArray()[0].Descendants().Contains(new XElement("Value"))) // is a dictionary
                        {
                            var dic = new Dictionary<string, object>();
                            foreach (var item in xElement.Descendants("Item"))
                            {
                                var key = item.Descendants("Key").ToArray()[0].Value;
                                object value = XmlContentToObject(item.Descendants("Value").ToArray()[0].Value);
                                dic.Add(key, value);
                            }
                            props.Add(xElement.Name.LocalName, dic);
                        }
                        else //must be a list
                        {
                            var list = xElement.Descendants("Item").Select(item => XmlContentToObject(item.Value)).ToList();
                            props.Add(xElement.Name.LocalName, list);
                        }
                    }
                }
            }
            //TODO: Get all fields of the class via reflection
            //Activate instance
            var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var result = constructors[0].Invoke(new object[] {});

            //TODO: Match every property with its value
            var properties = type.GetFields(BindingFlags.Instance | BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var property in properties)
            {
                var attribs = property.GetCustomAttributes(typeof (ContentSerializerAttribute), true);
                if (attribs.Length == 0)
                    continue;
                if (props.ContainsKey(((ContentSerializerAttribute)attribs[0]).ElementName))
                {
                    property.SetValue(result, props[((ContentSerializerAttribute)attribs[0]).ElementName]);
                    props.Remove(((ContentSerializerAttribute)attribs[0]).ElementName);
                }
            }
            if (props.Count > 0)
            {
                logger.LogWarning("", null, "There are unset properties left!", result);
                Debugger.Break();
            }

            //Return the whole construct
            return result;
        }

        private object XmlContentToObject(string xmlContent)
        {
            Debugger.Break();
            throw new NotImplementedException();
        }

    }
}
