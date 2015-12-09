#region Using Statements
using System;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.NonXNA.Development;
using System.ServiceModel;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(99)] //TODO: Logging to a file would be cool!
    [TestState(TestStateAttribute.TestState.Tested)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CCompilerBuildLogger : ContentBuildLogger
    {
        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            MainWindow.Instance.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.ribbonTextBox.SetTextColor(System.Drawing.Color.Blue);
                    MainWindow.Instance.ribbonTextBox.AddMessage("[IMPORTANT] " + String.Format(message, messageArgs));
                }));
        }

#if XNAEXT
        public override void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            MainWindow.Instance.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.ribbonTextBox.SetTextColor(System.Drawing.Color.Blue);
                    MainWindow.Instance.ribbonTextBox.AddMessage("[IMPORTANT] " + String.Format(message, messageArgs));
                }));
        }
#endif

        public override void LogMessage(string message, params object[] messageArgs)
        {
            MainWindow.Instance.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.ribbonTextBox.ResetTextColor();
                    MainWindow.Instance.ribbonTextBox.AddMessage("[Info] " + String.Format(message, messageArgs));
                }));
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message,
                                        params object[] messageArgs)
        {
            MainWindow.Instance.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.ribbonTextBox.SetTextColor(System.Drawing.Color.Yellow);
                    MainWindow.Instance.ribbonTextBox.AddMessage("[WARNING] " + String.Format(message, messageArgs));
                }));
        }
    }
}