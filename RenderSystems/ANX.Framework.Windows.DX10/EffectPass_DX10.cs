using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class EffectPass_DX10 : INativeEffectPass
    {
		public EffectPass NativePass { get; internal set; }

        public string Name
        {
            get 
            {
				return NativePass.Description.Name;
            }
        }

		internal EffectPass_DX10(EffectPass setNativePass)
		{
			NativePass = setNativePass;
		}
    }
}
