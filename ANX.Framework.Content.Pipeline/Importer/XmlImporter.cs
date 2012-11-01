#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.Reflection;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new[] {".xml"})]
    [Developer("SilentWarrior / Eagle Eye Studios")]
    [PercentageComplete(90)]
    [TestState(TestStateAttribute.TestState.InProgress)]
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
                    type = GetType(attrib.Value);
                }
                if (type == null)
                    throw new InvalidContentException("There is no assembly within the search path that contains such type.");
                context.Logger.LogImportantMessage("Type is " + type);
                //Create an instance of that type and fill it with the appropriate stuff
                result = ReadObject(type, context.Logger);
                context.Logger.LogMessage("XmlImporter has finished.");
            }
            return result;
        }

        private static Type GetType(string typeString)
        {
            //TODO: Implement custom assembly path checking
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
                            return type;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Debugger.Break(); //Go and check whats wrong
                }
            }
            Type t = Type.GetType(typeString);
            return t;
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
                    string key = xElement.Name.LocalName;
                    object value = XmlContentToObject(xElement);
                    props.Add(key, value);
                }
                else //List or Dictionary
                {
                    var theItem = xElement.Descendants("Item");
                    if (theItem.Any())
                    {
                        if (theItem.Descendants("Key").Any() && theItem.Descendants("Value").Any()) // is a dictionary
                        {
                            var dic = new Dictionary<string, object>();
                            foreach (var item in xElement.Descendants("Item"))
                            {
                                var key = item.Descendants("Key").ToArray()[0].Value;
                                object value = XmlContentToObject(item.Descendants("Value").ToArray()[0]);
                                dic.Add(key, value);
                            }
                            props.Add(xElement.Name.LocalName, dic);
                        }
                        else //must be a list
                        {
                            var list = xElement.Descendants("Item").Select(XmlContentToObject).ToList();
                            props.Add(xElement.Name.LocalName, list);
                        }
                    }
                }
            }
            //Activate instance
            var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var result = constructors[0].Invoke(new object[] {});

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

        public static object XmlContentToObject(XElement item)
        {
            if (!item.HasAttributes && !item.HasElements) // String
            {
                return item.Value;
            }
            else if (item.HasAttributes && !item.HasElements)
            {
                var stuffs = item.Attributes("Type").ToArray();
                string typeString = stuffs[0].Value;
                var type = GetType(typeString);
                if (type == typeof(Single))
                    return Convert.ToSingle(item.Value);
                if (type == typeof(Int32))
                    return Convert.ToInt32(item.Value);
                if (type == typeof(Int64))
                    return Convert.ToInt64(item.Value);
                if (type == typeof(Double))
                    return Convert.ToDouble(item.Value);
                if (type == typeof(Boolean))
                    return Convert.ToBoolean(item.Value);
                throw new NotSupportedException("Sorry, conversion of type \"" + type + "\" is currently not supported.");
            }
            throw new NotSupportedException("Conversion of nested stuff is not supported! If you have the time, go ahead and implement it! :P");
        }

    }
}
