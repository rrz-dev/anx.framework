#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeSamplerState : IDisposable
    {
        void Apply(GraphicsDevice graphicsDevice, int index);

        void Release();

        bool IsBound { get; }

        TextureAddressMode AddressU { set; }

        TextureAddressMode AddressV { set; }

        TextureAddressMode AddressW { set; }

        TextureFilter Filter { set; }

        int MaxAnisotropy { set; }

        int MaxMipLevel { set; }

        float MipMapLevelOfDetailBias { set; }
    }
}
