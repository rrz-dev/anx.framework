#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;
using ANX.Framework.Graphics.PackedVector;
using ANX.Framework.Graphics;
using ANX.Framework.Content.Pipeline.Helpers;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Model Processor - ANX Framework")]
    public class ModelProcessor : ContentProcessor<NodeContent, ModelContent>
    {
        [DefaultValue(typeof(Color), "255; 0; 255; 255")]
        public virtual Color ColorKeyColor { get; set; }

        [DefaultValue(true)]
        public virtual bool ColorKeyEnabled { get; set; }

        [DefaultValue(MaterialProcessorDefaultEffect.BasicEffect)]
        public virtual MaterialProcessorDefaultEffect DefaultEffect { get; set; }

        [DefaultValue(true)]
        public virtual bool GenerateMipmaps { get; set; }

        [DefaultValue(false)]
        public virtual bool GenerateTangentFrames { get; set; }

        [DefaultValue(true)]
        public virtual bool PremultiplyTextureAlpha { get; set; }

        [DefaultValue(true)]
        public virtual bool PremultiplyVertexColors { get; set; }

        [DefaultValue(false)]
        public virtual bool ResizeTexturesToPowerOfTwo { get; set; }

        [DefaultValue(0f)]
        public virtual float RotationX { get; set; }

        [DefaultValue(0f)]
        public virtual float RotationY { get; set; }

        [DefaultValue(0f)]
        public virtual float RotationZ { get; set; }

        [DefaultValue(1f)]
        public virtual float Scale { get; set; }

        [DefaultValue(false)]
        public virtual bool SwapWindingOrder { get; set; }

        [DefaultValue(TextureProcessorOutputFormat.DxtCompressed)]
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public ModelProcessor()
        {
            ColorKeyColor = Color.FromNonPremultiplied(255, 0, 255, 255);
            ColorKeyEnabled = true;
            DefaultEffect = MaterialProcessorDefaultEffect.BasicEffect;
            GenerateMipmaps = true;
            GenerateTangentFrames = false;
            PremultiplyTextureAlpha = true;
            PremultiplyVertexColors = true;
            ResizeTexturesToPowerOfTwo = false;
            RotationX = 0f;
            RotationY = 0f;
            RotationZ = 0f;
            Scale = 1f;
            SwapWindingOrder = false;
            TextureFormat = TextureProcessorOutputFormat.DxtCompressed;
        }

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (context == null)
                throw new ArgumentNullException("context");

            Matrix transform = Matrix.Identity;
            if (this.RotationZ != 0f)
                transform *= Matrix.CreateRotationZ(this.RotationZ);

            if (this.RotationX != 0f)
                transform *= Matrix.CreateRotationX(this.RotationX);

            if (this.RotationY != 0f)
                transform *= Matrix.CreateRotationY(this.RotationY);

            if (this.Scale != 1f)
                transform *= Matrix.CreateScale(this.Scale);

            if (transform != Matrix.Identity)
                MeshHelper.TransformScene(input, transform);

            Dictionary<NodeContent, ModelBoneContent> convertedBones = new Dictionary<NodeContent, ModelBoneContent>();
            var rootBone = MeshHelper.FindSkeleton(input);
            if (rootBone == null)
            {
                //Create a ModelBoneContent representing the rootBone.
                SetupParentBoneHierarchy(input, convertedBones);
            }
            else
                ConvertBones(MeshHelper.FlattenSkeleton(rootBone), convertedBones);

            
            var convertedRootBone = convertedBones.First().Value;

            var meshes = NodeContentHelper.EnumNodesOfType<MeshContent>(input);

            this.ProcessGeometryUsingMaterials(meshes, context);
            
            var convertedMeshes = ConvertMeshes(meshes, context, convertedBones);

            return new ModelContent(convertedRootBone, new List<ModelBoneContent>(convertedBones.Values), new List<ModelMeshContent>(convertedMeshes));
        }

        private IEnumerable<ModelMeshContent> ConvertMeshes(IEnumerable<MeshContent> meshes, ContentProcessorContext context, Dictionary<NodeContent, ModelBoneContent> convertedBones)
        {
            if (meshes == null)
                throw new ArgumentNullException("geometries");

            Dictionary<MeshContent, ModelMeshContent> result = new Dictionary<MeshContent, ModelMeshContent>();
            ConvertMeshes(meshes, context, result, convertedBones);

            return result.Values;
        }

        private void ConvertMeshes(IEnumerable<MeshContent> meshes, ContentProcessorContext context, Dictionary<MeshContent, ModelMeshContent> convertedMeshes, Dictionary<NodeContent, ModelBoneContent> convertedBones)
        {
            Dictionary<MaterialContent, MaterialContent> convertedMaterials = new Dictionary<MaterialContent, MaterialContent>();
            foreach (var mesh in meshes)
            {
                ModelMeshContent convertedMesh;
                if (!convertedMeshes.TryGetValue(mesh, out convertedMesh))
                {
                    MeshHelper.MergeDuplicatePositions(mesh, 0f);
                    MeshHelper.MergeDuplicateVertices(mesh);

                    if (this.GenerateTangentFrames)
                    {
                        bool containsTangents = !mesh.Geometry.Any((x) => x.Vertices.Channels.Contains(VertexChannelNames.Tangent(0)));
                        bool containsBinormals = !mesh.Geometry.Any((x) => x.Vertices.Channels.Contains(VertexChannelNames.Binormal(0)));
                        if (containsTangents || containsBinormals)
                        {
                            string tangentChannelName = containsTangents ? VertexChannelNames.Tangent(0) : null;
                            string binormalChannelName = containsBinormals ? VertexChannelNames.Binormal(0) : null;
                            MeshHelper.CalculateTangentFrames(mesh, VertexChannelNames.TextureCoordinate(0), tangentChannelName, binormalChannelName);
                        }
                    }

                    if (this.SwapWindingOrder)
                    {
                        MeshHelper.SwapWindingOrder(mesh);
                    }

                    MeshHelper.CalculateNormals(mesh, false);

                    ProcessVertexChannels(mesh, context);
                    MeshHelper.OptimizeForCache(mesh);

                    convertedMesh = new ModelMeshContent()
                    {
                        Name = mesh.Name,
                        SourceMesh = mesh,
                        BoundingSphere = BoundingSphere.CreateFromPoints(mesh.Positions),
                    };

                    convertedMesh.ParentBone = SetupParentBoneHierarchy(mesh, convertedBones);

                    foreach (var geometry in mesh.Geometry)
                    {
                        MaterialContent convertedMaterial = null;
                        if (geometry.Material != null)
                        {
                            if (!convertedMaterials.TryGetValue(geometry.Material, out convertedMaterial))
                            {
                                convertedMaterial = this.ConvertMaterial(geometry.Material, context);
                                convertedMaterials.Add(geometry.Material, convertedMaterial);
                            }
                        }
                        
                        var vertexBuffer = geometry.Vertices.CreateVertexBuffer();
                        IndexCollection indices = new IndexCollection();
                        indices.AddRange(geometry.Indices);
                        
                        ModelMeshPartContent convertedGeometry = new ModelMeshPartContent()
                        {
                            Material = convertedMaterial,
                            VertexBuffer = vertexBuffer,
                            NumVertices = geometry.Vertices.VertexCount,
                            PrimitiveCount = geometry.Indices.Count / 3,
                            VertexOffset = 0, //All have custom vertex buffer, so no vertex offset.
                            StartIndex = 0,
                            IndexBuffer = indices,
                        };

                        convertedMesh.MeshParts.Add(convertedGeometry);
                    }

                    convertedMeshes.Add(mesh, convertedMesh);
                }
            }
        }

        private ModelBoneContent SetupParentBoneHierarchy(NodeContent node, Dictionary<NodeContent, ModelBoneContent> convertedBones)
        {
            if (node == null)
                return null;

            ModelBoneContent convertedBone;
            if (convertedBones.TryGetValue(node, out convertedBone))
                return convertedBone;
            
            if (node is BoneContent)
            {
                BoneContent bone = (BoneContent)node;
                return ConvertBone(bone, convertedBones);
            }
            else
            {
                //Parent of mesh is not a bone, but the structure of the modelContent requires a bone parent for every mesh, so we have to create one and insert it into the hierarchy.
                convertedBone = new ModelBoneContent()
                {
                    Name = node.Name,
                    Transform = node.Transform,
                    Index = convertedBones.Count,
                };

                //If another mesh finds us as its parent, it will try to reuse our newly created bone.
                convertedBones.Add(node, convertedBone);

                convertedBone.Parent = SetupParentBoneHierarchy(node.Parent, convertedBones);
                if (convertedBone.Parent != null)
                    convertedBone.Parent.Children.Add(convertedBone);

                return convertedBone;
            }
        }

        private IEnumerable<ModelBoneContent> ConvertBones(IEnumerable<BoneContent> bones)
        {
            if (bones == null)
                throw new ArgumentNullException("bones");

            Dictionary<NodeContent, ModelBoneContent> result = new Dictionary<NodeContent, ModelBoneContent>();
            ConvertBones(bones, result);

            return result.Values;
        }

        private void ConvertBones(IEnumerable<BoneContent> bones, Dictionary<NodeContent, ModelBoneContent> convertedDict)
        {
            foreach (var bone in bones)
            {
                ModelBoneContent convertedBone;
                if (!convertedDict.TryGetValue(bone, out convertedBone))
                {
                    convertedBone = new ModelBoneContent()
                    {
                        Name = bone.Name,
                        Transform = bone.Transform,
                        Index = convertedDict.Count,
                    };

                    convertedDict.Add(bone, convertedBone);

                    if (bone.Parent is BoneContent)
                    {
                        convertedBone.Parent = ConvertBone((BoneContent)bone.Parent, convertedDict);
                    }

                    BoneContent[] boneChilds = bone.Children.OfType<BoneContent>().ToArray();
                    ConvertBones(boneChilds, convertedDict);

                    foreach (var child in boneChilds)
                    {
                        var convertedChild = convertedDict[child];

                        convertedBone.Children.Add(convertedChild);
                    }
                }
            }
        }

        private ModelBoneContent ConvertBone(BoneContent bone, Dictionary<NodeContent, ModelBoneContent> convertedDict)
        {
            ModelBoneContent result;
            if (!convertedDict.TryGetValue(bone, out result))
            {
                result = new ModelBoneContent()
                {
                    Name = bone.Name,
                    Transform = bone.Transform
                };

                //TODO: check on duplicates
                //TODO: handle animations

                convertedDict.Add(bone, result);
            }

            return result;
        }

        /// <summary>
        /// Called by the framework when the MaterialContent property of a GeometryContent object is encountered in the input node collection. 
        /// </summary>
        /// <param name="material">The input material content.</param>
        /// <param name="context">Context for the specified processor.</param>
        /// <returns>The converted material content.</returns>
        protected virtual MaterialContent ConvertMaterial(MaterialContent material, ContentProcessorContext context)
        {
            OpaqueDataDictionary opaqueDataDictionary = new OpaqueDataDictionary();

            opaqueDataDictionary["ColorKeyColor"] = this.ColorKeyColor;
            opaqueDataDictionary["ColorKeyEnabled"] = this.ColorKeyEnabled;
            opaqueDataDictionary["DefaultEffect"] = this.DefaultEffect;
            opaqueDataDictionary["GenerateMipmaps"] = this.GenerateMipmaps;
            opaqueDataDictionary["PremultiplyTextureAlpha"] = this.PremultiplyTextureAlpha;
            opaqueDataDictionary["ResizeTexturesToPowerOfTwo"] = this.ResizeTexturesToPowerOfTwo;
            opaqueDataDictionary["TextureFormat"] = this.TextureFormat;

            return context.Convert<MaterialContent, MaterialContent>(material, typeof(MaterialProcessor).Name, opaqueDataDictionary);
        }

        /// <summary>
        /// Maps the geometries in the meshes to their materials and calls <see cref="ProcessGeometryUsingMaterial"/> with it.
        /// </summary>
        /// <param name="meshes"></param>
        /// <param name="context"></param>
        private void ProcessGeometryUsingMaterials(IEnumerable<MeshContent> meshes, ContentProcessorContext context)
        {
            Dictionary<MaterialContent, List<GeometryContent>> materialGeometryAssoc = new Dictionary<MaterialContent, List<GeometryContent>>();
            //We can't add null as key to a dictionary so we must have a seperate list where all the geometries without materials go.
            List<GeometryContent> materiallessGeometries = new List<GeometryContent>();

            foreach (var mesh in meshes)
            {
                foreach (var geometry in mesh.Geometry)
                {
                    MaterialContent material = geometry.Material;
                    if (material != null)
                    {
                        List<GeometryContent> materialGeometries;
                        if (!materialGeometryAssoc.TryGetValue(material, out materialGeometries))
                        {
                            materialGeometries = new List<GeometryContent>();
                            materialGeometryAssoc.Add(material, materialGeometries);
                        }
                        materialGeometries.Add(geometry);
                    }
                    else
                    {
                        materiallessGeometries.Add(geometry);
                    }
                }
            }

            foreach (var pair in materialGeometryAssoc)
            {
                this.ProcessGeometryUsingMaterial(pair.Key, pair.Value, context);
            }

            if (materiallessGeometries.Count > 0)
            {
                this.ProcessGeometryUsingMaterial(null, materiallessGeometries, context);
            }
        }

        /// <summary>
        /// Processes all geometry using a specified material. 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="geometryContent">A collection of all the geometry using the specified material.</param>
        /// <param name="context"></param>
        protected virtual void ProcessGeometryUsingMaterial(MaterialContent material, IEnumerable<GeometryContent> geometryContent, ContentProcessorContext context)
        {
            if (material == null)
            {
                material = new BasicMaterialContent();
                foreach (var geometry in geometryContent)
                {
                    geometry.Material = material;
                }
            }

            MaterialProcessorDefaultEffect? materialProcessorDefaultEffect = null;
            if (material is BasicMaterialContent)
            {
                materialProcessorDefaultEffect = this.DefaultEffect;
            }
            else if (material is SkinnedMaterialContent)
            {
                materialProcessorDefaultEffect = MaterialProcessorDefaultEffect.SkinnedEffect;
            }
            else if (material is EnvironmentMapMaterialContent)
            {
                materialProcessorDefaultEffect = MaterialProcessorDefaultEffect.EnvironmentMapEffect;
            }
            else if (material is DualTextureMaterialContent)
            {
                materialProcessorDefaultEffect = MaterialProcessorDefaultEffect.DualTextureEffect;
            }
            else if (material is AlphaTestMaterialContent)
            {
                materialProcessorDefaultEffect = MaterialProcessorDefaultEffect.AlphaTestEffect;
            }

            if (materialProcessorDefaultEffect.HasValue)
            {
                bool vertexColorEnabled = false;
                int blendChannelCount = 0;
                int textureChannelCount = 0;
                switch (materialProcessorDefaultEffect.Value)
                {
                    case MaterialProcessorDefaultEffect.SkinnedEffect:
                        textureChannelCount = 1;
                        blendChannelCount = 1;
                        break;
                    case MaterialProcessorDefaultEffect.EnvironmentMapEffect:
                        textureChannelCount = 1;
                        break;
                    case MaterialProcessorDefaultEffect.DualTextureEffect:
                        textureChannelCount = 2;
                        vertexColorEnabled = true;
                        break;
                    case MaterialProcessorDefaultEffect.AlphaTestEffect:
                        textureChannelCount = 1;
                        vertexColorEnabled = true;
                        break;
                    default:
                        textureChannelCount = material.Textures.ContainsKey("Texture") ? 1 : 0;
                        vertexColorEnabled = true;
                        break;
                }

                foreach (var geometry in geometryContent)
                {
                    ValidateTextureChannelsExist(geometry, textureChannelCount);
                    ValidateWeightsChannelsExist(geometry, blendChannelCount);
                }

                if (vertexColorEnabled)
                {
                    //SetVertexColorEnabled(material, geometryContent);
                }
            }
        }

        private void SetVertexColorEnabled(MaterialContent material, IEnumerable<GeometryContent> geometryContent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the wanted number of texture channels exist within the geometry and if not an <see cref="InvalidContentException"/> gets thrown.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="channelCount"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidContentException"></exception>
        protected void ValidateTextureChannelsExist(GeometryContent geometry, int channelCount)
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");

            var channels = geometry.Vertices.Channels;
            for (int i = 0; i < channelCount; i++)
            {
                if (!channels.Contains(VertexChannelNames.TextureCoordinate(i)))
                {
                    throw new InvalidContentException(string.Format("The mesh \"{0}\", contains geometry that is missing texture coordinates for channel {1}.", geometry.Parent.Name, i), geometry.Identity);
                }
            }
        }

        /// <summary>
        /// Checks if the wanted number of blend index and blend weight channels exist within the geometry and if not an <see cref="InvalidContentException"/> gets thrown.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="channelCount"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidContentException"></exception>
        protected void ValidateWeightsChannelsExist(GeometryContent geometry, int channelCount)
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");

            var channels = geometry.Vertices.Channels;
            for (int i = 0; i < channelCount; i++)
            {
                if (!channels.Contains(VertexChannelNames.EncodeName(VertexElementUsage.BlendIndices, i)) | 
                    !channels.Contains(VertexChannelNames.EncodeName(VertexElementUsage.BlendWeight, i)))
                {
                    throw new InvalidContentException(string.Format("The mesh \"{0}\", contains geometry that is missing vertex weights for channel {1}.", geometry.Parent.Name, i), geometry.Identity);
                }
            }
        }

        private void ProcessVertexChannels(MeshContent mesh, ContentProcessorContext context)
        {
            foreach (var geometry in mesh.Geometry)
            {
                VertexChannelCollection channels = geometry.Vertices.Channels;

                for (int i = 0; i < channels.Count; i++)
                {
                    this.ProcessVertexChannel(geometry, i, context);
                }
            }
        }

        /// <summary>
        /// Processes geometry content vertex channels at the specified index. 
        /// </summary>
        /// <remarks>
        /// This function will be called for each VertexChannel of Vertices found in the input mesh. 
        /// Subclasses of ModelProcessor can override ProcessVertexChannel to control how vertex data is processed. 
        /// The default implementation converts VertexElementUsage.Color channels to Color format, and replaces Weights channels with a 
        /// pair of VertexElementUsage.BlendIndices and VertexElementUsage.BlendIndices channels (using Byte4 format for the
        /// VertexElementUsage.BlendWeight, Color for the VertexElementUsage.BlendWeight, and discarding excess weights if there are more 
        /// than four per vertex). 
        /// </remarks>
        /// <param name="geometry"></param>
        /// <param name="vertexChannelIndex"></param>
        /// <param name="context"></param>
        protected virtual void ProcessVertexChannel(GeometryContent geometry, int vertexChannelIndex, ContentProcessorContext context)
        {
            switch (VertexChannelNames.DecodeBaseName(geometry.Vertices.Channels[vertexChannelIndex].Name))
            {
                case "Color":
                    this.ProcessColorChannel(geometry, vertexChannelIndex, context);
                    break;
                case "Weights":
                     this.ProcessWeightsChannel(geometry, vertexChannelIndex, context);
                    break;
            }
        }

        /// <summary>
        /// Processes the channel inside the geometry as a color channel.
        /// <remarks>Gets called from <see cref="ProcessVertexChannel"/>.</remarks>
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="vertexChannelIndex"></param>
        /// <param name="context"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidContentException"></exception>
        protected virtual void ProcessColorChannel(GeometryContent geometry, int vertexChannelIndex, ContentProcessorContext context)
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");

            VertexChannelCollection channels = geometry.Vertices.Channels;
            try
            {
                channels.ConvertChannelContent<Color>(vertexChannelIndex);
            }
            catch (Exception exc)
            {
                throw new InvalidContentException(string.Format("Can't convert vertex channel \"{0}\" to element type {0} for color channel.", channels[vertexChannelIndex].Name, typeof(Color)), exc);
            }

            if (this.PremultiplyVertexColors)
            {
                VertexChannel<Color> vertexChannel = channels.Get<Color>(vertexChannelIndex);
                for (int i = 0; i < vertexChannel.Count; i++)
                {
                    Color color = vertexChannel[i];
                    vertexChannel[i] = Color.FromNonPremultiplied(color.R, color.G, color.B, color.A);
                }
            }
        }

        /// <summary>
        /// Processes the channel inside the geometry as a weights channel.
        /// <remarks>Gets called from <see cref="ProcessVertexChannel"/>.</remarks>
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="vertexChannelIndex"></param>
        /// <param name="context"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidContentException"></exception>
        protected virtual void ProcessWeightsChannel(GeometryContent geometry, int vertexChannelIndex, ContentProcessorContext context)
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");

            throw new NotImplementedException();
            //TODO: implement
            /*VertexChannelCollection channels = geometry.Vertices.Channels;
            VertexChannel vertexChannel = channels[vertexChannelIndex];
            if (!(vertexChannel is VertexChannel<BoneWeightCollection>))
            {
                throw new InvalidContentException(string.Format("Expected vertex channel \"{0}\" to contain BoneWeightCollection, but found {1}.", vertexChannel.Name, vertexChannel.ElementType));
            }

            VertexChannel<BoneWeightCollection> boneWeightsChannel = vertexChannel as VertexChannel<BoneWeightCollection>;

            Byte4[] indices = new Byte4[boneWeightsChannel.Count];
            Vector4[] weights = new Vector4[boneWeightsChannel.Count];
            for (int i = 0; i < boneWeightsChannel.Count; i++)
            {
                BoneWeightCollection boneWeightCollection = boneWeightsChannel[i];
                if (boneWeightCollection == null)
                {
                    throw new InvalidContentException(string.Format(CultureInfo.CurrentCulture, Resources.NullVertexChannelEntry, new object[]
            {
                boneWeightsChannel.Name,
                typeof(BoneWeightCollection).Name
            }));
                }

                ModelProcessor.ConvertVertexWeights(boneWeightCollection, indices, weights, i, geometry);
            }


            int usageIndex = VertexChannelNames.DecodeUsageIndex(boneWeightsChannel.Name);

            string blendIndicesText = VertexChannelNames.EncodeName(VertexElementUsage.BlendIndices, usageIndex);
            string blendWeightsText = VertexChannelNames.EncodeName(VertexElementUsage.BlendWeight, usageIndex);
            if (channels.Contains(blendIndicesText))
            {
                throw new InvalidContentException(string.Format(CultureInfo.CurrentCulture, Resources.ConvertWeightsOutputAlreadyExists, new object[]
        {
            boneWeightsChannel.Name,
            blendIndicesText
        }));
            }
            if (channels.Contains(blendWeightsText))
            {
                throw new InvalidContentException(string.Format(CultureInfo.CurrentCulture, Resources.ConvertWeightsOutputAlreadyExists, new object[]
        {
            boneWeightsChannel.Name,
            blendWeightsText
        }));
            }
            channels.Insert<Byte4>(vertexChannelIndex + 1, blendIndicesText, indices);
            channels.Insert<Vector4>(vertexChannelIndex + 2, blendWeightsText, weights);
            channels.RemoveAt(vertexChannelIndex);*/
        }

        /*private static void ConvertVertexWeights(BoneWeightCollection inputWeights, Byte4[] outputIndices, Vector4[] outputWeights, int vertexIndex, GeometryContent geometry)
        {
            inputWeights.NormalizeWeights(SkinnedEffect.MaxBonesPerVertex);
            for (int i = 0; i < inputWeights.Count; i++)
            {
                BoneWeight boneWeight = inputWeights[i];
                if (!boneIndices.TryGetValue(boneWeight.BoneName, out ModelProcessor.tempIndices[i]))
                {
                    throw new InvalidContentException(string.Format(CultureInfo.CurrentCulture, Resources.VertexHasUnknownBoneName, new object[]
            {
                boneWeight.BoneName
            }), geometry.Parent.Identity);
                }
                ModelProcessor.tempWeights[i] = boneWeight.Weight;
            }
            for (int j = inputWeights.Count; j < 4; j++)
            {
                ModelProcessor.tempIndices[j] = 0;
                ModelProcessor.tempWeights[j] = 0f;
            }
            outputIndices[vertexIndex] = new Byte4((float)ModelProcessor.tempIndices[0], (float)ModelProcessor.tempIndices[1], (float)ModelProcessor.tempIndices[2], (float)ModelProcessor.tempIndices[3]);
            outputWeights[vertexIndex] = new Vector4(ModelProcessor.tempWeights[0], ModelProcessor.tempWeights[1], ModelProcessor.tempWeights[2], ModelProcessor.tempWeights[3]);
        }*/
    }
}
