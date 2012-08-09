#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class Int16Reader : ContentTypeReader<Int16>
    {
        protected internal override Int16 Read(ContentReader input, Int16 existingInstance)
        {
            return input.ReadInt16();
        }
    }

    internal class UInt16Reader : ContentTypeReader<UInt16>
    {
        protected internal override UInt16 Read(ContentReader input, UInt16 existingInstance)
        {
            return input.ReadUInt16();
        }
    }
}
