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
    internal class TimeSpanReader : ContentTypeReader<TimeSpan>
    {
        protected internal override TimeSpan Read(ContentReader input, TimeSpan existingInstance)
        {
            long ticks = input.ReadInt64();
            return new TimeSpan(ticks);
        }
    }

}
