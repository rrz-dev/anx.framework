#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAKeyboardState = Microsoft.Xna.Framework.Input.KeyboardState;
using ANXKeyboardState = ANX.Framework.Input.KeyboardState;

using XNAKeys = Microsoft.Xna.Framework.Input.Keys;
using ANXKeys = ANX.Framework.Input.Keys;



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
    class KeyboardStateTest
    {
#region Input
               static object[] Key =
        {
         0, 8,9,13,19,20,21,25,27,28,
         29,
        32,
        33,
        34,
        35,
        36,
         37,
        38,
          39,
         40,
         41,
        42,
         43,
        44,
         45,
        46,
        47,
         48,
         49,
         50,
         51,
         52,
         53,
         54,
         55,
         56,
         57,
        65,
        66,
        67,
        68,
        69,
        70,
        71,
        72,
        73,
        74,
        75,
        76,
        77,
        78,
        79,
        80,
        81,
        82,
        83,
        84,
        85,
        86,
        87,
        88,
        89,
        90,
        91,
        92,
         93,
        95,
        96,
        97,
        98,
        99,
        100,
        101,
        102,
        103,
        104,
        105,
         106,
         107,
         108,
         109,
         110,
        111,
         112,
         113,
         114,
         115,
         116,
         117,
         118,
         119,
         120,
        121,
        122,
        123,
        124,
        125,
        126,
        127,
        128,
        129,
        130,
        131,
        132,
        133,
        134,
        135,
         144,
        145,
        160,
         161,
         162,
         163,
         164,
        165,
         166,
         167,
        168,
         169,
        170,
        171,
         172,
        173,
        174,
        175,
        176,
        177,
         178,
         179,
        180,
         181,
         182,
         183,
         186,
        187,
         188,
         189,
         190,
          191,
        192,
         202,
         203,
        219,
        220,
         221,
         222,
        223,
         226,
         229,
        242,
        243,
        244,
        246,
         247,
         248,
         249,
        250,
        251,
        253,
         254
        };

        static object[] Key10 =
        {
           new int[]{9,32,33,34,35,36,37,38,39,40},
           new int[]{9,9,9,34,35,36,37,38,39,40},        
        };
#endregion

        [TestCaseSource("Key")]
        public void IsKeyDown(int key)
        {
            ANXKeys[] anxkey = new ANXKeys[1];
            XNAKeys[] xnakey = new XNAKeys[1];
            anxkey[0] = (ANXKeys)key;
            xnakey[0] = (XNAKeys)key;
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            AssertHelper.ConvertEquals(xna.IsKeyDown((XNAKeys)key), anx.IsKeyDown((ANXKeys)key),"IsKeyDown" );
        }

        [TestCaseSource("Key")]
        public void IsKeyUp(int key)
        {
            ANXKeys[] anxkey = new ANXKeys[1];
            XNAKeys[] xnakey = new XNAKeys[1];
            anxkey[0] = (ANXKeys)key;
            xnakey[0] = (XNAKeys)key;
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            AssertHelper.ConvertEquals(xna.IsKeyUp((XNAKeys)key), anx.IsKeyUp((ANXKeys)key), "IsKeyUp");
        }

        [TestCaseSource("Key10")]
        public void opEqual(int[] key)
        {
            ANXKeys[] anxkey = new ANXKeys[10];
            
            XNAKeys[] xnakey = new XNAKeys[10];
            for (int i = 0; i < key.Length; i++)
            {
                anxkey[i] = (ANXKeys)key[i];
                xnakey[i] = (XNAKeys)key[i];

            }
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            ANXKeyboardState anx2 = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna2 = new XNAKeyboardState(xnakey);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "opequal");
        }

        [TestCaseSource("Key10")]
        public void opEqual2(int[] key)
        {
            ANXKeys[] anxkey = new ANXKeys[10];

            XNAKeys[] xnakey = new XNAKeys[10];
            for (int i = 0; i < key.Length; i++)
            {
                anxkey[i] = (ANXKeys)key[i];
                xnakey[i] = (XNAKeys)key[i];

            }
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            anxkey[9] = (ANXKeys)9;
            xnakey[9] = (XNAKeys)9;
            XNAKeyboardState xna2 = new XNAKeyboardState(xnakey);
            ANXKeyboardState anx2 = new ANXKeyboardState(anxkey);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "opequal2");
        }

        [TestCaseSource("Key10")]
        public void opEqual3(int[] key)
        {
            ANXKeys[] anxkey = new ANXKeys[10];

            XNAKeys[] xnakey = new XNAKeys[10];
            for (int i = 0; i < key.Length; i++)
            {
                anxkey[i] = (ANXKeys)key[i];
                xnakey[i] = (XNAKeys)key[i];

            }
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            ANXKeyboardState anx2 = new ANXKeyboardState(new ANXKeys[0]);
            XNAKeyboardState xna2 = new XNAKeyboardState(new XNAKeys[0]);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "opequal3");
        }

        [TestCaseSource("Key10")]
        public void opInEqual3(int[] key)
        {
            ANXKeys[] anxkey = new ANXKeys[10];

            XNAKeys[] xnakey = new XNAKeys[10];
            for (int i = 0; i < key.Length; i++)
            {
                anxkey[i] = (ANXKeys)key[i];
                xnakey[i] = (XNAKeys)key[i];

            }
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);
            ANXKeyboardState anx2 = new ANXKeyboardState(new ANXKeys[0]);
            XNAKeyboardState xna2 = new XNAKeyboardState(new XNAKeys[0]);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "opequal3");
        }

        [TestCaseSource("Key10")]
        public void GetPressedKeys(int[] key)
        {
            ANXKeys[] anxkey = new ANXKeys[10];

            XNAKeys[] xnakey = new XNAKeys[10];
            for (int i = 0; i < key.Length; i++)
            {
                anxkey[i] = (ANXKeys)key[i];
                xnakey[i] = (XNAKeys)key[i];

            }
            ANXKeyboardState anx = new ANXKeyboardState(anxkey);
            XNAKeyboardState xna = new XNAKeyboardState(xnakey);

            AssertHelper.ConvertEquals(xna.GetPressedKeys(), anx.GetPressedKeys(), "GetPressedKeys");
        }
    }
}
