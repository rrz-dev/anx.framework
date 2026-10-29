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
    public sealed class MeshBuilder
    {

        public bool MergeDuplicatePositions { get; set; }
        public float MergePositionTolerance { get; set; }
        public string Name { get; set; }
        public bool SwapWindingOrder { get; set; }

        public void AddTriangleVertex(int indexIntoVertexCollection)
        {
            throw new NotImplementedException();
        }

        public int CreatePosition(float x, float y, float z)
        {
            return CreatePosition(new Vector3(x, y, z));
        }

        public int CreatePosition(Vector3 pos)
        {
            throw new NotImplementedException();
        }

        public int CreateVertexChannel<T>(string usage)
        {
            throw new NotImplementedException();
        }

        public MeshContent FinishMesh()
        {
            throw new NotImplementedException();
        }

        public void SetMaterial(MaterialContent material)
        {
            throw new NotImplementedException();
        }

        public void SetOpaqueData(OpaqueDataDictionary opaqueData)
        {
            throw new NotImplementedException();
        }

        public void SetVertexChannelData(int vertexDataIndex, Object channelData)
        {
            throw new NotImplementedException();
        }

        public static MeshBuilder StartMesh(string name)
        {
            throw new NotImplementedException();
        }
    }
}
