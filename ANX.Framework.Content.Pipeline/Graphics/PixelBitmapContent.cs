#region Using Statements
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class PixelBitmapContent<T> : BitmapContent where T : struct, IEquatable<T>
    {
        private T[,] pixels;
        private int pixelSize;

        protected PixelBitmapContent()
        {
            this.pixelSize = Marshal.SizeOf(typeof(T));
        }

        public PixelBitmapContent(int width, int height)
            : base(width, height)
        {
            pixels = new T[width, height];
            this.pixelSize = Marshal.SizeOf(typeof(T));
        }

        public T GetPixel(int x, int y)
        {
            return pixels[x, y];
        }

        public override byte[] GetPixelData()
        {
            int rowSize = Marshal.SizeOf(typeof(T)) * base.Width;
            byte[] array = new byte[rowSize * base.Height];

            int destinationIndex = 0;
            for (int i = 0; i < base.Height; i++)
            {
                T[] row = GetRow(i);

                for (int x = 0; x < row.Length; x++)
                {
                    Array.Copy(GetBytes<T>(row[x]), 0, array, destinationIndex, pixelSize);
                    destinationIndex += pixelSize;
                }
            }

            return array;
        }

        private static byte[] GetBytes<Tv>(Tv value)
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(Tv))];

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            }
            finally
            {
                handle.Free();
            }

            return buffer;
        }

        public T[] GetRow(int y)
        {
            T[] row = new T[Width];
            for (int col = 0; col < Width; col++)
            {
                row[col] = pixels[col, y];
            }

            return row;
        }

        public void SetPixel(int x, int y, T value)
        {
            pixels[x, y] = value;
        }

        public override void SetPixelData(byte[] sourceData)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        protected override bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }

        protected override bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetFormat(out SurfaceFormat format)
        {
            string type = typeof(T).Name.ToLowerInvariant();

            switch (type)
            {
                case "float":
                    format = SurfaceFormat.Single;
                    return true;
                case "vector2":
                    format = SurfaceFormat.Single;
                    return true;
                case "vector4":
                    format = SurfaceFormat.Vector4;
                    return true;
                case "halfsingle":
                    format = SurfaceFormat.HalfVector2;
                    return true;
                case "halfvector2":
                    format = SurfaceFormat.HalfVector2;
                    return true;
                case "halfvector4":
                    format = SurfaceFormat.HalfVector4;
                    return true;
                case "bgra5551":
                    format = SurfaceFormat.Bgra5551;
                    return true;
                case "bgr565":
                    format = SurfaceFormat.Bgr565;
                    return true;
                case "bgra4444":
                    format = SurfaceFormat.Bgra4444;
                    return true;
                case "color":
                    format = SurfaceFormat.Color;
                    return true;
                case "rg32":
                    format = SurfaceFormat.Rg32;
                    return true;
                case "rgba64":
                    format = SurfaceFormat.Rgba64;
                    return true;
                case "rgba1010102":
                    format = SurfaceFormat.Rgba1010102;
                    return true;
                case "alpha8":
                    format = SurfaceFormat.Alpha8;
                    return true;
                case "normalizedbyte2":
                    format = SurfaceFormat.NormalizedByte2;
                    return true;
                case "normalizedbyte4":
                    format = SurfaceFormat.NormalizedByte4;
                    return true;
                default:
                    format = Framework.Graphics.SurfaceFormat.Color;
                    return false;
            }

        }
    }
}
