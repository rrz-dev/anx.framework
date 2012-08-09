#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ModelMesh
    {
        private BoundingSphere boundingSphere;
         
        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
        }

        private ModelEffectCollection effects;

        public ModelEffectCollection Effects
        {
            get { return effects; }
        }

        private ModelMeshPartCollection meshParts;

        public ModelMeshPartCollection MeshParts
        {
            get { return meshParts; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private ModelBone parentBone;

        public ModelBone ParentBone
        {
            get { return parentBone; }
        }

        private Object tag;

        public Object Tag
        {
            get { return tag; }
            set { this.tag = value; }
        }

        public ModelMesh(string name, ModelBone bone, BoundingSphere sphere, ModelMeshPart[] meshParts, Object tag)
        {
            this.name = name;
            this.parentBone = bone;
            this.boundingSphere = sphere;
            this.tag = tag;
            this.meshParts = new ModelMeshPartCollection(meshParts);
            this.effects = new ModelEffectCollection();

            foreach (var item in this.meshParts)
            {
                item.parentMesh = this;
                if (item.Effect != null && !this.effects.Contains(item.Effect))
                {
                    this.effects.Add(item.Effect);
                }
            }
        }

        internal void EffectChangedOnMeshPart(ModelMeshPart part, Effect oldEffect, Effect newEffect)
        {
            bool oldEffectIsInUse = false;
            bool newEffectIsKnown = false;

            foreach (var item in meshParts)
            {
                if (object.ReferenceEquals(item, part))
                {
                    continue;
                }

                if (object.ReferenceEquals(item.Effect, oldEffect))
                {
                    oldEffectIsInUse = true;
                }

                if (object.ReferenceEquals(item.Effect, newEffect))
                {
                    newEffectIsKnown = true;
                }
            }

            if (oldEffect != null && !oldEffectIsInUse)
            {
                effects.Remove(oldEffect);
            }

            if (newEffect != null && !newEffectIsKnown)
            {
                effects.Add(newEffect);
            }
        }

        public void Draw()
        {
            foreach (var part in meshParts)
            {
                foreach (var pass in part.Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice graphics = part.VertexBuffer.GraphicsDevice;
                    graphics.SetVertexBuffer(part.VertexBuffer, part.VertexOffset);
                    graphics.Indices = part.IndexBuffer;
                    graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                }
            }
        }
    }
}
