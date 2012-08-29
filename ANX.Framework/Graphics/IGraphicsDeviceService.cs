#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann")]
    public interface IGraphicsDeviceService
    {
        // Events
        event EventHandler<EventArgs> DeviceCreated;
        event EventHandler<EventArgs> DeviceDisposing;
        event EventHandler<EventArgs> DeviceReset;
        event EventHandler<EventArgs> DeviceResetting;

        // Properties
        GraphicsDevice GraphicsDevice { get; }
    }
}
