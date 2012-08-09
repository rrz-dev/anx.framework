#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class Model
    {
        private ModelBoneCollection bones;

        public ModelBoneCollection Bones
        {
            get { return bones; }
        }

        private ModelMeshCollection meshes;

        public ModelMeshCollection Meshes
        {
            get { return meshes; }
        }

        private ModelBone root;

        public ModelBone Root
        {
            get { return root; }
        }

        private Object tag;

        public Object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private static Matrix[] cachedBonesArray;

        public Model(ModelBoneCollection bones, ModelMeshCollection meshes, ModelBone rootBone, Object tag)
        {
            this.bones = bones;
            this.meshes = meshes;
            this.root = rootBone;
            this.tag = tag;
        }

        public void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            if (destinationBoneTransforms == null)
            {
                throw new ArgumentNullException("destinationBoneTransforms");
            }

            if (destinationBoneTransforms.Length < bones.Count)
            {
                throw new ArgumentOutOfRangeException("destinationBoneTransforms");
            }

            for (int i = 0; i < bones.Count; i++)
            {
                var bone = bones[i];
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

            if (sourceBoneTransforms.Length < bones.Count)
            {
                throw new ArgumentOutOfRangeException("sourceBoneTransforms");
            }

            for (int i = 0; i < bones.Count; i++)
            {
                bones[i].Transform = sourceBoneTransforms[i];
            }
        }

        public void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            if (destinationBoneTransforms == null)
            {
                throw new ArgumentNullException("destinationBoneTransforms");
            }

            if (destinationBoneTransforms.Length < bones.Count)
            {
                throw new ArgumentOutOfRangeException("destinationBoneTransforms");
            }

            for (int i = 0; i < bones.Count; i++)
            {
                destinationBoneTransforms[i] = bones[i].Transform;
            }
        }

        public void Draw (Matrix world, Matrix view, Matrix projection)
        {
            Matrix[] absTransforms = Model.cachedBonesArray;
            if (absTransforms == null || absTransforms.Length < this.bones.Count)
            {
                Array.Resize<Matrix>(ref absTransforms, this.bones.Count);
                Model.cachedBonesArray = absTransforms;
            }
            this.CopyAbsoluteBoneTransformsTo(absTransforms);

            foreach (var mesh in meshes)
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
