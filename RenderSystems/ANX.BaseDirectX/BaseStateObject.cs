using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.BaseDirectX
{
	public abstract class BaseStateObject<T> : IDisposable where T : class, IDisposable
	{
		protected const int IntMaxOver16 = int.MaxValue / 16;
		protected const float ColorByteToFloatFactor = 1f / 255f;

		#region Private
		protected bool isDirty;
		protected T nativeState;
		#endregion

		#region Public
		public bool IsBound { get; protected set; }
		#endregion

		#region Constructor
		protected BaseStateObject()
		{
			isDirty = true;
			Init();
		}
		#endregion

		#region Release
		public void Release()
		{
			IsBound = false;
		}
		#endregion

		#region SetValueIfDifferentAndMarkDirty
		protected void SetValueIfDifferentAndMarkDirty<T>(ref T oldValue, ref T newValue)
		{
			if (oldValue.Equals(newValue) == false)
			{
				isDirty = true;
				oldValue = newValue;
			}
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (nativeState != null)
			{
				nativeState.Dispose();
				nativeState = null;
			}
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphics)
		{
			UpdateNativeBlendState(graphics);
			IsBound = true;
			ApplyNativeState(graphics);
		}
		#endregion

		#region UpdateNativeBlendState
		private void UpdateNativeBlendState(GraphicsDevice graphics)
		{
			if (isDirty || nativeState == null)
			{
				Dispose();
				nativeState = CreateNativeState(graphics);
				isDirty = false;
			}
		}
		#endregion

		protected virtual void Init() { }
		protected abstract T CreateNativeState(GraphicsDevice graphics);
		protected abstract void ApplyNativeState(GraphicsDevice graphics);
	}
}
