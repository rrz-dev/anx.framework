using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
    [Developer("Glatzemann")]
    public enum EffectProcessorOutputFormat : byte
    {
        XNA_BYTE_CODE = 0,
        DX10_HLSL = 1,
        DX11_HLSL = 2,
        DX11_1_HLSL = 3,
        OPEN_GL3_GLSL = 4,
        MULTIFORMAT = 5,
    }
}
