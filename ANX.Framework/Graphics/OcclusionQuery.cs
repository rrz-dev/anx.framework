#region Using Statements
using System;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class OcclusionQuery : GraphicsResource, IGraphicsResource
    {
        private bool hasBegun;
        private bool completeCallPending;

        public OcclusionQuery(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            if (this.hasBegun)
            {
                throw new InvalidOperationException("Begin cannot be called again until End is called.");
            }

            if (this.completeCallPending)
            {
                throw new InvalidOperationException("Begin may not be called on this query object again before IsComplete is checked.");
            }

            this.hasBegun = true;
            this.completeCallPending = true;

            throw new NotImplementedException();
        }

        public void End()
        {
            if (!this.hasBegun)
            {
                throw new InvalidOperationException("Begin must be called before End can be called.");
            }

            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            throw new NotImplementedException();
        }

        public bool IsComplete
        {
            get
            {
                this.completeCallPending = false;

                throw new NotImplementedException();
            }
        }

        public int PixelCount
        {
            get
            {
                if (this.completeCallPending)
                {
                    throw new InvalidOperationException("The status of the query data is unknown. Use the IsComplete property to determine if the data is available before attempting to retrieve it.");
                }

                throw new NotImplementedException();
            }
        }
    }
}
