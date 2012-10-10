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
    public class DynamicVertexBuffer : VertexBuffer, IDynamicGraphicsResource
    {
        public virtual event EventHandler<EventArgs> ContentLost;

        #region Private Members
        private bool isContentLost;

        #endregion

        public DynamicVertexBuffer(GraphicsDevice graphicsDevice, Type vertexType, int vertexCount, BufferUsage usage)
            : base(graphicsDevice, vertexType, vertexCount, usage)
        {
            graphicsDevice.DeviceReset += new EventHandler<EventArgs>(graphicsDevice_DeviceReset);
        }

        public DynamicVertexBuffer(GraphicsDevice graphicsDevice, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
            : base(graphicsDevice, vertexDeclaration, vertexCount, usage)
        {
            graphicsDevice.DeviceReset += new EventHandler<EventArgs>(graphicsDevice_DeviceReset);
        }

        ~DynamicVertexBuffer()
        {
            base.GraphicsDevice.DeviceReset -= graphicsDevice_DeviceReset;
        }

        private void graphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            SetContentLost(true);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride, SetDataOptions options) where T : struct
        {
            //TODO: SetDataOptions not used
            base.SetData<T>(offsetInBytes, data, startIndex, elementCount, vertexStride);
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
