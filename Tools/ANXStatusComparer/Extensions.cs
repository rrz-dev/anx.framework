using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ANXStatusComparer
{
    public static class Extensions
    {
        public static XElement Element(this XElement instance, string localName)
        {
            return instance.Elements().FirstOrDefault((x) => x.Name.LocalName == localName);
        }

        public static IEnumerable<XElement> Elements(this XElement instance, string localName)
        {
            return instance.Elements().Where((x) => x.Name.LocalName == localName);
        }
    }
}
