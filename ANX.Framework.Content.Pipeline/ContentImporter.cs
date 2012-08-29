// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ContentImporter<T> : IContentImporter
    {
        protected ContentImporter()
        {
            // nothing to do here
        }

        public abstract T Import(string filename, ContentImporterContext context);

        object IContentImporter.Import(string filename, ContentImporterContext context)
        {
            return this.Import(filename, context);
        }
    }
}
