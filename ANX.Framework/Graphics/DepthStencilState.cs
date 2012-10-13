#region Using Statements
using System;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class DepthStencilState : GraphicsResource
    {
        #region Private Members
        private INativeDepthStencilState nativeDepthStencilState;

        private StencilOperation counterClockwiseStencilDepthBufferFail;
        private StencilOperation counterClockwiseStencilFail;
        private CompareFunction counterClockwiseStencilFunction;
        private StencilOperation counterClockwiseStencilPass;
        private bool depthBufferEnable;
        private CompareFunction depthBufferFunction;
        private bool depthBufferWriteEnable;
        private int referenceStencil;
        private StencilOperation stencilDepthBufferFail;
        private bool stencilEnable;
        private StencilOperation stencilFail;
        private CompareFunction stencilFunction;
        private int stencilMask;
        private StencilOperation stencilPass;
        private int stencilWriteMask;
        private bool twoSidedStencilMode;

        #endregion

        public static readonly DepthStencilState Default;
        public static readonly DepthStencilState DepthRead;
        public static readonly DepthStencilState None;

        public DepthStencilState()
        {
            nativeDepthStencilState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateDepthStencilState();

            // BackFace
            CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep;
            CounterClockwiseStencilFail = StencilOperation.Keep;
            CounterClockwiseStencilFunction = CompareFunction.Always;
            CounterClockwiseStencilPass = StencilOperation.Keep;

            // FrontFace
            StencilDepthBufferFail = StencilOperation.Keep;
            StencilFail = StencilOperation.Keep;
            StencilFunction = CompareFunction.Always;
            StencilPass = StencilOperation.Keep;

            DepthBufferEnable = true;
            DepthBufferFunction = CompareFunction.LessEqual;
            DepthBufferWriteEnable = true;
            ReferenceStencil = 0;
            StencilEnable = false;
            StencilMask = int.MaxValue;
            StencilWriteMask = int.MaxValue;
            TwoSidedStencilMode = false;
        }

        private DepthStencilState(bool depthBufferEnabled, bool depthBufferWriteEnabled, string name)
        {
            nativeDepthStencilState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateDepthStencilState();

            // BackFace
            CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep;
            CounterClockwiseStencilFail = StencilOperation.Keep;
            CounterClockwiseStencilFunction = CompareFunction.Always;
            CounterClockwiseStencilPass = StencilOperation.Keep;
            
            // FrontFace
            StencilDepthBufferFail = StencilOperation.Keep;
            StencilFail = StencilOperation.Keep;
            StencilFunction = CompareFunction.Always;
            StencilPass = StencilOperation.Keep;

            DepthBufferEnable = depthBufferEnabled;
            DepthBufferFunction = CompareFunction.LessEqual;
            DepthBufferWriteEnable = depthBufferWriteEnabled;
            ReferenceStencil = 0;
            StencilEnable = false;
            StencilMask = int.MaxValue;
            StencilWriteMask = int.MaxValue;
            TwoSidedStencilMode = false;

            Name = name;
        }

        static DepthStencilState()
        {
            None = new DepthStencilState(false, false, "DepthStencilState.None");
            Default = new DepthStencilState(true, true, "DepthStencilState.Default");
            DepthRead = new DepthStencilState(true, false, "DepthStencilState.DepthRead");
        }

        internal INativeDepthStencilState NativeDepthStencilState
        {
            get
            {
                return this.nativeDepthStencilState;
            }
        }

        public StencilOperation CounterClockwiseStencilDepthBufferFail
        {
            get { return this.counterClockwiseStencilDepthBufferFail; }
            set
            {
                ThrowIfBound();

                this.counterClockwiseStencilDepthBufferFail = value;
                this.nativeDepthStencilState.CounterClockwiseStencilDepthBufferFail = value;
            }
        }

        public StencilOperation CounterClockwiseStencilFail
        {
            get { return this.counterClockwiseStencilFail; }
            set
            {
                ThrowIfBound();

                this.counterClockwiseStencilFail = value;
                this.nativeDepthStencilState.CounterClockwiseStencilFail = value;
            }
        }

        public CompareFunction CounterClockwiseStencilFunction
        {
            get { return this.counterClockwiseStencilFunction; }
            set
            {
                ThrowIfBound();

                this.counterClockwiseStencilFunction = value;
                this.nativeDepthStencilState.CounterClockwiseStencilFunction = value;
            }
        }

        public StencilOperation CounterClockwiseStencilPass
        {
            get { return this.counterClockwiseStencilPass; }
            set
            {
                ThrowIfBound();

                this.counterClockwiseStencilPass = value;
                this.nativeDepthStencilState.CounterClockwiseStencilPass = value;
            }
        }

        public bool DepthBufferEnable
        {
            get { return this.depthBufferEnable; }
            set
            {
                ThrowIfBound();

                this.depthBufferEnable = value;
                this.nativeDepthStencilState.DepthBufferEnable = value;
            }
        }

        public CompareFunction DepthBufferFunction
        {
            get { return this.depthBufferFunction; }
            set
            {
                ThrowIfBound();

                this.depthBufferFunction = value;
                this.nativeDepthStencilState.DepthBufferFunction = value;
            }
        }

        public bool DepthBufferWriteEnable
        {
            get { return this.depthBufferWriteEnable; }
            set
            {
                ThrowIfBound();

                this.depthBufferWriteEnable = value;
                this.nativeDepthStencilState.DepthBufferWriteEnable = value;
            }
        }

        public int ReferenceStencil
        {
            get { return this.referenceStencil; }
            set
            {
                ThrowIfBound();

                this.referenceStencil = value;
                this.nativeDepthStencilState.ReferenceStencil = value;
            }
        }

        public StencilOperation StencilDepthBufferFail
        {
            get { return this.stencilDepthBufferFail; }
            set
            {
                ThrowIfBound();

                this.stencilDepthBufferFail = value;
                this.nativeDepthStencilState.StencilDepthBufferFail = value;
            }
        }

        public bool StencilEnable
        {
            get { return this.stencilEnable; }
            set
            {
                ThrowIfBound();

                this.stencilEnable = value;
                this.nativeDepthStencilState.StencilEnable = value;
            }
        }

        public StencilOperation StencilFail
        {
            get { return this.stencilFail; }
            set
            {
                ThrowIfBound();

                this.stencilFail = value;
                this.nativeDepthStencilState.StencilFail = value;
            }
        }

        public CompareFunction StencilFunction
        {
            get { return this.stencilFunction; }
            set
            {
                ThrowIfBound();

                this.stencilFunction = value;
                this.nativeDepthStencilState.StencilFunction = value;
            }
        }

        public int StencilMask
        {
            get { return this.stencilMask; }
            set
            {
                ThrowIfBound();

                this.stencilMask = value;
                this.nativeDepthStencilState.StencilMask = value;
            }
        }

        public StencilOperation StencilPass
        {
            get { return this.stencilPass; }
            set
            {
                ThrowIfBound();

                this.stencilPass = value;
                this.nativeDepthStencilState.StencilPass = value;
            }
        }

        public int StencilWriteMask
        {
            get { return this.stencilWriteMask; }
            set
            {
                ThrowIfBound();

                this.stencilWriteMask = value;
                this.nativeDepthStencilState.StencilWriteMask = value;
            }
        }

        public bool TwoSidedStencilMode
        {
            get { return this.twoSidedStencilMode; }
            set
            {
                ThrowIfBound();

                this.twoSidedStencilMode = value;
                this.nativeDepthStencilState.TwoSidedStencilMode = value;
            }
        }

        private void ThrowIfBound()
        {
            if (nativeDepthStencilState.IsBound)
                throw new InvalidOperationException(
                    "You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
        }

        public override void Dispose()
        {
            if (this.nativeDepthStencilState != null)
            {
                this.nativeDepthStencilState.Dispose();
                this.nativeDepthStencilState = null;
            }
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
