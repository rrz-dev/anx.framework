#region Using Statements
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.ModernUI
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann")]
    public class MouseCreator : IMouseCreator
    {
        public string Name
        {
            get
            {
                return "ModernUI.Mouse";
            }
        }

        public int Priority
        {
            get
            {
                return 10;
            }
        }

        public IMouse CreateDevice()
        {
            return new Mouse();
        }


        public string Provider
        {
            get { return "XInput"; }
        }
    }
}
