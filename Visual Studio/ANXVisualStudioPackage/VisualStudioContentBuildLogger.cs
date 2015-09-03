using ANX.Framework.Content.Pipeline;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class VisualStudioContentBuildLogger : ContentBuildLogger
    {
        ErrorLoggingHelper loggingHelper;

        public VisualStudioContentBuildLogger(ErrorLoggingHelper loggingHelper)
        {
            this.loggingHelper = loggingHelper;
        }

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            loggingHelper.LogCriticalMessage(null, null, null, this.GetCurrentFilename(), 0, 0, message, messageArgs);
        }

        public override void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
 	        loggingHelper.LogMessage(helpLink, contentIdentity, this.GetCurrentFilename(contentIdentity), ErrorLoggingHelper.MessageImportance.Info, message, messageArgs);
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
 	        loggingHelper.LogMessage(this.GetCurrentFilename(), message, messageArgs);
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
 	        loggingHelper.LogWarning(helpLink, contentIdentity, this.GetCurrentFilename(contentIdentity), message, messageArgs);
        }
    }
}
