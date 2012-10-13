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
    public sealed class Model
    {
        public ModelBoneCollection Bones { get; private set; }
        public ModelMeshCollection Meshes { get; private set; }
        public ModelBone Root { get; private set; }
        public object Tag { get; set; }

        private static Matrix[] cachedBonesArray;

        public Model(ModelBoneCollection bones, ModelMeshCollection meshes, ModelBone rootBone, Object tag)
        {
            this.Bones = bones;
            this.Meshes = meshes;
            this.Root = rootBone;
            this.Tag = tag;
        }

        public void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            if (destinationBoneTransforms == null)
            {
                throw new ArgumentNullException("destinationBoneTransforms");
            }

            if (destinationBoneTransforms.Length < Bones.Count)
            {
                throw new ArgumentOutOfRangeException("destinationBoneTransforms");
            }

            for (int i = 0; i < Bones.Count; i++)
            {
                var bone = Bones[i];
                if (bone.Parent == null)
                {
                    destinationBoneTransforms[i] = bone.Transform;
                }
                else
                {
                    int parentIndex = bone.Parent.Index;
                    destinationBoneTransforms[i] = bone.Transform * destinationBoneTransforms[parentIndex];
                }
            }
        }

        public void CopyBoneTransformsFrom(Matrix[] sourceBoneTransforms)
        {
            if (sourceBoneTransforms == null)
            {
                throw new ArgumentNullException("sourceBoneTransforms");
            }

            if (sourceBoneTransforms.Length < Bones.Count)
            {
                throw new ArgumentOutOfRangeException("sourceBoneTransforms");
            }

            for (int i = 0; i < Bones.Count; i++)
            {
                Bones[i].Transform = sourceBoneTransforms[i];
            }
        }

        public void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            if (destinationBoneTransforms == null)
            {
                throw new ArgumentNullException("destinationBoneTransforms");
            }

            if (destinationBoneTransforms.Length < Bones.Count)
            {
                throw new ArgumentOutOfRangeException("destinationBoneTransforms");
            }

            for (int i = 0; i < Bones.Count; i++)
            {
                destinationBoneTransforms[i] = Bones[i].Transform;
            }
        }

        public void Draw (Matrix world, Matrix view, Matrix projection)
        {
            Matrix[] absTransforms = Model.cachedBonesArray;
            if (absTransforms == null || absTransforms.Length < this.Bones.Count)
            {
                Array.Resize<Matrix>(ref absTransforms, this.Bones.Count);
                Model.cachedBonesArray = absTransforms;
            }
            this.CopyAbsoluteBoneTransformsTo(absTransforms);

            foreach (var mesh in Meshes)
            {
                foreach (var effect in mesh.Effects)
                {
                    IEffectMatrices matEffect = effect as IEffectMatrices;
                    if (matEffect != null)
                    {
                        matEffect.World = absTransforms[mesh.ParentBone.Index] * world;
                        matEffect.View = view;
                        matEffect.Projection = projection;
                    }
                }

                mesh.Draw();
            }
        }
    }
}
