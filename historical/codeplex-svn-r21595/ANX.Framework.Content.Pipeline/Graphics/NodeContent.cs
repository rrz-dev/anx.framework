#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class NodeContent : ContentItem
    {
        public Matrix AbsolutTransform
        {
            get
            {
                if (Parent != null)
                {
                    return this.Transform * Parent.AbsolutTransform;
                }
                return this.Transform;
            }
        }

        public AnimationContentDictionary Animation
        {
            get;
            private set;
        }

        public NodeContentCollection Children
        {
            get;
            private set;
        }

        public NodeContent Parent
        {
            get;
            set;
        }

        public Matrix Transform
        {
            get;
            set;
        }

        public NodeContent()
        {
            Transform = Matrix.Identity;
            Animation = new AnimationContentDictionary();
            Children = new NodeContentCollection(this);
        }
    }
}
