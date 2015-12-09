using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public abstract class BaseStateObject<T> : IDisposable where T : class, IDisposable
	{
		protected const int IntMaxOver16 = int.MaxValue / 16;
		protected const float ColorByteToFloatFactor = 1f / 255f;

		protected bool isDirty;
		protected T nativeState;

		public bool IsBound { get; protected set; }

        protected GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

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
		protected void SetValueIfDifferentAndMarkDirty<Tv>(ref Tv oldValue, ref Tv newValue)
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
            if (graphics != this.GraphicsDevice)
            {
                if (this.GraphicsDevice != null)
                    this.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;

                if (graphics != null)
                    graphics.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

                this.GraphicsDevice = graphics;
                this.Dispose();
            }

			if (isDirty || nativeState == null)
			{
				Dispose();
                this.GraphicsDevice = graphics;
				nativeState = CreateNativeState(graphics);
				isDirty = false;
			}
		}

        void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
        {
            this.Dispose();
        }
		#endregion

		protected virtual void Init() { }
		protected abstract T CreateNativeState(GraphicsDevice graphics);
		protected abstract void ApplyNativeState(GraphicsDevice graphics);
	}
}
