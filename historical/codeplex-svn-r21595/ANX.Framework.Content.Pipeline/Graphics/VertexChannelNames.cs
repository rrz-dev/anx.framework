#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.Text.RegularExpressions;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public static class VertexChannelNames
    {
        public static string Binormal(int usageIndex)
        {
            return EncodeName("BINORMAL", usageIndex);
        }

        public static string Color(int usageIndex)
        {
            return EncodeName("COLOR", usageIndex);
        }

        public static string DecodeBaseName(string encodedName)
        {
            return encodedName.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        }

        public static string DecodeUsageIndex(string encodedName)
        {
            string baseName = DecodeBaseName(encodedName);
            return encodedName.Replace(baseName, "").Trim();
        }

        public static string EncodeName(string baseName, int usageIndex)
        {
            return String.Format("{0}{1}", baseName.ToUpperInvariant(), usageIndex);
        }

        public static string EncodeName(VertexElementUsage vertexElementUsage, int usageIndex)
        {
            string baseName = String.Empty;

            switch (vertexElementUsage)
            {
                case VertexElementUsage.Binormal:
                case VertexElementUsage.BlendIndices:
                case VertexElementUsage.BlendWeight:
                case VertexElementUsage.Color:
                case VertexElementUsage.Normal:
                case VertexElementUsage.Position:
                case VertexElementUsage.Tangent:
                    baseName = vertexElementUsage.ToString().ToUpperInvariant();
                    break;
                case VertexElementUsage.PointSize:
                    baseName = "PSIZE";
                    break;
                case VertexElementUsage.TessellateFactor:
                    baseName = "TESSFACTOR";
                    break;
                case VertexElementUsage.TextureCoordinate:
                    baseName = "TEXCOORD";
                    break;
            }

            if (!String.IsNullOrEmpty(baseName))
            {
                return EncodeName(baseName, usageIndex);
            }

            return baseName;
        }

        public static string Normal()
        {
            return Normal(0);
        }

        public static string Normal(int usageIndex)
        {
            return EncodeName("NORMAL", usageIndex);
        }

        public static string Tangent(int usageIndex)
        {
            return EncodeName("TANGENT", usageIndex);
        }

        public static string TextureCoordinate(int usageIndex)
        {
            return EncodeName("TEXCOORD", usageIndex);
        }

        public static bool TryDecodeUsage(string encodedName, out VertexElementUsage usage)
        {
            string baseName = DecodeBaseName(encodedName);

            switch (baseName)
            {
                case "BINORMAL":
                    usage = VertexElementUsage.Binormal;
                    return true;
                case "BLENDINDICES":
                    usage = VertexElementUsage.BlendIndices;
                    return true;
                case "BLENDWEIGHT":
                    usage = VertexElementUsage.BlendWeight;
                    return true;
                case "COLOR":
                    usage = VertexElementUsage.Color;
                    return true;
                case "NORMAL":
                    usage = VertexElementUsage.Normal;
                    return true;
                case "POSITION":
                    usage = VertexElementUsage.Position;
                    return true;
                case "PSIZE":
                    usage = VertexElementUsage.PointSize;
                    return true;
                case "TANGENT":
                    usage = VertexElementUsage.Tangent;
                    return true;
                case "TEXCOORD":
                    usage = VertexElementUsage.TextureCoordinate;
                    return true;
                case "TESSFACTOR":
                    usage = VertexElementUsage.TessellateFactor;
                    return true;
                default:
                    usage = 0;
                    return false;
            }
        }

        public static string Weights()
        {
            return Weights(0);
        }

        public static string Weights(int usageIndex)
        {
            return EncodeName("BLENDWEIGHT", usageIndex);
        }
    }
}
