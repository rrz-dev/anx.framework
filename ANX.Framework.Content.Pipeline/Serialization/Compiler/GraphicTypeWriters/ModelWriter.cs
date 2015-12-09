using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [ContentTypeWriter]
    internal class ModelWriter : BuiltinTypeWriter<ModelContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(Model).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, ModelContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            this.WriteBones(output, value);
            this.WriteMeshes(output, value);
            this.WriteBoneIndex(output, value, value.Root);
            output.WriteObject<object>(value.Tag);
        }

        private void WriteBones(ContentWriter output, ModelContent model)
        {
            //Write bone data, except indices.
            output.Write(model.Bones.Count);
            foreach (var bone in model.Bones)
            {
                output.WriteObject<string>(bone.Name);
                output.Write(bone.Transform);
            }

            //Write bone indices.
            foreach (var bone in model.Bones)
            {
                this.WriteBoneIndex(output, model, bone.Parent);
                output.Write(bone.Children.Count);

                foreach (ModelBoneContent child in bone.Children)
                {
                    this.WriteBoneIndex(output, model, child);
                }
            }
        }

        private void WriteMeshes(ContentWriter output, ModelContent model)
        {
            output.Write(model.Meshes.Count);
            foreach (var mesh in model.Meshes)
            {
                output.WriteObject<string>(mesh.Name);
                this.WriteBoneIndex(output, model, mesh.ParentBone);
                output.Write(mesh.BoundingSphere.Center);
                output.Write(mesh.BoundingSphere.Radius);
                output.WriteObject<object>(mesh.Tag);

                this.WriteMeshParts(output, mesh.MeshParts);
            }
        }

        private void WriteMeshParts(ContentWriter output, IList<ModelMeshPartContent> meshParts)
        {
            output.Write(meshParts.Count);
            foreach (ModelMeshPartContent current in meshParts)
            {
                output.Write(current.VertexOffset);
                output.Write(current.NumVertices);
                output.Write(current.StartIndex);
                output.Write(current.PrimitiveCount);
                output.WriteObject<object>(current.Tag);
                output.WriteSharedResource<VertexBufferContent>(current.VertexBuffer);
                output.WriteSharedResource<IndexCollection>(current.IndexBuffer);
                output.WriteSharedResource<MaterialContent>(current.Material);
            }
        }

        private void WriteBoneIndex(ContentWriter output, ModelContent model, ModelBoneContent bone)
        {
            //If the number of bones can be placed in a byte, we have to write the indices into bytes too, otherwise we write ints.
            //To make sure we can write into a byte, we start the normal indices at 1, that means if a bone has an index of -1, we can still write it.
            //We could make that only for the case where we write a byte, but XNA did it that way and we want to stay compatible with XNA, at least to a certain degree.
            int boneIndex;
            if (bone == null)
                boneIndex = 0;
            else
            {
                if (bone.Index < -1 || bone.Index >= model.Bones.Count)
                    throw new ArgumentOutOfRangeException(string.Format("Index of bone \"{0}\" is either less than -1 or greater or equal to the count of childs in his parent.", bone.Name));

                boneIndex = bone.Index + 1;
            }

            //The highest possible index is Count - 1, but the reader checks if the bones count is less than 255, so I just made the same check.
            if (model.Bones.Count < byte.MaxValue)
            {
                output.Write((byte)boneIndex);
            }
            else
            {
                output.Write(boneIndex);
            }
        }


        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(Model), targetPlatform);
        }
    }
}
