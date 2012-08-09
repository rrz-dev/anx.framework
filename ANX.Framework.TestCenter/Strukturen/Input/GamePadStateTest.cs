#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
using ANX.Framework; 
#endregion // Using Statements

#region Using TestStatements
using XNAGamePadButtons = Microsoft.Xna.Framework.Input.GamePadButtons;
using ANXGamePadButtons = ANX.Framework.Input.GamePadButtons;

using XNAButtons = Microsoft.Xna.Framework.Input.Buttons;
using ANXButtons = ANX.Framework.Input.Buttons;

using XNAGamePadDPad = Microsoft.Xna.Framework.Input.GamePadDPad;
using ANXGamePadDPad = ANX.Framework.Input.GamePadDPad;

using XNAGamePadThumbSticks = Microsoft.Xna.Framework.Input.GamePadThumbSticks;
using ANXGamePadThumbSticks = ANX.Framework.Input.GamePadThumbSticks;

using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using ANXButtonState = ANX.Framework.Input.ButtonState;

using ANXGamePadTriggers = ANX.Framework.Input.GamePadTriggers;
using XNAGamePadTriggers = Microsoft.Xna.Framework.Input.GamePadTriggers;

using ANXGamePadState = ANX.Framework.Input.GamePadState;
using XNAGamePadState = Microsoft.Xna.Framework.Input.GamePadState;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class GamePadStateTest
    {
        #region Testdata
        static object[] Stats16 =
        {
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] {DataFactory.RandomIntValueMinMax(0,32769),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000),DataFactory.RandomValueMinMax(float.Epsilon, 1000), ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Pressed},
        };
        #endregion

        [TestCaseSource("Stats16")]
        public void IsButtonDown(int i1,float f1, float f2, float f3, float f4, float f5, float f6, ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            XNAGamePadState xna = new XNAGamePadState(new XNAGamePadThumbSticks(new XNAVector2(f1,f2),new XNAVector2(f3,f4)),new XNAGamePadTriggers(f5,f6),new XNAGamePadButtons((XNAButtons)i1),new XNAGamePadDPad(upValue2,downValue2,leftValue2,rightValue2));
            ANXGamePadState anx = new ANXGamePadState(new ANXGamePadThumbSticks(new ANXVector2(f1, f2), new ANXVector2(f3, f4)), new ANXGamePadTriggers(f5, f6), new ANXGamePadButtons((ANXButtons)i1), new ANXGamePadDPad(upValue, downValue, leftValue, rightValue));

            AssertHelper.ConvertEquals(xna.IsButtonDown(XNAButtons.A), anx.IsButtonDown(ANXButtons.A), "IsButtonDown");
        }
        [TestCaseSource("Stats16")]
        public void IsButtonUp(int i1, float f1, float f2, float f3, float f4, float f5, float f6, ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            XNAGamePadState xna = new XNAGamePadState(new XNAGamePadThumbSticks(new XNAVector2(f1, f2), new XNAVector2(f3, f4)), new XNAGamePadTriggers(f5, f6), new XNAGamePadButtons((XNAButtons)i1), new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2));
            ANXGamePadState anx = new ANXGamePadState(new ANXGamePadThumbSticks(new ANXVector2(f1, f2), new ANXVector2(f3, f4)), new ANXGamePadTriggers(f5, f6), new ANXGamePadButtons((ANXButtons)i1), new ANXGamePadDPad(upValue, downValue, leftValue, rightValue));

            AssertHelper.ConvertEquals(xna.IsButtonUp(XNAButtons.A), anx.IsButtonUp(ANXButtons.A), "IsButtonUp");
        }
        [TestCaseSource("Stats16")]
        public void IsConnected(int i1, float f1, float f2, float f3, float f4, float f5, float f6, ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            XNAGamePadState xna = new XNAGamePadState(new XNAGamePadThumbSticks(new XNAVector2(f1, f2), new XNAVector2(f3, f4)), new XNAGamePadTriggers(f5, f6), new XNAGamePadButtons((XNAButtons)i1), new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2));
            ANXGamePadState anx = new ANXGamePadState(new ANXGamePadThumbSticks(new ANXVector2(f1, f2), new ANXVector2(f3, f4)), new ANXGamePadTriggers(f5, f6), new ANXGamePadButtons((ANXButtons)i1), new ANXGamePadDPad(upValue, downValue, leftValue, rightValue));

            AssertHelper.ConvertEquals(xna.IsConnected, anx.IsConnected, "IsConnected");
        }
        [Test]
        public void IsConnected2()
        {
            XNAGamePadState xna = new XNAGamePadState();
            ANXGamePadState anx = new ANXGamePadState();

            AssertHelper.ConvertEquals(xna.IsConnected, anx.IsConnected, "IsConnected2");
        }
    }
}
