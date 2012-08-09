#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.ContentPipeline;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    public class ModelReader : ContentTypeReader<Model>
    {
        protected internal override Model Read(ContentReader input, Model existingInstance)
        {
            IServiceProvider service = input.ContentManager.ServiceProvider;

            var rfc = service.GetService(typeof(IRenderSystemCreator)) as IRenderSystemCreator;
            if (rfc == null)
            {
                throw new ContentLoadException("Service not found IRenderFrameworkCreator");
            }

            var gds = service.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            if (gds == null)
            {
                throw new ContentLoadException("Service not found IGraphicsDeviceService");
            }

            ModelBoneCollection bones = ReadBones(input);
            ModelMeshCollection meshes = ReadMeshes(input, bones);
            ModelBone rootBone = ReadBoneReference(input, bones);
            object tag = input.ReadObject<object>();

            return new Model(bones, meshes, rootBone, tag);
        }

        private ModelBoneCollection ReadBones(ContentReader reader)
        {
            int boneCount = reader.ReadInt32();
            ModelBone[] bones = new ModelBone[boneCount];
            for (int i = 0; i < boneCount; i++)
            {
                string name = reader.ReadObject<String>();
                Matrix transform = reader.ReadMatrix();
                bones[i] = new ModelBone(name, transform, i);
            }
            for (int i = 0; i < boneCount; i++)
            {
                ModelBone bone = bones[i];
                bone.Parent = ReadBoneReference(reader, bones);

                int childCount = reader.ReadInt32();
                ModelBone[] childBones = new ModelBone[childCount];
                for (int c = 0; c < childCount; c++)
                {
                    childBones[c] = ReadBoneReference(reader, bones);
                }

                bone.Children = new ModelBoneCollection(childBones);
            }

            return new ModelBoneCollection(bones);
        }

        private int ReadBoneIndex(ContentReader reader, int bones)
        {
            int index = 0;
            if (bones < 255)
            {
                index = (int)reader.ReadByte();
            }
            else
            {
                index = (int)reader.ReadInt32();
            }
            return index - 1;
        }

        private ModelBone ReadBoneReference(ContentReader reader, ModelBone[] bones)
        {
            ModelBone reference = null;
            int index = ReadBoneIndex(reader, bones.Length);
            if (index >= 0 && index < bones.Length)
            {
                reference = bones[index];
            }
            return reference;
        }

        private ModelBone ReadBoneReference(ContentReader reader, ModelBoneCollection bones)
        {
            ModelBone reference = null;
            int index = ReadBoneIndex(reader, bones.Count);
            if (index >= 0)
            {
                reference = bones[index];
            }
            return reference;
        }

        private ModelMeshCollection ReadMeshes(ContentReader reader, ModelBoneCollection bones)
        {
            int meshCount = reader.ReadInt32();
            ModelMesh[] meshes = new ModelMesh[meshCount];
            for (int i = 0; i < meshCount; i++)
            {
                string name = reader.ReadObject<String>();
                ModelBone parentBone = ReadBoneReference(reader, bones);
                BoundingSphere boundingSphere = new BoundingSphere();
                boundingSphere.Center = reader.ReadVector3();
                boundingSphere.Radius = reader.ReadSingle();
                object meshTag = reader.ReadObject<object>();

                int meshPartCount = reader.ReadInt32();
                ModelMeshPart[] meshParts = new ModelMeshPart[meshPartCount];
                for (int j = 0; j < meshPartCount; j++)
                {
                    int vertexOffset = reader.ReadInt32();
                    int numVertices = reader.ReadInt32();
                    int startIndex = reader.ReadInt32();
                    int primitiveCount = reader.ReadInt32();
                    object meshPartTag = reader.ReadObject<object>();

                    ModelMeshPart meshPart = new ModelMeshPart(vertexOffset, numVertices, startIndex, primitiveCount, meshPartTag);
                    reader.ReadSharedResource<VertexBuffer>((buffer) => { 
                        meshPart.VertexBuffer = buffer; 
                    });
                    reader.ReadSharedResource<IndexBuffer>((buffer) => { 
                        meshPart.IndexBuffer = buffer; 
                    });
                    reader.ReadSharedResource<Effect>((effect) => { 
                        meshPart.Effect = effect; 
                    });
                    meshParts[j] = meshPart;
                }

                meshes[i] = new ModelMesh(name, parentBone, boundingSphere, meshParts, meshTag);
            }

            return new ModelMeshCollection(meshes);
        }
    }
}
