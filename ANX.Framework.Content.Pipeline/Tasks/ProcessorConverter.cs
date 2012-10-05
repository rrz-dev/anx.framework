using System.ComponentModel;
using System.Linq;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    /// <summary>
    /// Class for enabling a dropdown list containing all available Processors for the PropertyGrid.
    /// </summary>
    public class ProcessorConverter : StringConverter
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
            var pManager = new ProcessorManager();
            return new StandardValuesCollection(pManager.AvailableProcessors.Select(availableProcessor => availableProcessor.Key).ToArray());
        }
    }
}
