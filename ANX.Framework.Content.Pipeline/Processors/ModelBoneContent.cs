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
        public ModelBoneContentCollection Children 
        { 
            get; 
            private set; 
        }

        public int Index
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ModelBoneContent Parent
        {
            get;
            set;
        }

        public Matrix Transform
        {
            get;
            set;
        }

        public ModelBoneContent()
        {
            Children = new ModelBoneContentCollection();
        }
    }
}
