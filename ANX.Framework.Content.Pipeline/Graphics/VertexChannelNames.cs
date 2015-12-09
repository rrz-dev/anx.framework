#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.Text.RegularExpressions;
using System.Globalization;

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
            return EncodeName(VertexElementUsage.Binormal, usageIndex);
        }

        public static string Color(int usageIndex)
        {
            return EncodeName(VertexElementUsage.Color, usageIndex);
        }

        public static string DecodeBaseName(string encodedName)
        {
            if (encodedName == null)
                throw new ArgumentNullException("encodedName");

            return encodedName.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        }

        public static int DecodeUsageIndex(string encodedName)
        {
            string baseName = DecodeBaseName(encodedName);
            return int.Parse(encodedName.Substring(baseName.Length), CultureInfo.InvariantCulture);
        }

        public static string EncodeName(string baseName, int usageIndex)
        {
            if (baseName == null)
                throw new ArgumentNullException("baseName");

            if (usageIndex < 0)
                throw new ArgumentOutOfRangeException("usageIndex");

            return String.Format("{0}{1}", baseName, usageIndex);
        }

        public static string EncodeName(VertexElementUsage vertexElementUsage, int usageIndex)
        {
            return EncodeName(vertexElementUsage.ToString(), usageIndex);
        }

        public static string Normal()
        {
            return Normal(0);
        }

        public static string Normal(int usageIndex)
        {
            return EncodeName(VertexElementUsage.Normal, usageIndex);
        }

        public static string Tangent(int usageIndex)
        {
            return EncodeName(VertexElementUsage.Tangent, usageIndex);
        }

        public static string TextureCoordinate(int usageIndex)
        {
            return EncodeName(VertexElementUsage.TextureCoordinate, usageIndex);
        }

        public static bool TryDecodeUsage(string encodedName, out VertexElementUsage usage)
        {
            string baseName = DecodeBaseName(encodedName);

            if (Enum.TryParse<VertexElementUsage>(baseName, true, out usage))
                return true;
            else
            {
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
            return EncodeName(VertexElementUsage.BlendWeight, usageIndex);
        }
    }
}
