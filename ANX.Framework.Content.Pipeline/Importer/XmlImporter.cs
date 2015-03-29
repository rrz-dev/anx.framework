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
using ANX.Framework.Content.Pipeline.Serialization.Intermediate;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new[] { ".xml" })]
    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public class XmlImporter : ContentImporter<object>
    {
        private XDocument _doc;
        public override object Import(string filename, ContentImporterContext context)
        {
            using (XmlReader xmlReader = XmlReader.Create(filename))
            {
                return IntermediateSerializer.Deserialize<object>(xmlReader, filename);
            }
        }
    }
}
