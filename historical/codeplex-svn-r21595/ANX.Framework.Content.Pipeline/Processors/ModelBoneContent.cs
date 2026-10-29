#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public sealed class ModelBoneContent
    {
        public ModelBoneContentCollection Children { get; set; }

        public int Index
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ModelBoneContent Parent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Matrix Transform
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }
}
