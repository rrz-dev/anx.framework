#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.ContentPipeline;
using ANX.Framework.NonXNA;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License


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
                    reader.ReadSharedResource<VertexBuffer>((buffer) => { meshPart.VertexBuffer = buffer; });
                    reader.ReadSharedResource<IndexBuffer>((buffer) => { meshPart.IndexBuffer = buffer; });
                    reader.ReadSharedResource<Effect>((effect) => { meshPart.Effect = effect; });
                    meshParts[j] = meshPart;
                }

                meshes[i] = new ModelMesh(name, parentBone, boundingSphere, meshParts, meshTag);
            }

            return new ModelMeshCollection(meshes);
        }
    }
}
