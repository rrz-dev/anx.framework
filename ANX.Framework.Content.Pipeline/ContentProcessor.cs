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
    public abstract class ContentProcessor<TInput, TOutput> : IContentProcessor
    {
        protected ContentProcessor()
        {
            throw new NotImplementedException();
        }

        public abstract TOutput Process(TInput input, ContextProcessorContext context);

        Type IContentProcessor.InputType
        {
            get { throw new NotImplementedException(); }
        }

        Type IContentProcessor.OutputType
        {
            get { throw new NotImplementedException(); }
        }

        object IContentProcessor.Process(object input, ContextProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
