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
    public class ContentItem
    {
        public ContentItem()
        {
            throw new NotImplementedException();
        }

        public ContentIdentity Identity
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public OpaqueDataDictionary OpaqueData
        {
            get;
            private set;
        }


    }
}
