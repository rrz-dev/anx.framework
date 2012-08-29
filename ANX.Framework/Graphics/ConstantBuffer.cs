#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
#if XNAEXT

    [PercentageComplete(10)]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class ConstantBuffer : GraphicsResource, IGraphicsResource
    {
        #region Private
        private BufferUsage bufferUsage;
        private INativeConstantBuffer nativeConstantBuffer;
        #endregion

        #region Public
        internal INativeConstantBuffer NativeConstantBuffer
        {
            get
            {
                if (this.nativeConstantBuffer == null)
                {
                    CreateNativeBuffer();
                }

                return this.nativeConstantBuffer;
            }
        }

        public BufferUsage BufferUsage
        {
            get
            {
                return this.bufferUsage;
            }
        }

        #endregion

		#region Constructor

		public ConstantBuffer(GraphicsDevice graphicsDevice, BufferUsage usage)
			: base(graphicsDevice)
		{
			this.bufferUsage = usage;

			base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

			CreateNativeBuffer();
		}

		~ConstantBuffer()
		{
			Dispose();
			base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
		}
		#endregion

		#region GraphicsDevice_ResourceDestroyed
		private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
		{
			if (nativeConstantBuffer != null)
			{
				nativeConstantBuffer.Dispose();
				nativeConstantBuffer = null;
			}
		}
		#endregion

		#region GraphicsDevice_ResourceCreated
		private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
		{
			if (nativeConstantBuffer != null)
			{
				nativeConstantBuffer.Dispose();
				nativeConstantBuffer = null;
			}

			CreateNativeBuffer();
		}
		#endregion

		#region CreateNativeBuffer
		private void CreateNativeBuffer()
		{
			this.nativeConstantBuffer = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateConstantBuffer(GraphicsDevice, this, bufferUsage);
		}
		#endregion

		#region GetData
		public void GetData<T>(T data) where T : struct
		{
			NativeConstantBuffer.GetData(data);
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			NativeConstantBuffer.GetData(data, startIndex, elementCount);
		}
		#endregion

		#region SetData
		public void SetData<T>(T data) where T : struct
		{
			NativeConstantBuffer.SetData(GraphicsDevice, data);
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			if (nativeConstantBuffer != null)
			{
				nativeConstantBuffer.Dispose();
				nativeConstantBuffer = null;
			}
		}

		#endregion
    }

#endif
}
