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
    public sealed class ProcessorParameter
    {
        internal ProcessorParameter(string propertyName, Type propertyType)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }

            this.PropertyName = propertyName;
            this.PropertyType = propertyType.AssemblyQualifiedName;
        }

        
        public string PropertyName
        {
            get;
            private set;
        }

        
        public string DisplayName
        {
            get;
            internal set;
        }

        
        public string PropertyType
        {
            get;
            private set;
        }

        
        public object DefaultValue
        {
            get;
            internal set;
        }

        
        public string Description
        {
            get;
            internal set;
        }
    }
}
