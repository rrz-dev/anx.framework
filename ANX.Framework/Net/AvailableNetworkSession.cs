#region Using Statements
using System;
using System.Net;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(95)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class AvailableNetworkSession
    {
        internal AvailableNetworkSession()
        {
            QualityOfService = new QualityOfService();
        }

        public int CurrentGamerCount { get; internal set; }
        public string HostGamertag { get; internal set; }
        public int OpenPrivateGamerSlots { get; internal set; }
        public int OpenPublicGamerSlots { get; internal set; }
        public QualityOfService QualityOfService { get; internal set; }
        public NetworkSessionProperties SessionProperties { get; internal set; }

#if !WINDOWSMETRO //TODO: search replacement for Win8
        internal IPEndPoint EndPoint { get; set; }
        internal IPEndPoint InternalEndpont { get; set; }
#endif

        internal NetworkSessionType SessionType { get; set; }
    }
}
