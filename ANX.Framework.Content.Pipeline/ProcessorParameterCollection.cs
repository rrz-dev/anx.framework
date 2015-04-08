using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    [Developer("KorsarNek")]
    [Serializable]
    
    public sealed class ProcessorParameterCollection : ReadOnlyCollection<ProcessorParameter>
    {
        internal ProcessorParameterCollection(IEnumerable<ProcessorParameter> parameters)
            : base(new List<ProcessorParameter>(parameters))
        {

        }
    }
}
