#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public static class MeshHelper
    {
        private static bool HasCompleteNormals(MeshContent mesh)
        {
            foreach (var geometry in mesh.Geometry)
            {
                if (!geometry.Vertices.Channels.Contains(VertexChannelNames.Normal()))
                {
                    return false;
                }
            }
            return true;
        }

        public static void CalculateNormals(MeshContent mesh, bool overwriteExistingNormals)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");

            if (!overwriteExistingNormals && HasCompleteNormals(mesh))
            {
                return;
            }

            foreach (GeometryContent geometry in mesh.Geometry)
            {
                VertexChannelCollection channels = geometry.Vertices.Channels;
                if (overwriteExistingNormals)
                {
                    channels.Remove(VertexChannelNames.Normal());
                }
                else if (channels.Contains(VertexChannelNames.Normal()))
                {
                    continue;
                }

                Vector3[] normals = new Vector3[mesh.Positions.Count];
                IndirectPositionCollection positions = geometry.Vertices.Positions;
                VertexChannel<int> positionIndices = geometry.Vertices.PositionIndices;
                for (int i = 0; i < positionIndices.Count; i += 3)
                {
                    Vector3 vector = positions[positionIndices[i + 1]];
                    //Calculate the normale for the given Vector.
                    Vector3 normalVector = Vector3.SafeNormalize(Vector3.Cross(positions[positionIndices[i + 2]] - vector, vector - positions[positionIndices[i]]));
                    for (int j = 0; j < 3; j++)
                    {
                        normals[positionIndices[geometry.Indices[i + j]]] = normalVector;
                    }
                }

                VertexChannel<Vector3> vertexChannel = channels.Add<Vector3>(VertexChannelNames.Normal(), null);
                for (int i = 0; i < vertexChannel.Count; i++)
                {
                    vertexChannel[i] = normals[positionIndices[i]];
                }
            }
        }

        //description from msdn.
        /// <summary>
        /// Compute tangent frames for the given mesh. 
        /// </summary>
        /// <remarks>
        /// This method computes and adds tangent and binormal vertex channels to the given mesh.
        /// 
        /// An InvalidContentException is thrown if:
        /// <list type="bullet">
        ///     <item>The mesh does not contain a normal channel (stored in Normal).</item>
        ///     <item>The data specified in the normal channel is not a Vector3 type.</item>
        ///     <item>The vertex channel of the mesh does not have the name specified by textureCoordinateChannelName.</item>
        ///     <item>The data specified in the texture coordinate channel is not a Vector2 type.</item>
        ///     <item>The channel specified by tangentChannelName already exists.</item>
        ///     <item>The channel specified by binormalChannelName already exists.</item>
        /// </list>
        /// </remarks>
        /// <param name="mesh">The target mesh used to create the tangent frame. All geometries in this mesh must have normal vertex channels stored in Normal, and must contain Vector3 data.</param>
        /// <param name="textureCoordinateChannelName">The texture coordinate channel used for computing the tangent frame. This channel most contain Vector2 data.</param>
        /// <param name="tangentChannelName">Target channel name used to store calculated tangents. A tangent channel is not generated if null or an empty string is specified.</param>
        /// <param name="binormalChannelName">Target channel name used to store calculated binormals. A tangent channel is not generated if null or an empty string is specified.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public static void CalculateTangentFrames(MeshContent mesh, string textureCoordinateChannelName, string tangentChannelName, string binormalChannelName)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");
            
            if (string.IsNullOrEmpty(textureCoordinateChannelName))
                throw new ArgumentNullException("textureCoordinateChannelName");
            
            //Check first for all the cases that we throw an InvalidContentException.
            foreach (var geometry in mesh.Geometry)
            {
                VertexChannelCollection channels = geometry.Vertices.Channels;
                if (!channels.Contains(VertexChannelNames.Normal()))
                    throw new InvalidContentException(string.Format("Does not contain a channel for {0}.", mesh.Name, VertexChannelNames.Normal()), mesh.Identity);

                if (channels[VertexChannelNames.Normal()].ElementType != typeof(Vector3))
                    throw new InvalidContentException(string.Format("ElementType of channel \"{0}\" is not {1}, found instead {2}.", VertexChannelNames.Normal(), typeof(Vector3).Name, channels[VertexChannelNames.Normal()].ElementType), mesh.Identity);

                if (!channels.Contains(textureCoordinateChannelName))
                    throw new InvalidContentException(string.Format("Does not contain a channel with the name \"{0}\".", textureCoordinateChannelName), mesh.Identity);

                if (channels[textureCoordinateChannelName].ElementType != typeof(Vector2))
                    throw new InvalidContentException(string.Format("ElementType of channel \"{0}\" is not {1}, found instead {2}.", textureCoordinateChannelName, typeof(Vector2).Name, channels[textureCoordinateChannelName].ElementType), mesh.Identity);
                
                if (!string.IsNullOrEmpty(tangentChannelName) && channels.Contains(tangentChannelName))
                    throw new InvalidContentException(string.Format("Wanted to create a new channel for \"{0}\", but it already exists.", tangentChannelName), mesh.Identity);

                if (!string.IsNullOrEmpty(binormalChannelName) && channels.Contains(binormalChannelName))
                    throw new InvalidContentException(string.Format("Wanted to create a new channel for \"{0}\", but it already exists.", binormalChannelName), mesh.Identity);
            }

            throw new NotImplementedException();
        }

        public static BoneContent FindSkeleton(NodeContent node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            
            while (node != null)
            {
                if (node is BoneContent)
                {
                    BoneContent boneContent = (BoneContent)node;
                    while (boneContent.Parent is BoneContent)
                    {
                        boneContent = (BoneContent)boneContent.Parent;
                    }
                    return boneContent;
                }

                node = node.Parent;
            }
            return null;
        }

        public static IList<BoneContent> FlattenSkeleton(BoneContent skeleton)
        {
            if (skeleton == null)
                throw new ArgumentNullException("skeleton");

            Dictionary<BoneContent, bool> list = new Dictionary<BoneContent, bool>();
            MeshHelper.FlattenSkeleton(skeleton, list);
            return list.Keys.ToList();
        }

        private static void FlattenSkeleton(BoneContent skeleton, Dictionary<BoneContent, bool> list)
        {
            if (list.ContainsKey(skeleton))
            {
                throw new ArgumentException("The skeleton contains duplicate entries: {0}", skeleton.Name);
            }

            list.Add(skeleton, false);
            foreach (var child in skeleton.Children)
            {
                if (child is BoneContent)
                {
                    FlattenSkeleton((BoneContent)child, list);
                }
            }
        }

        public static void MergeDuplicatePositions(MeshContent mesh, float tolerance)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");

            //TODO: test
            float toleranceSquared = tolerance * tolerance;

            var positions = mesh.Positions;
            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = positions.Count - 1; j > i; j--)
                {
                    if (Vector3.DistanceSquared(positions[i], positions[j]) < toleranceSquared)
                    {
                        //Remove duplicate position.
                        positions.RemoveAt(j);

                        foreach (GeometryContent geometry in mesh.Geometry)
                        {
                            var positionIndices = geometry.Vertices.PositionIndices;
                            for (int k = 0; k < positionIndices.Count; k++)
                            {
                                if (positionIndices[k] == j)
                                {
                                    positionIndices[k] = i;
                                }
                                //reduce the index for all position indexes that references the removed position by 1.
                                else if (positionIndices[k] > j)
                                {
                                    positionIndices[k]--;
                                }
                            }
                        }

                        j++;
                    }
                }
            }
        }

        public static void MergeDuplicateVertices(GeometryContent geometry)
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");

            List<int> newIndices = new List<int>();
            List<int> newPositionIndices = new List<int>();
            //Maps from the index in newPositionIndices to the index in the old position indices.
            List<int> positionIndexMapping = new List<int>();
            
            //Find positionIndex duplicates
            var positionIndices = geometry.Vertices.PositionIndices;
            for (int i = 0; i < positionIndices.Count; i++)
            {
                int index = newPositionIndices.IndexOf(positionIndices[i]);
                //If the positionIndex is not in the list, add it.
                if (index == -1)
                {
                    index = newPositionIndices.Count;
                    newPositionIndices.Add(positionIndices[i]);
                    positionIndexMapping.Add(i);
                }
                else
                {
                    var originalIndex = positionIndexMapping[index];
                    //check other channels, if any of them has other data, we can't skip that vertex.
                    foreach (var channel in geometry.Vertices.Channels)
                    {
                        var originalVertex = channel[originalIndex];
                        var currentVertex = channel[i];

                        //Only add an positionIndex that was already found if the vertex represented is different in any channel.
                        if (currentVertex != null && (originalVertex == null || !originalVertex.Equals(currentVertex)))
                        {
                            index = newPositionIndices.Count;
                            newPositionIndices.Add(positionIndices[i]);
                            positionIndexMapping.Add(i);
                            break;
                        }
                    }
                }

                newIndices.Add(index);
            }

            if (newPositionIndices.Count != positionIndices.Count)
            {
                geometry.Indices.Clear();
                geometry.Indices.AddRange(newIndices);

                //Copy the needed data from the vertex channels.
                var channels = geometry.Vertices.Channels;
                List<object>[] vertexChannelsData = new List<object>[channels.Count];
                for (int i = 0; i < channels.Count; i++)
                {
                    List<object> data = new List<object>();
                    vertexChannelsData[i] = data;

                    var channel = channels[i];
                    for (int j = 0; j < newPositionIndices.Count; j++)
                    {
                        data.Add(channel[positionIndexMapping[j]]);
                    }
                }

                //Set new position indices.
                positionIndices.Clear();
                positionIndices.AddRange(newPositionIndices);

                //Set data of the vertex channels.
                for (int i = 0; i < channels.Count; i++)
                {
                    var channel = channels[i];

                    channel.Clear();
                    channel.AddRange(vertexChannelsData[i]);
                }
            }
        }

        public static void MergeDuplicateVertices(MeshContent mesh)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");

            foreach (var geometry in mesh.Geometry)
            {
                MergeDuplicateVertices(geometry);
            }
        }

        public static void OptimizeForCache(MeshContent mesh)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");


        }

        public static void SwapWindingOrder(MeshContent mesh)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");
            
            foreach (var geometry in mesh.Geometry)
            {
                var indices = geometry.Indices;
                if (indices.Count % 3 != 0)
                    throw new ArgumentException(string.Format("Number of indices in geometry \"{0}\" is not a multiple of 3.", geometry.Name));

                for (int i = 0; i < indices.Count; i += 3)
                {
                    //Flip the start and the end index of the triangle.
                    int index = indices[i];

                    indices[i] = indices[i + 2];
                    indices[i + 2] = index;
                }
            }
        }

        /// <summary>
        /// Applies a transformation to the contents of a scene hierarchy. 
        /// </summary>
        /// <remarks>
        /// The resulting world space positions are similar to the results obtained from applying the specified transform to the Transform property of the scene object. 
        /// However, this method performs the transformation by cascading down through the scene hierarchy and modifying the actual underlying vertex positions. 
        /// </remarks>
        /// <param name="scene">Scene hierarchy being transformed.</param>
        /// <param name="transform">Matrix used in the transformation</param>
        public static void TransformScene(NodeContent scene, Matrix transform)
        {
            if (scene is MeshContent)
            {
                var mesh = (MeshContent)scene;
                
                var positions = mesh.Positions;
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i] = Vector3.Transform(positions[i], transform);
                }
            }

            foreach (var animation in scene.Animations.Values)
            {
                foreach (var channel in animation.Channels.Values)
                {
                    for (int i = 0; i < channel.Count; i++)
                    {
                        channel[i].Transform *= transform;
                    }
                }
            }

            foreach (var child in scene.Children)
            {
                TransformScene(child, transform);
            }
        }
    }
}
