#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ContentBuildLogger
    {
        private Stack<string> files = new Stack<string>();

        protected ContentBuildLogger()
        {
        }

        public abstract void LogImportantMessage(string message, params Object[] messageArgs);
        public abstract void LogMessage(string message, params Object[] messageArgs);
        public abstract void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        public void PopFile()
        {
            files.Pop();
        }

        public void PushFile(string filename)
        {
            files.Push(filename);
        }

        protected string GetCurrentFilename(ContentIdentity contentIdentity)
        {
            throw new NotImplementedException();
        }
    }
}
