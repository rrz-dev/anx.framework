using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class ListSerializer<T> : CollectionSerializerBase<List<T>, T>
    {
    }
}
