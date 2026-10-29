#region Using Statements
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Storage
{
    [PercentageComplete(95)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class StorageDeviceNotConnectedException : ExternalException
    {
        public StorageDeviceNotConnectedException()
            : base()
        { }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected StorageDeviceNotConnectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
#endif

        public StorageDeviceNotConnectedException(string message)
            : base(message)
        { }

        public StorageDeviceNotConnectedException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
