#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(80)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class DynamicIndexBuffer : IndexBuffer, IDynamicGraphicsResource
    {
        public virtual event EventHandler<EventArgs> ContentLost;

        #region Private Members
        private bool isContentLost;

        #endregion

        public DynamicIndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage)
            : base(graphicsDevice, indexElementSize, indexCount, usage)
        {
            graphicsDevice.DeviceReset += graphicsDevice_DeviceReset;
        }

        public DynamicIndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage)
            : base(graphicsDevice, indexType, indexCount, usage)
        {
            graphicsDevice.DeviceReset += graphicsDevice_DeviceReset;
        }

        ~DynamicIndexBuffer()
        {
            base.GraphicsDevice.DeviceReset -= graphicsDevice_DeviceReset;
        }

        private void graphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            SetContentLost(true);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
        {
            //TODO: SetDataOptions not used
            base.SetData<T>(offsetInBytes, data, startIndex, elementCount);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
        {
            //TODO: SetDataOptions not used
            base.SetData<T>(data, startIndex, elementCount);
        }

        public bool IsContentLost
        {
            get
            {
                return this.isContentLost;
            }
        }

        public void SetContentLost(bool isContentLost)
        {
            this.isContentLost = isContentLost;
            if (isContentLost)
            {
                raise_ContentLost(this, EventArgs.Empty);
            }
        }

        protected void raise_ContentLost(object sender, EventArgs args)
        {
            if (ContentLost != null)
            {
                ContentLost(sender, args);
            }
        }

    }
}
