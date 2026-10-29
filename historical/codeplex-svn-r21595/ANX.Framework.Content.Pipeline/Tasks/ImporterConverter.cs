using System.Collections.Specialized;
using System.Linq;
using System.ComponentModel;
using ANX.Framework.NonXNA.Development;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    /// <summary>
    /// Class for enabling a dropdown list containing all available Importers for the PropertyGrid.
    /// </summary>
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
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
            var iManager = new ImporterManager(); //<- No, thats not a new apple product :-)
            return new StandardValuesCollection(iManager.AvailableImporters.Select(availableImporter => availableImporter.Key).ToArray()); //TODO: Implement correct call to get available Importers
        }
    }
}
