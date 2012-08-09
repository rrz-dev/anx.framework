#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class NoSuitableGraphicsDeviceException : Exception
    {
        public NoSuitableGraphicsDeviceException()
            : base()
        {
        }

        public NoSuitableGraphicsDeviceException(string message)
            : base(message)
        {
        }

        public NoSuitableGraphicsDeviceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
