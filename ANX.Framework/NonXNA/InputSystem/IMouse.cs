#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface IMouse
    {
       IntPtr WindowHandle { get; set; }
       MouseState GetState();
       void SetPosition(int x, int y);
    }
}
