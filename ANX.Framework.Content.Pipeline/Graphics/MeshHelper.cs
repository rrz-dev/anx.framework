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
        public static void CalculateNormals(MeshContent mesh, bool overwriteExistingNormals)
        {
            throw new NotImplementedException();
        }

        public static void CalculateTangentFrames(MeshContent mesh, string textureCoordinateChannelName, string tangentChannelName, string binormalChannelName)
        {
            throw new NotImplementedException();
        }

        public static BoneContent FindSkeleton(NodeContent node)
        {
            throw new NotImplementedException();
        }

        public static IList<BoneContent> FlattenSkeleton(BoneContent skeleton)
        {
            throw new NotImplementedException();
        }

        public static void MergeDuplicatePositions(MeshContent mesh, float tolerance)
        {
            throw new NotImplementedException();
        }

        public static void MergeDuplicateVertices(GeometryContent geometry)
        {
            throw new NotImplementedException();
        }

        public static void MergeDuplicateVertices(MeshContent mesh)
        {
            throw new NotImplementedException();
        }

        public static void OptimizeForCache(MeshContent mesh)
        {
            throw new NotImplementedException();
        }

        public static void SwapWindingOrder(MeshContent mesh)
        {
            throw new NotImplementedException();
        }

        public static void TransformScene(NodeContent scene, Matrix transform)
        {
            throw new NotImplementedException();
        }
    }
}
