using Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    static class AssimpExtensions
    {
        public static Matrix ToAnx(this Matrix4x4 matrix)
        {
            return new Matrix(
                matrix.A1, matrix.B1, matrix.C1, matrix.D1,
                matrix.A2, matrix.B2, matrix.C2, matrix.D2,
                matrix.A3, matrix.B3, matrix.C3, matrix.D3,
                matrix.A4, matrix.B4, matrix.C4, matrix.D4);
        }

        public static Color ToAnx(this Color4D color)
        {
            return Color.FromNonPremultiplied(new Vector4(color.R, color.G, color.B, color.A));
        }

        public static Vector3 ToAnx(this Vector3D vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static void AddRange(this OpaqueDataDictionary instance, Metadata metadata)
        {
            foreach (var entry in metadata)
            {
                instance.Add(entry.Key, entry.Value.Data);
            }
        }

        public static IEnumerable<Mesh> EnumerateMeshes(this Node instance, Scene scene)
        {
            foreach (var meshIndex in instance.MeshIndices)
            {
                yield return scene.Meshes[meshIndex];
            }
        }
    }
}
