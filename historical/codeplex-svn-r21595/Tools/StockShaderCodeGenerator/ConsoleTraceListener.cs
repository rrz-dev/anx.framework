#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
    class ConsoleTraceListener
    {
        public static bool Silence = false;

        public void Write(string message)
        {
            if (!Silence)
            {
                Console.Write(message);
            }
        }

        public void WriteLine(string message)
        {
            if (!Silence)
            {
                Console.WriteLine(message);
            }
        }

        public void WriteLine(string formatMessage, params object[] parameters)
        {
            if (!Silence)
            {
                Console.WriteLine(formatMessage, parameters);
            }
        }
    }
}
