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
    [Serializable]
    public class ContentIdentity
    {
        public ContentIdentity()
        {

        }

        public ContentIdentity(string sourceFilename)
        {
            SourceFilename = sourceFilename;
        }

        public ContentIdentity(string sourceFilename, string sourceTool)
        {
            SourceFilename = sourceFilename;
            SourceTool = sourceTool;
        }

        public ContentIdentity(string sourceFilename, string sourceTool, string fragmentIdentifier)
        {
            SourceFilename = sourceFilename;
            SourceTool = sourceTool;
            FragmentIdentifier = fragmentIdentifier;
        }

        [ContentSerializer(Optional = true)]
        public string SourceFilename
        {
            get;
            set;
        }

        [ContentSerializer(Optional = true)]
        public string SourceTool
        {
            get;
            set;
        }

        [ContentSerializer(Optional = true)]
        public string FragmentIdentifier
        {
            get;
            set;
        }
    }
}
