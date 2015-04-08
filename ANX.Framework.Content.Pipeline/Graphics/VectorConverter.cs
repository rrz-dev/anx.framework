#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.Graphics.PackedVector;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public static class VectorConverter
    {
        private static Dictionary<Type, VertexElementFormat> typeVertexFormatAssoc = new Dictionary<Type, VertexElementFormat>();

        static VectorConverter()
        {
            //VertexElementFormat is our own custom type, so it's probably ok to add all the supported types without the ability to extend it from the outside.
            typeVertexFormatAssoc.Add(typeof(Byte4), VertexElementFormat.Byte4);
            typeVertexFormatAssoc.Add(typeof(Color), VertexElementFormat.Color);
            typeVertexFormatAssoc.Add(typeof(HalfVector2), VertexElementFormat.HalfVector2);
            typeVertexFormatAssoc.Add(typeof(HalfVector4), VertexElementFormat.HalfVector4);
            typeVertexFormatAssoc.Add(typeof(NormalizedShort2), VertexElementFormat.NormalizedShort2);
            typeVertexFormatAssoc.Add(typeof(NormalizedShort4), VertexElementFormat.NormalizedShort4);
            typeVertexFormatAssoc.Add(typeof(Short2), VertexElementFormat.Short2);
            typeVertexFormatAssoc.Add(typeof(Short4), VertexElementFormat.Short4);
            typeVertexFormatAssoc.Add(typeof(Single), VertexElementFormat.Single);
            typeVertexFormatAssoc.Add(typeof(Vector2), VertexElementFormat.Vector2);
            typeVertexFormatAssoc.Add(typeof(Vector3), VertexElementFormat.Vector3);
            typeVertexFormatAssoc.Add(typeof(Vector4), VertexElementFormat.Vector4);
        }

        public static Converter<TInput, TOutput> GetConverter<TInput, TOutput>()
        {
            //Convert the input to a common format and then convert that to the output format.
            //For that, we need individual converters with a common type.
            //The biggest possible types of the values can be represendet by Vector4, so we use that one.
            var toVector4 = GetToVector4Converter(typeof(TInput));
            var fromVector4 = GetFromVector4Converter(typeof(TOutput));

            //Doing some boxing, unboxing here, but that shouldn't be of concern.
            return (TInput input) => (TOutput)fromVector4(toVector4(input));
        }

        private static Func<object, Vector4> GetToVector4Converter(Type type)
        {
            if (type == typeof(float))
                return (object value) => new Vector4((float)value, 0f, 0f, 1f);
            else if (type == typeof(Vector2))
            {
                return (object value) =>
                    {
                        Vector2 val = (Vector2)value;
                        return new Vector4(val.X, val.Y, 0f, 1f);
                    };
            }
            else if (type == typeof(Vector3))
            {
                return (object value) =>
                    {
                        Vector3 val = (Vector3)value;
                        return new Vector4(val.X, val.Y, val.Z, 1f);
                    };
            }
            else if (type == typeof(Vector4))
            {
                return (object value) => { return (Vector4)value; };
            }
            else if (type.IsValueType && typeof(IPackedVector).IsAssignableFrom(type))
            {
                return (object value) => { return ((IPackedVector)value).ToVector4(); };
            }
            else throw new NotSupportedException(type.FullName + " isn't supported by VectorConverter");
        }

        private static Func<Vector4, object> GetFromVector4Converter(Type type)
        {
            if (type == typeof(float))
                return (Vector4 value) => value.X;
            else if (type == typeof(Vector2))
            {
                return (Vector4 value) => new Vector2(value.X, value.Y);
            }
            else if (type == typeof(Vector3))
            {
                return (Vector4 value) => new Vector3(value.X, value.Y, value.Z);
            }
            else if (type == typeof(Vector4))
            {
                return (Vector4 value) => value;
            }
            else if (type.IsValueType && typeof(IPackedVector).IsAssignableFrom(type))
            {
                return (Vector4 value) => 
                { 
                    var instance = (IPackedVector)Activator.CreateInstance(type);
                    instance.PackFromVector4(value);
                    return instance;
                };
            }
            else throw new NotSupportedException(type.FullName + " isn't supported by VectorConverter");
        }

        public static bool TryGetSurfaceFormat(Type vectorType, out SurfaceFormat surfaceFormat)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetVectorType(SurfaceFormat surfaceFormat, out Type vectorType)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetVectorType(VertexElementFormat vertexElementFormat, out Type vectorType)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetVertexElementFormat(Type vectorType, out VertexElementFormat vertexElementFormat)
        {
            return typeVertexFormatAssoc.TryGetValue(vectorType, out vertexElementFormat);
        }
    }
}
