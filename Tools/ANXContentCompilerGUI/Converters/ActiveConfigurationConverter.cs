using ANX.ContentCompiler.GUI.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Converters
{
    class ActiveConfigurationConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var contentProject = ((ContentProjectNodeProperties)context.Instance).ContentProject;

            return new StandardValuesCollection(contentProject.Configurations.GetUniqueNames());
        }
    }
}
