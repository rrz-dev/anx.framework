using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnxSampleBrowser
{
    class DropDownElement
    {
        public string Name
        {
            get;
            private set;
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public DropDownElement(string name, string displayName)
        {
            this.Name = name;
            this.DisplayName = displayName;
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
