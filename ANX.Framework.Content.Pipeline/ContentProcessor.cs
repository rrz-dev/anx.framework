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
        public abstract TOutput Process(TInput input, ContentProcessorContext context);

        Type IContentProcessor.InputType
        {
            get 
            { 
                return typeof(TInput); 
            }
        }

        Type IContentProcessor.OutputType
        {
            get 
            { 
                return typeof(TOutput); 
            }
        }

        object IContentProcessor.Process(object input, ContentProcessorContext context)
        {
            return Process((TInput)input, context);
        }
    }
}
