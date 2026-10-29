#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAGamePad = Microsoft.Xna.Framework.Input.GamePad;
using ANXGamePad = ANX.Framework.Input.GamePad;

using XNAGamePadState = Microsoft.Xna.Framework.Input.GamePadState;
using ANXGamePadState = ANX.Framework.Input.GamePadState;

using XNAGamePadDPad = Microsoft.Xna.Framework.Input.GamePadDPad;
using ANXGamePadDPad = ANX.Framework.Input.GamePadDPad;

using ANXButtons = ANX.Framework.Input.Buttons;
using XNAButtons = Microsoft.Xna.Framework.Input.Buttons;

using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using ANXButtonState = ANX.Framework.Input.ButtonState;

using XNAPlayerIndex = Microsoft.Xna.Framework.PlayerIndex;
using ANXPlayerIndex = ANX.Framework.PlayerIndex;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class GamePadTest
    {

        static object[] playergets =
        {
            new object[]{ANXPlayerIndex.One,new ANXGamePadState(new Vector2(100, 100), new Vector2(100, 100), 0.5f, 0.5f, ANXButtons.A, ANXButtons.B)},
            new object[]{ANXPlayerIndex.Two,new ANXGamePadState(new Vector2(200, 200), new Vector2(100, 100), 0.5f, 0.5f, ANXButtons.A, ANXButtons.BigButton)},
            new object[]{ ANXPlayerIndex.Three, new ANXGamePadState(new Vector2(100, 100), new Vector2(100, 100), 0.5f, 0.5f, ANXButtons.A, ANXButtons.X)},
            new object[]{ ANXPlayerIndex.Four,new ANXGamePadState()},
        };
        static object[] playervib =
        {
            new object[]{ANXPlayerIndex.One,0.5f,0.5f,true},
            new object[]{ANXPlayerIndex.Two,0.7f,0.5f,true},
            new object[]{ ANXPlayerIndex.Three,-0.5f,0.7f,true},
            new object[]{ ANXPlayerIndex.Four,0.5f,0.5f,false},
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

        [TestCaseSource("playergets")]
        public void GetState(ANXPlayerIndex anxplayer, ANXGamePadState state)
        {

            ANXGamePadState anxstate = ANXGamePad.GetState(anxplayer);

            AssertHelper.ConvertEquals(true, anxstate == state, "GetState");

        }

        [TestCaseSource("playergets")]
        public void GetState2(ANXPlayerIndex anxplayer, ANXGamePadState state)
        {

            ANXGamePadState anxstate = ANXGamePad.GetState(anxplayer, Framework.Input.GamePadDeadZone.None);

            AssertHelper.ConvertEquals(true, anxstate == state, "GetState2");

        }
        [Test]
        public void GetCapabilities()
        {
            ANX.Framework.Input.GamePadCapabilities test = new Framework.Input.GamePadCapabilities();
            AssertHelper.ConvertEquals(true, ANXGamePad.GetCapabilities(PlayerIndex.One).Equals(test), "GetCapabilities");
        }
        [TestCaseSource("playervib")]
        public void SetVibration(ANXPlayerIndex anxplayer, float left, float right, bool result)
        {

            AssertHelper.ConvertEquals(result, ANXGamePad.SetVibration(anxplayer, left, right), "SetVibration");
        }
    }
}