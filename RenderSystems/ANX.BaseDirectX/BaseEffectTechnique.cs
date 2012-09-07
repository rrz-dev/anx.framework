using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.BaseDirectX
{
	public abstract class BaseEffectTechnique<T> where T : class, IDisposable
	{
		protected Effect parentEffect;

		public T NativeTechnique { get; protected set; }

		public BaseEffectTechnique(Effect parentEffect, T nativeTechnique)
		{
			if (parentEffect == null)
				throw new ArgumentNullException("parentEffect");

			this.parentEffect = parentEffect;
			NativeTechnique = nativeTechnique;
		}
	}
}
