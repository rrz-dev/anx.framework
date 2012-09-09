using System;
using ANX.Framework.Content.Pipeline;

namespace ANX.ContentCompiler.GUI
{
    public class CCompilerBuildLogger : ContentBuildLogger
    {
        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
           MainWindow.Instance.ribbonTextBox.AddMessage("[IMPORTANT] " + String.Format(message, messageArgs));
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            MainWindow.Instance.ribbonTextBox.AddMessage("[Info] " + String.Format(message, messageArgs)); 
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message,
                                        params object[] messageArgs)
        {
            MainWindow.Instance.ribbonTextBox.AddMessage("[WARNING] " + String.Format(message, messageArgs));
        }
    }
}