#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class ModelBone
    {
        public ModelBoneCollection Children { get; internal set; }
        public int Index { get; private set; }
        public string Name { get; private set; }
        public ModelBone Parent { get; internal set; }
        public Matrix Transform { get; set; }

        public ModelBone(string name, Matrix transform, int index)
        {
            this.Name = name;
            this.Transform = transform;
            this.Index = index;
        }
    }
}
