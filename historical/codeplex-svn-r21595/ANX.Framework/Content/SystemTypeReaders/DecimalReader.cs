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
    internal class DecimalReader : ContentTypeReader<Decimal>
    {
        protected internal override Decimal Read(ContentReader input, Decimal existingInstance)
        {
            int[] values = new int[4]
            {
                input.ReadInt32(),
                input.ReadInt32(),
                input.ReadInt32(),
                input.ReadInt32()
            };

            return new Decimal(values);
        }
    }
}
