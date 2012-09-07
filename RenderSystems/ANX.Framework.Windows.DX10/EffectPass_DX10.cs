using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class EffectPass_DX10 : INativeEffectPass
	{
		private EffectPass nativePass;

        public string Name
        {
            get 
            {
				return nativePass.Description.Name;
            }
        }

		internal EffectPass_DX10(EffectPass setNativePass)
		{
			nativePass = setNativePass;
		}
    }
}
