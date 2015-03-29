#region Using Statements
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
#endregion

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class ICollectionSerializer<T> : CollectionSerializerBase<ICollection<T>, T>
    {
        
    }
}
