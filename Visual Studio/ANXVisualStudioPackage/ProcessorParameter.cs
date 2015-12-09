using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    [Serializable]
    public class ProcessorParameter
    {
        public ProcessorParameter(string propertyName, string displayName, string propertyType, string value, string defaultValue, string description)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.PropertyType = propertyType;
            this.DefaultValue = defaultValue;
            this.Description = description;
            this.Value = value;
        }

        public string PropertyName
        {
            get;
            private set;
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public string PropertyType
        {
            get;
            private set;
        }

        public string DefaultValue
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }
    }
}
