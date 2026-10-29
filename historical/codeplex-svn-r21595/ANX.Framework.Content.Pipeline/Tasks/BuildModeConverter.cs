using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA.Development;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    /// <summary>
    /// Class for enabling a dropdown list containing all available Processors for the PropertyGrid.
    /// </summary>
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class BuildModeConverter : StringConverter
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
            var buildModes = new List<string> {"Debug", "Release"};
            return new StandardValuesCollection(buildModes);
        }
    }
}
