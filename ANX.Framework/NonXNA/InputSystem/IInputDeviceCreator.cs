#region Using Statements
using System;
using ANX.Framework.Input;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.InputSystem
{
    public interface IInputDeviceCreator
    {
        string Name { get; }

        void RegisterCreator(InputDeviceFactory factory);

        int Priority { get; }
    }
}
