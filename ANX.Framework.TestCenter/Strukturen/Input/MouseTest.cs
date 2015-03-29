#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using ANXButtonState = ANX.Framework.Input.ButtonState;

using XNAMouseState = Microsoft.Xna.Framework.Input.MouseState;
using ANXMouseState = ANX.Framework.Input.MouseState;

using ANXMouse = ANX.Framework.Input.Mouse;



// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class MouseTest
    {
        static object[] twoInt =
        {
           new int[]{DataFactory.RandomBitPlus,DataFactory.RandomBitPlus},
           new int[]{0,0}
        };

        [TestFixtureSetUp]
        public void Setup()
        {
            AddInSystemFactory.Instance.Initialize();
            if (AddInSystemFactory.Instance.GetPreferredSystem(AddInType.InputSystem) == null)
            {
                AddInSystemFactory.Instance.SetPreferredSystem(AddInType.InputSystem, "Test");
            }
        }

        [TestCaseSource("twoInt")]
        public void GetState(int x, int y)
        {
            ANXMouse.SetPosition(x, y);
            AssertHelper.ConvertEquals(new ANXMouseState(x, y, 0, ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released), ANXMouse.GetState(), "GetState");

        }
    }
}
