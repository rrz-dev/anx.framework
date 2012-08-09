#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface IInputSystemCreator : ICreator
    {
        IGamePad GamePad { get; }

        IMouse Mouse { get; }

        IKeyboard Keyboard { get; }

#if XNAEXT
        IMotionSensingDevice MotionSensingDevice { get; }
#endif

    }
}
