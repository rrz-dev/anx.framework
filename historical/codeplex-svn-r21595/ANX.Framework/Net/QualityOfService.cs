using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public sealed class QualityOfService
    {
        public bool IsAvailable { get; private set; }
        public int BytesPerSecondUpstream { get; private set; }
        public int BytesPerSecondDownstream { get; private set; }
        public TimeSpan AverageRoundtripTime { get; private set; }
        public TimeSpan MinimumRoundtripTime { get; private set; }

        internal QualityOfService()
        {
        }

        internal QualityOfService(int bytesPerSecondUpstream, int bytesPerSecondDownstream, TimeSpan averageRoundtripTime,
            TimeSpan minimumRoundtripTime)
        {
            BytesPerSecondUpstream = bytesPerSecondUpstream;
            BytesPerSecondDownstream = bytesPerSecondDownstream;
            AverageRoundtripTime = averageRoundtripTime;
            MinimumRoundtripTime = minimumRoundtripTime;
            IsAvailable = true;
        }
    }
}
