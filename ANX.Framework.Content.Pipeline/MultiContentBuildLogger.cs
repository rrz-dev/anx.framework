using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    [Developer("KorsarNek")]
    public class MultiContentBuildLogger : ContentBuildLogger
    {
        private List<IContentBuildLogger> _childs = new List<IContentBuildLogger>();

        public IList<IContentBuildLogger> Childs
        {
            get { return _childs; }
        }

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            foreach (var child in Childs)
                try
                {
                    //Before we send the messageArgs over the eventual process or appDomain boundaries, we format them into the message to make sure that we don't transfer non-marshable types.
                    child.LogImportantMessage(string.Format(message, messageArgs)); 
                }
                catch (Exception exc) 
                {
                    LogLoggerError(exc, child);
                }
        }

        public override void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            foreach (var child in Childs)
                try
                {
                    child.LogImportantMessage(helpLink, contentIdentity, string.Format(message, messageArgs));
                }
                catch (Exception exc) 
                {
                    LogLoggerError(exc, child); 
                }
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            foreach (var child in Childs)
                try
                {
                    child.LogMessage(string.Format(message, messageArgs));
                }
                catch (Exception exc) 
                {
                    LogLoggerError(exc, child);
                }
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            foreach (var child in Childs)
                try
                {
                    child.LogWarning(helpLink, contentIdentity, string.Format(message, messageArgs));
                }
                catch (Exception exc)
                {
                    LogLoggerError(exc, child); 
                }
        }

        public override string LoggerRootDirectory
        {
            get
            {
                return base.LoggerRootDirectory;
            }
            set
            {
                base.LoggerRootDirectory = value;
                foreach (var child in Childs)
                    try
                    {
                        child.LoggerRootDirectory = value;
                    }
                    catch (Exception exc) 
                    {
                        LogLoggerError(exc, child);
                    }
            }
        }

        public override void PopFile()
        {
            base.PopFile();
            foreach (var child in Childs)
                try
                {
                    child.PopFile();
                }
                catch (Exception exc)
                {
                    LogLoggerError(exc, child); 
                }
        }

        public override void PushFile(string filename)
        {
            base.PushFile(filename);
            foreach (var child in Childs)
                try
                {
                    child.PushFile(filename);
                }
                catch (Exception exc)
                {
                    LogLoggerError(exc, child); 
                }
        }

        private void LogLoggerError(Exception exc, IContentBuildLogger faultyLogger)
        {
            //For .net 4.0, whe have to use a stackFrame to find out from where we have been called instead of using the [CallerMemberName] attribute.
            StackFrame stack = new StackFrame(1);
            string methodName = stack.GetMethod().Name;

            foreach (var child in Childs.Except(new[] { faultyLogger }))
            {
                try
                {
                    child.LogMessage(string.Format("Logger \"{0}\" had an error executing {1}: {2}", faultyLogger.GetType().Name, methodName, exc.Message));
                }
                catch
                {
                    //If there's an exception again, just forget about it and let the actual error get logged by the loggers that can handle these things.
                }
            }
        }
    }
}
