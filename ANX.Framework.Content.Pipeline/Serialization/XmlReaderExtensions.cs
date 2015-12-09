using System;
using System.Collections.Generic;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization
{
    public static class XmlReaderExtensions
    {
        /// <summary>
        /// Checks if the current document contains the given element.
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <param name="name">Name of the element</param>
        /// <returns></returns>
        public static bool CheckForElement(this XmlReader xmlReader, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Element name can not be null!");
            }
            return xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == name;
        }

        public static IEnumerable<XmlNode> GetAsEnumerable(this XmlNodeList list)
        {
            for (int i = 0; i < list.Count; i++)
                yield return list[i];
        }
    }
}
