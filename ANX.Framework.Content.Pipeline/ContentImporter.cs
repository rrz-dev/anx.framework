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
    public abstract class ContentImporter<T> : IContentImporter
    {
        protected ContentImporter()
        {
            throw new NotImplementedException();
        }

        Object ANX.Framework.Content.Pipeline.IContentImporter.Import(string filename, ContentImporterContext context)
        {
            throw new NotImplementedException();
        }

    }
}
