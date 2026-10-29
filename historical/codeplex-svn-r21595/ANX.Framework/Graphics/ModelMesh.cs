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
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public sealed class ModelMesh
    {
        private readonly BoundingSphere boundingSphere;
         
        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
        }

        public ModelEffectCollection Effects { get; private set; }
        public ModelMeshPartCollection MeshParts { get; private set; }
        public string Name { get; private set; }
        public ModelBone ParentBone { get; private set; }
        public object Tag { get; set; }

        internal ModelMesh(string name, ModelBone bone, BoundingSphere sphere, ModelMeshPart[] meshParts, Object tag)
        {
            this.Name = name;
            this.ParentBone = bone;
            this.boundingSphere = sphere;
            this.Tag = tag;
            this.MeshParts = new ModelMeshPartCollection(meshParts);
            this.Effects = new ModelEffectCollection();

            foreach (var item in this.MeshParts)
            {
                item.parentMesh = this;
                if (item.Effect != null && Effects.Contains(item.Effect) == false)
                    Effects.Add(item.Effect);
            }
        }

        internal void EffectChangedOnMeshPart(ModelMeshPart part, Effect oldEffect, Effect newEffect)
        {
            bool oldEffectIsInUse = false;
            bool newEffectIsKnown = false;

            foreach (var item in MeshParts)
            {
                if (ReferenceEquals(item, part))
                    continue;

                if (ReferenceEquals(item.Effect, oldEffect))
                    oldEffectIsInUse = true;

                if (ReferenceEquals(item.Effect, newEffect))
                    newEffectIsKnown = true;
            }

            if (oldEffect != null && !oldEffectIsInUse)
                Effects.Remove(oldEffect);

            if (newEffect != null && !newEffectIsKnown)
                Effects.Add(newEffect);
        }

        public void Draw()
        {
            foreach (var part in MeshParts)
            {
                foreach (var pass in part.Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice graphics = part.VertexBuffer.GraphicsDevice;
                    graphics.SetVertexBuffer(part.VertexBuffer, part.VertexOffset);
                    graphics.Indices = part.IndexBuffer;
                    graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, part.NumVertices, part.StartIndex,
                        part.PrimitiveCount);
                }
            }
        }
    }
}
