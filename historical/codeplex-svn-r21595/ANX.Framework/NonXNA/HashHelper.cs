#region Using Statements
using System;
using System.Runtime.InteropServices;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    internal class HashHelper
    {
        public unsafe static int GetGCHandleHashCode(object obj)
        {
            int result = 0;
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);

            try
            {
                int objSize = Marshal.SizeOf(obj);
                int* ptr = (int*)handle.AddrOfPinnedObject().ToPointer();

                for (int i = 4; i <= objSize; i += 4)
                {
                    result ^= *ptr;
                    ptr++;
                }

                if (result <= 0)
                {
                    result = int.MaxValue;
                }
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

    }
}
