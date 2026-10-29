using ANX.Framework.Content.Pipeline;
using ANX.Framework.NonXNA.Development;

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI
{
    [PercentageComplete(100)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class FakeBuildLogger : ContentBuildLogger
    {
        #region Overrides of ContentBuildLogger

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
           
        }

        #endregion
    }
}
