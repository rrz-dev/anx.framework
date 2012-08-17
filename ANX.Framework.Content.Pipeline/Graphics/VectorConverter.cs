#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public static class VectorConverter
    {
        public static Converter<TInput, TOutput> GetConverter<TInput, TOutput>()
        {
            throw new NotImplementedException();
        }

        public static bool TryGetSurfaceFormat(Type vectorType, out SurfaceFormat surfaceFormat)
        {
            throw new NotImplementedException();
        }

        public static bool TrayGetVectorType(SurfaceFormat surfaceFormat, out Type vectorType)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetVectorType(VertexElementFormat vertexElementFormat, out Type vectorType)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetVertexElementFormat(Type vectorType, out VertexElementFormat vertexElementFormat)
        {
            throw new NotImplementedException();
        }
    }
}
