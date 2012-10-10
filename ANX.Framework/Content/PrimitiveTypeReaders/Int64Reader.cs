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
    internal class Int64Reader : ContentTypeReader<Int64>
    {
        protected internal override Int64 Read(ContentReader input, Int64 existingInstance)
        {
            return input.ReadInt64();
        }
    }

    [Developer("GinieDP")]
    internal class UInt64Reader : ContentTypeReader<UInt64>
    {
        protected internal override UInt64 Read(ContentReader input, UInt64 existingInstance)
        {
            return input.ReadUInt64();
        }
    }
}
