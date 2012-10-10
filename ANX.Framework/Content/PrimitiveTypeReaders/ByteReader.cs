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
    internal class ByteReader : ContentTypeReader<Byte>
    {
        protected internal override byte Read(ContentReader input, Byte existingInstance)
        {
            return input.ReadByte();
        }
    }

    [Developer("GinieDP")]
    internal class SByteReader : ContentTypeReader<SByte>
    {
        protected internal override sbyte Read(ContentReader input, SByte existingInstance)
        {
            return input.ReadSByte();
        }
    }
}
