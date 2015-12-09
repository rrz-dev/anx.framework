using ANX.Framework.Content.Pipeline;
using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ErrorLoggingHelper : MarshalByRefObject
    {
        public enum MessageImportance
        {
            Error = 0,
            Warning = 1,
            Info = 2,
            BuildLog = 3,
        }

        ErrorListProvider errorProvider;
        IVsOutputWindowPane outputWindow;
        ProjectNode projectNode;

        public ErrorLoggingHelper(ProjectNode projectNode, ErrorListProvider errorProvider)
        {
            if (errorProvider == null)
                throw new ArgumentNullException("errorProvider");

            this.errorProvider = errorProvider;
            this.projectNode = projectNode;
        }

        public ErrorLoggingHelper(ProjectNode projectNode, ErrorListProvider errorProvider, IVsOutputWindowPane outputWindow)
            : this(projectNode, errorProvider)
        {
            this.outputWindow = outputWindow;
        }

        public void LogMessage(string subcategory, string code, string helpKeyword, string file, int lineNumber, int columnNumber, MessageImportance importance, string message, params object[] messageArgs)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            int index = -1;
            if (subcategory != null)
            {
                index = errorProvider.Subcategories.IndexOf(subcategory);
                if (index == -1)
                {
                    index = errorProvider.Subcategories.Add(subcategory);
                }
            }

            if (importance != MessageImportance.BuildLog)
            {
                errorProvider.Tasks.Add(new Microsoft.VisualStudio.Shell.ErrorTask() 
                { 
                    ErrorCategory = (TaskErrorCategory)importance,
                    HierarchyItem = projectNode,
                    Priority = (TaskPriority)importance, 
                    Category = TaskCategory.BuildCompile, 
                    Column = columnNumber, 
                    Document = file, 
                    HelpKeyword = helpKeyword, 
                    SubcategoryIndex = index, 
                    Line = lineNumber, 
                    Text = string.Format(message, messageArgs) });
            }

            if (this.outputWindow != null)
            {
                string finalMesssage = message;
                if (messageArgs != null)
                    finalMesssage = string.Format(message, messageArgs);

                this.outputWindow.OutputStringThreadSafe(finalMesssage + Environment.NewLine);
            }
        }

        public void LogMessage(string file, string message, params object[] messageArgs)
        {
            LogMessage(null, null, null, file, 0, 0, MessageImportance.BuildLog, message, messageArgs);
        }

        public void LogError(string file, string message, params object[] messageArgs)
        {
            this.LogCriticalMessage(null, null, null, file, 0, 0, message, messageArgs);
        }

        public void LogError(Exception exception)
        {
            int lineNumber = 0;
            int columnNumber = 0;

            if (exception is InvalidContentException)
            {
                var exc = (InvalidContentException)exception;
                var identity = exc.ContentIdentity;
                if (identity != null)
                {
                    identity.TryGetLineAndColumn(out lineNumber, out columnNumber);
                }
            }

            this.LogCriticalMessage(null, null, exception.HelpLink, exception.TargetSite.DeclaringType.Assembly.Location, lineNumber, columnNumber, exception.Message + Environment.NewLine + exception.StackTrace);
        }

        public void LogCriticalMessage(string subcategory, string code, string helpKeyword, string file, int lineNumber, int columnNumber, string message, params object[] messageArgs)
        {
            LogMessage(subcategory, code, helpKeyword, file, lineNumber, columnNumber, MessageImportance.Error, message, messageArgs);
        }

        public void LogWarning(string helpLink, ContentIdentity contentIdentity, string filename, string message, params Object[] messageArgs)
        {
            int lineNumber = 0;
            int columnNumber = 0;

            if (contentIdentity != null)
                contentIdentity.TryGetLineAndColumn(out lineNumber, out columnNumber);

            this.LogWarning(null, null, helpLink, filename, lineNumber, columnNumber, message, messageArgs);
        }
        
        public void LogMessage(string helpLink, ContentIdentity contentIdentity, string file, string message, params Object[] messageArgs)
        {
            this.LogMessage(helpLink, contentIdentity, file, MessageImportance.BuildLog, message, messageArgs);
        }

        public void LogMessage(string helpLink, ContentIdentity contentIdentity, string file, MessageImportance importance, string message, params Object[] messageArgs)
        {
            int lineNumber = 0;
            int columnNumber = 0;

            if (contentIdentity != null)
                contentIdentity.TryGetLineAndColumn(out lineNumber, out columnNumber);

            this.LogMessage(null, null, helpLink, file, lineNumber, columnNumber, importance, message, messageArgs);
        }

        public void LogWarning(string subcategory, string warningCode, string helpKeyword, string file, int lineNumber, int columnNumber, string message, params object[] messageArgs)
        {
            LogMessage(subcategory, warningCode, helpKeyword, file, lineNumber, columnNumber, MessageImportance.Warning, message, messageArgs);
        }
    }
}
