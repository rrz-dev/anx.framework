#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    [Developer("KorsarNek")]
    public class ConsoleContentBuildLogger : ContentBuildLogger
    {
        private string _name;

        public override string Name
        {
            get { return this._name; }
        }

        public ConsoleContentBuildLogger(string name)
        {
            this._name = name;
        }

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Write("Important", message, messageArgs);
            Console.ForegroundColor = previousColor;
        }

        public override void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Write("Important", message, messageArgs);
            Console.ForegroundColor = previousColor;
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            var previousColor = Console.ForegroundColor;
            Console.ResetColor();
            Write("Info", message, messageArgs);
            Console.ForegroundColor = previousColor;
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write("Warning", message, messageArgs);
            Console.ForegroundColor = previousColor;
        }

        private void Write(string severity, string message, params object[] messageArgs)
        {
            Console.WriteLine(String.Format("[{0}] {1}", severity, String.Format(message, messageArgs)));
        }
    }
}
