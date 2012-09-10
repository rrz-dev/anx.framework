using System;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(99)] //TODO: Logging to a file and (RTF) colors would be cool!
    [TestState(TestStateAttribute.TestState.Tested)]
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