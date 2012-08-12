using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectPass_Metro : INativeEffectPass
	{
		//private EffectPass nativePass;

		//public EffectPass NativePass
		//{
		//    get
		//    {
		//        return this.nativePass;
		//    }
		//    internal set
		//    {
		//        this.nativePass = value;
		//    }
		//}

		public string Name
		{
			get
			{
				//return nativePass.Description.Name;
				throw new NotImplementedException();
			}
		}
	}
}
