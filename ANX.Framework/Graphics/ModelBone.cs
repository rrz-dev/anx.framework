#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ModelBone
    {
        private ModelBoneCollection children;
        public ModelBoneCollection Children
        { 
            get { return children; }
            internal set { children = value; }
        }

        private int index;
        public int Index 
        {
            get { return index; }
        }

        private string name;
        public string Name 
        {
            get { return name; }
        }

        private ModelBone parent;
        public ModelBone Parent 
        { 
            get { return parent; }
            internal set { parent = value; }
        }

        private Matrix transform;
        public Matrix Transform 
        { 
            get { return transform; }
            set { transform = value; }
        }

        public ModelBone(string name, Matrix transform, int index)
        {
            this.name = name;
            this.transform = transform;
            this.index = index;
        }
    }
}
