#region Using Statements
using System;

#endregion // Using Statements

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

namespace ANX.Framework.Windows.DX10
{
    internal static class ShaderByteCode
    {

        #region SpriteBatchShader
        internal static byte[] SpriteBatchByteCode = new byte[]
        {
            68, 88, 66, 67, 76, 188, 7, 250, 222, 184, 8, 85, 30, 93, 218, 8, 162, 103, 199, 180, 1,
            0, 0, 0, 44, 8, 0, 0, 1, 0, 0, 0, 36, 0, 0, 0, 70, 88, 49, 48, 0, 8, 0, 0, 1, 16, 255, 254,
            1, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 12, 7, 
            0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 36, 71, 108, 111, 98, 97, 108, 115, 0, 102, 108,
            111, 97, 116, 52, 120, 52, 0, 13, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 0, 
            64, 0, 0, 0, 11, 100, 0, 0, 77, 97, 116, 114, 105, 120, 84, 114, 97, 110, 115, 102, 111, 114, 
            109, 0, 84, 101, 120, 116, 117, 114, 101, 50, 68, 0, 66, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 0, 84, 101, 120, 116, 117, 114, 101, 0, 83, 97, 109,
            112, 108, 101, 114, 83, 116, 97, 116, 101, 0, 112, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 21, 0, 0, 0, 84, 101, 120, 116, 117, 114, 101, 83, 97, 109, 112, 108,
            101, 114, 0, 83, 112, 114, 105, 116, 101, 84, 101, 99, 104, 110, 105, 113, 117, 101, 0, 83, 112,
            114, 105, 116, 101, 67, 111, 108, 111, 114, 80, 97, 115, 115, 0, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0,
            0, 0, 128, 3, 0, 0, 68, 88, 66, 67, 93, 23, 212, 206, 149, 8, 160, 173, 236, 209, 184, 180, 25,
            147, 8, 113, 1, 0, 0, 0, 128, 3, 0, 0, 5, 0, 0, 0, 52, 0, 0, 0, 8, 1, 0, 0, 120, 1, 0, 0, 236,
            1, 0, 0, 4, 3, 0, 0, 82, 68, 69, 70, 204, 0, 0, 0, 1, 0, 0, 0, 72, 0, 0, 0, 1, 0, 0, 0, 28, 0,
            0, 0, 0, 4, 254, 255, 0, 1, 0, 0, 152, 0, 0, 0, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 36, 71, 108, 111, 98, 97, 108, 115, 0, 171,
            171, 171, 60, 0, 0, 0, 1, 0, 0, 0, 96, 0, 0, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 120, 0, 0,
            0, 0, 0, 0, 0, 64, 0, 0, 0, 2, 0, 0, 0, 136, 0, 0, 0, 0, 0, 0, 0, 77, 97, 116, 114, 105, 120,
            84, 114, 97, 110, 115, 102, 111, 114, 109, 0, 3, 0, 3, 0, 4, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            77, 105, 99, 114, 111, 115, 111, 102, 116, 32, 40, 82, 41, 32, 72, 76, 83, 76, 32, 83, 104, 
            97, 100, 101, 114, 32, 67, 111, 109, 112, 105, 108, 101, 114, 32, 57, 46, 50, 57, 46, 57, 53,
            50, 46, 51, 49, 49, 49, 0, 171, 171, 171, 73, 83, 71, 78, 104, 0, 0, 0, 3, 0, 0, 0, 8, 0, 0,
            0, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 15, 15, 0, 0, 89, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 15, 15, 0, 0, 95, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 3, 0, 0, 0, 2, 0, 0, 0, 3, 3, 0, 0, 80, 79, 83, 73, 84, 73, 79, 78, 0, 67, 79, 76, 79, 82,
            0, 84, 69, 88, 67, 79, 79, 82, 68, 0, 79, 83, 71, 78, 108, 0, 0, 0, 3, 0, 0, 0, 8, 0, 0, 0, 80,
            0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 92, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 15, 0, 0, 0, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0,
            0, 2, 0, 0, 0, 3, 12, 0, 0, 83, 86, 95, 80, 79, 83, 73, 84, 73, 79, 78, 0, 67, 79, 76, 79, 82, 0,
            84, 69, 88, 67, 79, 79, 82, 68, 0, 171, 83, 72, 68, 82, 16, 1, 0, 0, 64, 0, 1, 0, 68, 0, 0, 0,
            89, 0, 0, 4, 70, 142, 32, 0, 0, 0, 0, 0, 4, 0, 0, 0, 95, 0, 0, 3, 242, 16, 16, 0, 0, 0, 0, 0,
            95, 0, 0, 3, 242, 16, 16, 0, 1, 0, 0, 0, 95, 0, 0, 3, 50, 16, 16, 0, 2, 0, 0, 0, 103, 0, 0, 4, 
            242, 32, 16, 0, 0, 0, 0, 0, 1, 0, 0, 0, 101, 0, 0, 3, 242, 32, 16, 0, 1, 0, 0, 0, 101, 0, 0, 3, 
            50, 32, 16, 0, 2, 0, 0, 0, 17, 0, 0, 8, 18, 32, 16, 0, 0, 0, 0, 0, 70, 30, 16, 0, 0, 0, 0, 0, 70,
            142, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 8, 34, 32, 16, 0, 0, 0, 0, 0, 70, 30, 16, 0, 0, 0, 0,
            0, 70, 142, 32, 0, 0, 0, 0, 0, 1, 0, 0, 0, 17, 0, 0, 8, 66, 32, 16, 0, 0, 0, 0, 0, 70, 30, 16, 0, 0,
            0, 0, 0, 70, 142, 32, 0, 0, 0, 0, 0, 2, 0, 0, 0, 17, 0, 0, 8, 130, 32, 16, 0, 0, 0, 0, 0, 70, 30,
            16, 0, 0, 0, 0, 0, 70, 142, 32, 0, 0, 0, 0, 0, 3, 0, 0, 0, 54, 0, 0, 5, 242, 32, 16, 0, 1, 0, 0, 0, 
            70, 30, 16, 0, 1, 0, 0, 0, 54, 0, 0, 5, 50, 32, 16, 0, 2, 0, 0, 0, 70, 16, 16, 0, 2, 0, 0, 0, 62, 0, 
            0, 1, 83, 84, 65, 84, 116, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 4, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 212, 0, 0, 0, 0, 0, 0, 0, 
            160, 2, 0, 0, 68, 88, 66, 67, 29, 76, 118, 93, 197, 15, 41, 178, 119, 144, 245, 77, 96, 29, 105, 32, 1,
            0, 0, 0, 160, 2, 0, 0, 5, 0, 0, 0, 52, 0, 0, 0, 224, 0, 0, 0, 84, 1, 0, 0, 136, 1, 0, 0, 36, 2, 0, 0, 
            82, 68, 69, 70, 164, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 28, 0, 0, 0, 0, 4, 255, 255, 0, 1, 0,
            0, 115, 0, 0, 0, 92, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
            1, 0, 0, 0, 107, 0, 0, 0, 2, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 1, 0, 0, 
            0, 13, 0, 0, 0, 84, 101, 120, 116, 117, 114, 101, 83, 97, 109, 112, 108, 101, 114, 0, 84, 101, 120, 116,
            117, 114, 101, 0, 77, 105, 99, 114, 111, 115, 111, 102, 116, 32, 40, 82, 41, 32, 72, 76, 83, 76, 32, 83, 
            104, 97, 100, 101, 114, 32, 67, 111, 109, 112, 105, 108, 101, 114, 32, 57, 46, 50, 57, 46, 57, 53, 50, 46, 
            51, 49, 49, 49, 0, 73, 83, 71, 78, 108, 0, 0, 0, 3, 0, 0, 0, 8, 0, 0, 0, 80, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0,
            0, 3, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 92, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 15, 15,
            0, 0, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 2, 0, 0, 0, 3, 3, 0, 0, 83, 86, 95, 80, 79, 83, 73, 84,
            73, 79, 78, 0, 67, 79, 76, 79, 82, 0, 84, 69, 88, 67, 79, 79, 82, 68, 0, 171, 79, 83, 71, 78, 44, 0, 0, 0, 1, 0,
            0, 0, 8, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 83, 86, 95, 84, 97,
            114, 103, 101, 116, 0, 171, 171, 83, 72, 68, 82, 148, 0, 0, 0, 64, 0, 0, 0, 37, 0, 0, 0, 90, 0, 0, 3, 0, 96, 16,
            0, 0, 0, 0, 0, 88, 24, 0, 4, 0, 112, 16, 0, 0, 0, 0, 0, 85, 85, 0, 0, 98, 16, 0, 3, 242, 16, 16, 0, 1, 0, 0, 0, 98, 
            16, 0, 3, 50, 16, 16, 0, 2, 0, 0, 0, 101, 0, 0, 3, 242, 32, 16, 0, 0, 0, 0, 0, 104, 0, 0, 2, 1, 0, 0, 0, 69, 0, 0, 9,
            242, 0, 16, 0, 0, 0, 0, 0, 70, 16, 16, 0, 2, 0, 0, 0, 70, 126, 16, 0, 0, 0, 0, 0, 0, 96, 16, 0, 0, 0, 0, 0, 56, 0, 0, 
            7, 242, 32, 16, 0, 0, 0, 0, 0, 70, 14, 16, 0, 0, 0, 0, 0, 70, 30, 16, 0, 1, 0, 0, 0, 62, 0, 0, 1, 83, 84, 65, 84, 116, 
            0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96,
            4, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 64, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 50, 0, 0, 0, 22, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 104, 0, 0, 0, 76, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255,
            255, 0, 0, 0, 0, 153, 0, 0, 0, 125, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 168, 0, 0, 0, 1, 0,
            0, 0, 0, 0, 0, 0, 184, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 200, 0, 0, 0, 6, 0, 0, 0, 0,
            0, 0, 0, 7, 0, 0, 0, 88, 4, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 4, 7, 0, 0
        };

        #endregion // SpriteBatchShader
    }
}
