#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class Int32Reader : ContentTypeReader<Int32>
    {
        protected internal override Int32 Read(ContentReader input, Int32 existingInstance)
        {
            return input.ReadInt32();
        }
    }

    [Developer("GinieDP")]
    internal class UInt32Reader : ContentTypeReader<UInt32>
    {
        protected internal override UInt32 Read(ContentReader input, UInt32 existingInstance)
        {
            return input.ReadUInt32();
        }
    }
}
