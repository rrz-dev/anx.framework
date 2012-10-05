using System.Collections.Specialized;
using System.ComponentModel;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    /// <summary>
    /// Class for enabling a dropdown list containing all available Importers for the PropertyGrid.
    /// </summary>
    public class ImporterConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //Show comboBox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //Non editable list
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new StringCollection()); //TODO: Implement correct call to get available Importers
        }
    }
}
