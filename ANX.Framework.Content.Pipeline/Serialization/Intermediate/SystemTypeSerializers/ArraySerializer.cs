#region Using Statements
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [Developer("KorsarNek")]
    class ArraySerializer<T> : CollectionSerializerBase<T[], T>
    {
        protected override T[] Deserialize(IntermediateReader input, ContentSerializerAttribute format, T[] existingInstance)
        {
            List<T> list = new List<T>();
            base.Deserialize(input, format, list);
            return list.ToArray();
        }
    }
}
