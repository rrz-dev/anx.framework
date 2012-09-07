#region Using Statements
using System;
using ANX.Framework.Graphics;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.DXGI;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    internal class FormatConverter
    {
        public static Filter Translate(TextureFilter filter)
        {
            switch (filter)
            {
                case TextureFilter.Anisotropic:
                    return Filter.Anisotropic;
                case TextureFilter.Linear:
                    return Filter.MinMagMipLinear;
                case TextureFilter.LinearMipPoint:
                    return Filter.MinMagMipPoint;
                case TextureFilter.MinLinearMagPointMipLinear:
                    return Filter.MinLinearMagPointMipLinear;
                case TextureFilter.MinLinearMagPointMipPoint:
                    return Filter.MinLinearMagMipPoint;
                case TextureFilter.MinPointMagLinearMipLinear:
                    return Filter.MinPointMagMipLinear;
                case TextureFilter.MinPointMagLinearMipPoint:
                    return Filter.MinPointMagLinearMipPoint;
                case TextureFilter.Point:
                    return Filter.MinMagMipPoint;
                case TextureFilter.PointMipLinear:
                    return Filter.MinMagPointMipLinear;
            }

            throw new NotImplementedException();
        }

        public static SharpDX.Direct3D11.TextureAddressMode Translate(ANX.Framework.Graphics.TextureAddressMode addressMode)
        {
            switch (addressMode)
            {
                case ANX.Framework.Graphics.TextureAddressMode.Clamp:
                    return SharpDX.Direct3D11.TextureAddressMode.Clamp;
                case ANX.Framework.Graphics.TextureAddressMode.Mirror:
                    return SharpDX.Direct3D11.TextureAddressMode.Mirror;
                case ANX.Framework.Graphics.TextureAddressMode.Wrap:
                    return SharpDX.Direct3D11.TextureAddressMode.Wrap;
            }

            return SharpDX.Direct3D11.TextureAddressMode.Clamp;
        }

        public static BlendOperation Translate(BlendFunction blendFunction)
        {
            switch (blendFunction)
            {
                case BlendFunction.Add:
                    return BlendOperation.Add;
                case BlendFunction.Max:
                    return BlendOperation.Maximum;
                case BlendFunction.Min:
                    return BlendOperation.Minimum;
                case BlendFunction.ReverseSubtract:
                    return BlendOperation.ReverseSubtract;
                case BlendFunction.Subtract:
                    return BlendOperation.Subtract;
            }

            throw new NotImplementedException();
        }

        public static BlendOption Translate(Blend blend)
        {
            switch (blend)
            {
                case Blend.BlendFactor:
                    return BlendOption.BlendFactor;
                case Blend.DestinationAlpha:
                    return BlendOption.DestinationAlpha;
                case Blend.DestinationColor:
                    return BlendOption.DestinationColor;
                case Blend.InverseBlendFactor:
                    return BlendOption.InverseBlendFactor;
                case Blend.InverseDestinationAlpha:
                    return BlendOption.InverseDestinationAlpha;
                case Blend.InverseDestinationColor:
                    return BlendOption.InverseDestinationColor;
                case Blend.InverseSourceAlpha:
                    return BlendOption.InverseSourceAlpha;
                case Blend.InverseSourceColor:
                    return BlendOption.InverseSourceColor;
                case Blend.One:
                    return BlendOption.One;
                case Blend.SourceAlpha:
                    return BlendOption.SourceAlpha;
                case Blend.SourceAlphaSaturation:
                    return BlendOption.SourceAlphaSaturate;
                case Blend.SourceColor:
                    return BlendOption.SourceColor;
                case Blend.Zero:
                    return BlendOption.Zero;
            }

            throw new NotImplementedException();
        }

        public static ColorWriteMaskFlags Translate(ColorWriteChannels colorWriteChannels)
        {
            ColorWriteMaskFlags mask = 0;

            if ((colorWriteChannels & ColorWriteChannels.All) == ColorWriteChannels.All)
                mask |= ColorWriteMaskFlags.All;

            if ((colorWriteChannels & ColorWriteChannels.Alpha) == ColorWriteChannels.Alpha)
                mask |= ColorWriteMaskFlags.Alpha;

            if ((colorWriteChannels & ColorWriteChannels.Blue) == ColorWriteChannels.Blue)
                mask |= ColorWriteMaskFlags.Blue;

            if ((colorWriteChannels & ColorWriteChannels.Green) == ColorWriteChannels.Green)
                mask |= ColorWriteMaskFlags.Green;

            if ((colorWriteChannels & ColorWriteChannels.Red) == ColorWriteChannels.Red)
                mask |= ColorWriteMaskFlags.Red;

            return mask;
        }

        public static SharpDX.Direct3D11.StencilOperation Translate(ANX.Framework.Graphics.StencilOperation stencilOperation)
        {
            switch (stencilOperation)
            {
                case ANX.Framework.Graphics.StencilOperation.Decrement:
                    return SharpDX.Direct3D11.StencilOperation.Decrement;
                case ANX.Framework.Graphics.StencilOperation.DecrementSaturation:
                    return SharpDX.Direct3D11.StencilOperation.DecrementAndClamp;
                case ANX.Framework.Graphics.StencilOperation.Increment:
                    return SharpDX.Direct3D11.StencilOperation.Increment;
                case ANX.Framework.Graphics.StencilOperation.IncrementSaturation:
                    return SharpDX.Direct3D11.StencilOperation.IncrementAndClamp;
                case ANX.Framework.Graphics.StencilOperation.Invert:
                    return SharpDX.Direct3D11.StencilOperation.Invert;
                case ANX.Framework.Graphics.StencilOperation.Keep:
                    return SharpDX.Direct3D11.StencilOperation.Keep;
                case ANX.Framework.Graphics.StencilOperation.Replace:
                    return SharpDX.Direct3D11.StencilOperation.Replace;
                case ANX.Framework.Graphics.StencilOperation.Zero:
                    return SharpDX.Direct3D11.StencilOperation.Zero;
            }

            throw new NotImplementedException("unknown StencilOperation");
        }

        public static Comparison Translate(ANX.Framework.Graphics.CompareFunction compareFunction)
        {
            switch (compareFunction)
            {
                case ANX.Framework.Graphics.CompareFunction.Always:
                    return Comparison.Always;
                case ANX.Framework.Graphics.CompareFunction.Equal:
                    return Comparison.Equal;
                case ANX.Framework.Graphics.CompareFunction.Greater:
                    return Comparison.Greater;
                case ANX.Framework.Graphics.CompareFunction.GreaterEqual:
                    return Comparison.GreaterEqual;
                case ANX.Framework.Graphics.CompareFunction.Less:
                    return Comparison.Less;
                case ANX.Framework.Graphics.CompareFunction.LessEqual:
                    return Comparison.LessEqual;
                case ANX.Framework.Graphics.CompareFunction.Never:
                    return Comparison.Never;
                case ANX.Framework.Graphics.CompareFunction.NotEqual:
                    return Comparison.NotEqual;
            }

            throw new NotImplementedException("unknown CompareFunction");
        }

        public static SharpDX.Direct3D11.CullMode Translate(ANX.Framework.Graphics.CullMode cullMode)
        {
            if (cullMode == ANX.Framework.Graphics.CullMode.CullClockwiseFace)
            {
                return SharpDX.Direct3D11.CullMode.Front;
            }
            else if (cullMode == ANX.Framework.Graphics.CullMode.CullCounterClockwiseFace)
            {
                return SharpDX.Direct3D11.CullMode.Back;
            }
            else
            {
                return SharpDX.Direct3D11.CullMode.None;
            }
        }

        public static SharpDX.Direct3D11.FillMode Translate(ANX.Framework.Graphics.FillMode fillMode)
        {
            if (fillMode == ANX.Framework.Graphics.FillMode.WireFrame)
            {
                return SharpDX.Direct3D11.FillMode.Wireframe;
            }
            else
            {
                return SharpDX.Direct3D11.FillMode.Solid;
            }
        }

    }
}
