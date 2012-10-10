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
    internal class DateTimeReader : ContentTypeReader<DateTime>
    {
        protected internal override DateTime Read(ContentReader input, DateTime existingInstance)
        {
            long value = input.ReadInt64();

            long kind = (value >> 62) & 3L;     // High 2 bits hold a .NET DateTimeKind enum value
            long ticks = value & (~(3L << 62)); // Low 62 bits hold a .NET DateTime tick count

            DateTimeKind dateTimeKind = (DateTimeKind)kind;
            if (dateTimeKind == DateTimeKind.Local)
            {
                return new DateTime(ticks, DateTimeKind.Utc).ToLocalTime();
            }
            return new DateTime(ticks, dateTimeKind);
        }
    }
}
