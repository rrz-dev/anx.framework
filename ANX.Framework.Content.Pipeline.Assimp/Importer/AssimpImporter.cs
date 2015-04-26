#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.Content.Pipeline.Processors;
using System.Collections;
using Assimp;
using System.Diagnostics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    [Developer("Konstantin Koch")]
    [AssimpImporter(Category = "Model Files", DefaultProcessor = "ModelProcessor", DisplayName = "Assimp Importer - ANX Framework", CacheImportedData = true)]
    public class AssimpImporter : ContentImporter<NodeContent>
    {
        ContentImporterContext context;
        ContentIdentity identity;

        public AssimpImporter()
        {
        }

        public override NodeContent Import(string filename, ContentImporterContext context)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("filename");

            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;

            Debugger.Break();

            this.identity = new ContentIdentity(filename, this.GetType().Name);

            AssimpDeploy.DeployLibraries();

            Assimp.AssimpContext assimpContext = new AssimpContext();
            
            Scene scene = assimpContext.ImportFile(filename, PostProcessPreset.TargetRealTimeMaximumQuality | PostProcessSteps.MakeLeftHanded);
            
            return ConvertScene(scene);
        }

        private NodeContent ConvertScene(Scene scene)
        {
            var rootNode = Convert(scene, scene.RootNode);
            //var allNodes = WalkNodes(rootNode).ToDictionary((x) => x.Name);
            
            /*foreach (var camera in scene.Cameras)
            {
                ReplaceNode(rootNode, allNodes, Convert(camera));
            }

            foreach (var light in scene.Lights)
            {
                ReplaceNode(rootNode, allNodes, Convert(light));
            }*/

            //TODO: Support ANimation
            /*foreach (var animation in scene.Animations)
            {
                if (animation.TicksPerSecond == 0)
                {
                    this.context.Logger.LogWarning(null, null, string.Format("Animation \"{0}\" has no ticks per seconds specified. Unable to process animation.", animation.NodeAnimationChannels));
                    continue;
                }
                
                AnimationContent animationContent = new AnimationContent();
                animationContent.Duration = TimeSpan.FromSeconds(animation.DurationInTicks / animation.TicksPerSecond);

                foreach (var channel in animation.NodeAnimationChannels)
                {
                    

                    AnimationChannel animationChannelContent = new AnimationChannel();
                    animationChannelContent.Add(new AnimationKeyframe())
                    animationContent.Channels.Add();
                }
            }*/

            //return rootNode;
            return CollapseNodes(rootNode);
        }

        private NodeContent CollapseNodes(NodeContent content)
        {
            //Find all the single nodes and eat them.
            if (content.Children.Count == 1)
            {
                var nodes = WalkCollapsableChildren(content, (x) => !(x is MeshContent) && x.Children.Count == 1).ToArray();
                if (nodes.Length > 0)
                {
                    foreach (var nodeToEat in nodes.Take(nodes.Length - 1))
                    {
                        content.Transform *= nodeToEat.Transform;
                        content.OpaqueData.AddRange(nodeToEat.OpaqueData, overwrite: true);
                    }
                    content.Children.Clear();

                    var lastNode = nodes.Last();
                    lastNode.Transform = content.Transform * lastNode.Transform;
                    lastNode.OpaqueData.AddRange(content.OpaqueData, overwrite: true);
                    content = lastNode;
                }
            }
            else if (content.Children.Count > 1)
            {
                List<NodeContent> newChildren = new List<NodeContent>();
                foreach (var child in content.Children)
                {
                    var newChild = CollapseNodes(child);
                    newChild.Parent = null;
                    newChildren.Add(newChild);
                }
                content.Children.Clear();
                content.Children.AddRange(newChildren);
            }

            return content;
        }

        private NodeContent Convert(Camera camera)
        {
            NodeContent cameraNode = new NodeContent()
            {
                Name = camera.Name,
                Transform = Matrix.CreateLookAt(camera.Position.ToAnx(), (camera.Position + camera.Direction).ToAnx(), camera.Up.ToAnx()),
            };

            cameraNode.OpaqueData.Add("AspectRatio", camera.AspectRatio);
            cameraNode.OpaqueData.Add("ClipPlaneFar", camera.ClipPlaneFar);
            cameraNode.OpaqueData.Add("ClipPlaneNear", camera.ClipPlaneNear);
            cameraNode.OpaqueData.Add("FieldOfView", camera.FieldOfview);
            cameraNode.OpaqueData.Add("Up", camera.Up);
            cameraNode.OpaqueData.Add("Position", camera.Position);
            cameraNode.OpaqueData.Add("Direction", camera.Direction);

            return cameraNode;
        }

        private NodeContent Convert(Light light)
        {
            NodeContent node = new NodeContent()
            {
                Name = light.Name,
                Transform = Matrix.CreateFromYawPitchRoll(light.Direction.X, light.Direction.Y, light.Direction.Z) * Matrix.CreateTranslation(light.Position.ToAnx())
            };

            node.OpaqueData.Add("InnerConeAngle", light.AngleInnerCone);
            node.OpaqueData.Add("OuterConeAngle", light.AngleOuterCone);

            if (light.LightType != LightSourceType.Directional)
            {
                node.OpaqueData.Add("ConstantAttentuation", light.AttenuationConstant);
                node.OpaqueData.Add("LinearAttentuation", light.AttenuationLinear);
                node.OpaqueData.Add("QuadraticAttentuation", light.AttenuationQuadratic);
            }

            node.OpaqueData.Add("AmbientColor", light.ColorAmbient);
            node.OpaqueData.Add("DiffuseColor", light.ColorDiffuse);
            node.OpaqueData.Add("SpecularColor", light.ColorSpecular);
            node.OpaqueData.Add("Direction", light.Direction);
            node.OpaqueData.Add("Position", light.Position);

            return node;
        }

        public GeometryContent ConvertAndAdd(MeshContent meshContent, Mesh mesh, Scene scene)
        {
            GeometryContent geometry = new GeometryContent();
            geometry.Indices.AddRange(mesh.GetIndices());
            int positionOffset = meshContent.Positions.Count;
            meshContent.Positions.AddRange(mesh.Vertices.Select((x) => x.ToAnx()));

            geometry.Vertices.AddRange(Enumerable.Range(positionOffset, mesh.VertexCount));
            if (mesh.HasNormals)
                geometry.Vertices.Channels.Add(VertexChannelNames.Normal(), mesh.Normals.Select((x) => x.ToAnx()));

            /*if (mesh.HasTangentBasis)
            {
                geometry.Vertices.Channels.Add(VertexChannelNames.Tangent(0), mesh.Tangents.Select((x) => x.ToAnx()));
                geometry.Vertices.Channels.Add(VertexChannelNames.Binormal(0), mesh.BiTangents.Select((x) => x.ToAnx()));
            }
            */
            for (int i = 0; i < mesh.TextureCoordinateChannelCount; i++)
            {
                var componentCount = mesh.UVComponentCount[i];
                geometry.Vertices.Channels.Add(VertexChannelNames.TextureCoordinate(i), GetUvVertexType(componentCount), mesh.TextureCoordinateChannels[i].Select((x) => ExtractUvVector(x.ToAnx(), componentCount)));
            }

            for (int i = 0; i < mesh.VertexColorChannelCount; i++)
            {
                geometry.Vertices.Channels.Add(VertexChannelNames.Color(i), mesh.VertexColorChannels[i].Select((x) => x.ToAnx().ToVector4()));
            }

            List<BoneContent> bones = new List<BoneContent>();
            if (mesh.HasBones)
            {
                var boneWeights = new Dictionary<int, BoneWeight>();
                foreach (var assimpBone in mesh.Bones)
                {
                    BoneContent bone = new BoneContent();
                    bone.Name = assimpBone.Name;
                    bone.Transform = assimpBone.OffsetMatrix.ToAnx();

                    foreach (var vertexWeight in assimpBone.VertexWeights)
                    {
                        boneWeights.Add(vertexWeight.VertexID, new BoneWeight(assimpBone.Name, vertexWeight.Weight));
                    }

                    bones.Add(bone);
                }

                var weightlessVertices = new List<int>();
                var weightChannel = new BoneWeightCollection();
                for (int i = 0; i < boneWeights.Count; i++)
                {
                    BoneWeight weight;
                    if (!boneWeights.TryGetValue(i, out weight))
                    {
                        weight = new BoneWeight(null, 0);
                        weightlessVertices.Add(i);
                    }

                    weightChannel.Add(weight);
                }

                if (weightlessVertices.Count > 0)
                {
                    this.context.Logger.LogImportantMessage(string.Format("The following vertices on the mesh \"{0}\" didn't contain bone weights: {1}", mesh.Name, string.Join(",", weightlessVertices)));
                }

                geometry.Vertices.Channels.Add(VertexChannelNames.Weights(), weightChannel);
            }

            var material = Convert(scene.Materials[mesh.MaterialIndex]);
            if (mesh.VertexColorChannelCount > 0)
                material.VertexColorEnabled = true;

            geometry.Material = material;

            meshContent.Geometry.Add(geometry);

            return geometry;
        }

        private Type GetUvVertexType(int uvComponentCount)
        {
            switch (uvComponentCount)
            {
                case 1:
                    return typeof(float);
                case 2:
                    return typeof(Vector2);
                case 3:
                    return typeof(Vector3);
                default:
                    throw new NotSupportedException(String.Format("An UV component count of {0} is not supported.", uvComponentCount));
            }
        }

        private object ExtractUvVector(Vector3 vector, int componentCount)
        {
            switch (componentCount)
            {
                case 1:
                    return vector.X;
                case 2:
                    return new Vector2(vector.X, vector.Y);
                case 3:
                    return vector;
                default:
                    throw new NotSupportedException(String.Format("An UV component count of {0} is not supported.", componentCount));
            }
        }

        private AssimpMaterialContent Convert(Material assimpMaterial)
        {
            AssimpMaterialContent material = new AssimpMaterialContent();
            
            material.Name = assimpMaterial.Name;
            foreach (var texture in assimpMaterial.GetAllMaterialTextures())
            {
                if (string.IsNullOrEmpty(texture.FilePath))
                    continue;

                material.Textures.Add(System.IO.Path.GetFileNameWithoutExtension(texture.FilePath) + " " + texture.TextureIndex, new ExternalReference<TextureContent>(texture.FilePath));
            }
            
            if (assimpMaterial.HasBlendMode)
                material.IsAdditive = assimpMaterial.BlendMode == BlendMode.Additive;

            if (assimpMaterial.HasBumpScaling)
                material.BumpScaling = assimpMaterial.BumpScaling;

            if (assimpMaterial.HasColorAmbient)
                material.AmbientColor = assimpMaterial.ColorAmbient.ToAnx().ToVector3();

            if (assimpMaterial.HasColorDiffuse)
                material.DiffuseColor = assimpMaterial.ColorDiffuse.ToAnx().ToVector3();
            
            if (assimpMaterial.HasColorEmissive)
                material.EmissiveColor = assimpMaterial.ColorEmissive.ToAnx().ToVector3();

            if (assimpMaterial.HasColorReflective)
                material.ReflectiveColor = assimpMaterial.ColorReflective.ToAnx().ToVector3();

            if (assimpMaterial.HasColorSpecular)
            {
                var specularColor = assimpMaterial.ColorSpecular.ToAnx().ToVector3();
                if (assimpMaterial.HasShininessStrength)
                    specularColor *= assimpMaterial.ShininessStrength;

                material.SpecularColor = specularColor;
            }

            if (assimpMaterial.HasOpacity)
                material.Alpha = 1 - assimpMaterial.Opacity;
            
            if (assimpMaterial.HasReflectivity)
                material.Reflectivity = assimpMaterial.Reflectivity;

            if (assimpMaterial.HasShadingMode)
                material.ShadingMode = Enum.GetName(typeof(ShadingMode), assimpMaterial.ShadingMode);

            if (assimpMaterial.HasShininess)
                material.SpecularPower = assimpMaterial.Shininess;

            if (assimpMaterial.HasTwoSided)
                material.IsTwoSided = assimpMaterial.IsTwoSided;

            if (assimpMaterial.HasWireFrame)
                material.IsWireframe = assimpMaterial.IsWireFrameEnabled;

            return material;
        }

        private NodeContent Convert(Scene scene, Node node)
        {
            NodeContent content;
            if (node.HasMeshes)
            {
                content = new MeshContent();
                foreach (var mesh in node.EnumerateMeshes(scene))
                {
                    ConvertAndAdd((MeshContent)content, mesh, scene);
                }
            }
            else
            {
                content = new NodeContent();
            }

            content.Name = node.Name;
            content.Transform = node.Transform.ToAnx();
            content.OpaqueData.AddRange(node.Metadata);

            foreach (var child in node.Children)
            {
                var childContent = Convert(scene, child);

                content.Children.Add(childContent);
            }

            return content;
        }

        private IEnumerable<NodeContent> WalkCollapsableChildren(NodeContent node, Predicate<NodeContent> predicate)
        {
            foreach (var child in node.Children)
            {
                if (!predicate(child))
                {
                    yield return child;
                    yield break;
                }

                yield return child;

                foreach (var subChild in WalkCollapsableChildren(child, predicate))
                {
                    yield return subChild;
                }
            }
        }

        private IEnumerable<NodeContent> FindChildren(NodeContentCollection collection, Predicate<NodeContent> predicate)
        {
            foreach (var child in collection)
            {
                if (predicate(child))
                    yield return child;
                else
                    foreach (var subChild in FindChildren(child.Children, predicate))
                    {
                        yield return subChild;
                    }
            }
        }
    }
}
