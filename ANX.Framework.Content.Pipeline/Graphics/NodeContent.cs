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
    /// <summary>
    /// Provides a base class for graphics types that define local coordinate systems. 
    /// </summary>
    /// <remarks>These objects can be arranged in a tree structure. This enables the root transform to be automatically inherited from the parent object.</remarks>
    public class NodeContent : ContentItem
    {
        /// <summary>
        /// Gets the value of the local <see cref="Transform"/> property, multiplied by the <see cref="AbsoluteTransform"/> of the parent. 
        /// </summary>
        /// <value>Matrix of the <see cref="NodeContent"/> object. </value>
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

        //TODO: offer useful animation system, XNA missed to implement one.
        /// <summary>
        /// Gets the set of animations belonging to this node. 
        /// </summary>
        /// <value>Collection of animations for this content item.</value>
        public AnimationContentDictionary Animations
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the children of the <see cref="NodeContent"/> object.
        /// </summary>
        /// <value>Collection of children.</value>
        public NodeContentCollection Children
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the parent of this <see cref="NodeContent"/> object. 
        /// </summary>
        /// <value>Parent of the <see cref="NodeContent"/> object, or null if this object is the root of the scene.</value>
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
            Animations = new AnimationContentDictionary();
            Children = new NodeContentCollection(this);
        }
    }
}
