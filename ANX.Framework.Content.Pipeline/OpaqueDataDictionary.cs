#region Using Statements
using ANX.Framework.Content.Pipeline.Serialization.Intermediate;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    [ContentSerializerCollectionItemName("Data")]
    [Serializable]
    public sealed class OpaqueDataDictionary : NamedValueDictionary<Object>
    {
        public OpaqueDataDictionary()
            : base()
        {
            // nothing to do
        }

        public OpaqueDataDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            object retValue = null;
            if (base.TryGetValue(key, out retValue))
            {
                if (retValue is T)
                {
                    return (T)retValue;
                }
            }

            return defaultValue;
        }

        public string GetContentAsXml()
        {
            if (Count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(result))
            {
                IntermediateSerializer.Serialize(writer, this, null);
            }

            return result.ToString();
        }
    }
}
