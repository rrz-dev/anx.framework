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
        protected ContentBuildLogger()
        {
            throw new NotImplementedException();
        }

        public abstract void LogImportantMessage(string message, params Object[] messageArgs);
        public abstract void LogMessage(string message, params Object[] messageArgs);
        public abstract void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        public void PopFile()
        {
            throw new NotImplementedException();
        }

        public void PushFile(string filename)
        {
            throw new NotImplementedException();
        }

        protected string GetCurrentFilename(ContentIdentity contentIdentity)
        {
            throw new NotImplementedException();
        }
    }
}
