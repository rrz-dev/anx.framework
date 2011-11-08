#region Using Statements
using System;
using System.Runtime.InteropServices;

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

namespace ANX.Framework.Graphics.PackedVector
{
    internal class HalfTypeHelper
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct uif
        {
            [FieldOffset(0)]
            public float f;
            [FieldOffset(0)]
            public int i;
        }

        internal static UInt16 convert(float f)
        {
            uif uif = new uif();
            uif.f = f;
            return convert(uif.i);
        }

        internal static UInt16 convert(int i)
        {
            int s = (i >> 16) & 0x00008000;
            int e = ((i >> 23) & 0x000000ff) - (127 - 15);
            int m = i & 0x007fffff;

            if (e <= 0)
            {
                if (e < -10)
                {
                    return (UInt16)s;
                }

                m = m | 0x00800000;

                int t = 14 - e;
                int a = (1 << (t - 1)) - 1;
                int b = (m >> t) & 1;

                m = (m + a + b) >> t;

                return (UInt16)(s | m);
            }
            else if (e == 0xff - (127 - 15))
            {
                if (m == 0)
                {
                    return (UInt16)(s | 0x7c00);
                }
                else
                {
                    m >>= 13;
                    return (UInt16)(s | 0x7c00 | m | ((m == 0) ? 1 : 0));
                }
            }
            else
            {
                m = m + 0x00000fff + ((m >> 13) & 1);

                if ((m & 0x00800000) != 0)
                {
                    m = 0;
                    e += 1;
                }

                if (e > 30)
                {
                    return (UInt16)(s | 0x7c00);
                }

                return (UInt16)(s | (e << 10) | (m >> 13));
            }
        }

        internal static float convert(UInt16 value)
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}
