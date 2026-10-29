#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ContentBuilder
{
    public class ConsoleLogger : ContentBuildLogger
    {
        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            Write("Important", message, messageArgs);
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            Write("", message, messageArgs);
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            Write("Warning", message, messageArgs);
        }

        private void Write(string severity, string message, params object[] messageArgs)
        {
            Console.WriteLine(String.Format("[{0}] {1}", severity, String.Format(message, messageArgs)));
        }
    }
}
