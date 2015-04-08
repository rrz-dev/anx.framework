#region Using Statements
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ContentBuildLogger : IContentBuildLogger
    {
        private Stack<string> files = new Stack<string>();

        protected ContentBuildLogger()
        {
        }

        /// <summary>
        /// Gets or sets the base reference path used when reporting errors during the content build process. 
        /// </summary>
        public virtual string LoggerRootDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the name of the logger.
        /// </summary>
        public abstract string Name
        {
            get;
        }

        public abstract void LogImportantMessage(string message, params Object[] messageArgs);

        public abstract void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        public abstract void LogMessage(string message, params Object[] messageArgs);

        public abstract void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        public virtual void PopFile()
        {
            files.Pop();
        }

        public virtual void PushFile(string filename)
        {
            files.Push(filename);
        }

        protected string GetCurrentFilename()
        {
            return this.GetCurrentFilename(null);
        }

        protected string GetCurrentFilename(ContentIdentity contentIdentity)
        {
            if (contentIdentity != null && !string.IsNullOrEmpty(contentIdentity.SourceFilename))
            {
                return GetRelativeFilename(contentIdentity.SourceFilename);
            }
            if (this.files.Count > 0)
            {
                return GetRelativeFilename(this.files.Peek());
            }
            return null;
        }

        private string GetRelativeFilename(string filename)
        {
            if (LoggerRootDirectory != null && filename.StartsWith(this.LoggerRootDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return filename.Substring(this.LoggerRootDirectory.Length);
            }
            return filename;
        }
    }
}
