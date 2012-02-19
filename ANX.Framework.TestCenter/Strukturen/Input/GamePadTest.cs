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


#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
            AddInSystemFactory.Instance.SetPreferredSystem(AddInType.InputSystem, "Test");

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