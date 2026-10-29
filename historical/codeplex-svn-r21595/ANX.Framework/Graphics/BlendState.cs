#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
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
    public class BlendState : GraphicsResource
    {
        #region Private Members
        private INativeBlendState nativeBlendState;

        private BlendFunction alphaBlendFunction;
        private Blend alphaDestinationBlend;
        private Blend alphaSourceBlend;
        private Color blendFactor;
        private BlendFunction colorBlendFunction;
        private Blend colorDestinationBlend;
        private Blend colorSourceBlend;
        private ColorWriteChannels colorWriteChannels0;
        private ColorWriteChannels colorWriteChannels1;
        private ColorWriteChannels colorWriteChannels2;
        private ColorWriteChannels colorWriteChannels3;
        private int multiSampleMask;

        #endregion // Private Members

        public static readonly BlendState Opaque;
        public static readonly BlendState AlphaBlend;
        public static readonly BlendState Additive;
        public static readonly BlendState NonPremultiplied;

        public BlendState()
        {
            this.nativeBlendState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateBlendState();

            this.AlphaBlendFunction = BlendFunction.Add;
            this.AlphaDestinationBlend = Blend.One;
            this.AlphaSourceBlend = Blend.One;
            this.BlendFactor = Color.White;
            this.ColorBlendFunction = BlendFunction.Add;
            this.ColorDestinationBlend = Blend.One;
            this.ColorSourceBlend = Blend.One;
            this.ColorWriteChannels = ColorWriteChannels.All;
            this.ColorWriteChannels1 = ColorWriteChannels.All;
            this.ColorWriteChannels2 = ColorWriteChannels.All;
            this.ColorWriteChannels3 = ColorWriteChannels.All;
            this.MultiSampleMask = -1;
        }

        private BlendState(Blend sourceBlend, Blend destinationBlend, string name)
        {
            this.nativeBlendState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateBlendState();

            this.AlphaBlendFunction = BlendFunction.Add;
            this.AlphaDestinationBlend = destinationBlend;
            this.AlphaSourceBlend = sourceBlend;
            this.BlendFactor = Color.White;
            this.ColorBlendFunction = BlendFunction.Add;
            this.ColorDestinationBlend = destinationBlend;
            this.ColorSourceBlend = sourceBlend;
            this.ColorWriteChannels = ColorWriteChannels.All;
            this.ColorWriteChannels1 = ColorWriteChannels.All;
            this.ColorWriteChannels2 = ColorWriteChannels.All;
            this.ColorWriteChannels3 = ColorWriteChannels.All;
            this.MultiSampleMask = -1;
            
            Name = name;
        }

        static BlendState()
        {
            Opaque = new BlendState(Blend.One, Blend.Zero, "BlendState.Opaque");
            AlphaBlend = new BlendState(Blend.One, Blend.InverseSourceAlpha, "BlendState.AlphaBlend");
            Additive = new BlendState(Blend.SourceAlpha, Blend.One, "BlendState.Additive");
            NonPremultiplied = new BlendState(Blend.SourceAlpha, Blend.InverseSourceAlpha, "BlendState.NonPremultiplied");
        }

        internal INativeBlendState NativeBlendState
        {
            get
            {
                return this.nativeBlendState;
            }
        }

        public BlendFunction AlphaBlendFunction
        {
            get { return this.alphaBlendFunction; }
            set
            {
                ThrowIfBound();

                this.alphaBlendFunction = value;
                this.nativeBlendState.AlphaBlendFunction = value;
            }
        }

        public Blend AlphaDestinationBlend
        {
            get { return this.alphaDestinationBlend; }
            set
            {
                ThrowIfBound();

                this.alphaDestinationBlend = value;
                this.nativeBlendState.AlphaDestinationBlend = value;
            }
        }

        public Blend AlphaSourceBlend
        {
            get { return this.alphaSourceBlend; }
            set
            {
                ThrowIfBound();

                this.alphaSourceBlend = value;
                this.nativeBlendState.AlphaSourceBlend = value;
            }
        }

        public Color BlendFactor
        {
            get { return this.blendFactor; }
            set
            {
                ThrowIfBound();

                this.blendFactor = value;
                this.nativeBlendState.BlendFactor = value;
            }
        }

        public BlendFunction ColorBlendFunction
        {
            get { return this.colorBlendFunction; }
            set
            {
                ThrowIfBound();

                this.colorBlendFunction = value;
                this.nativeBlendState.ColorBlendFunction = value;
            }
        }

        public Blend ColorDestinationBlend
        {
            get { return this.colorDestinationBlend; }
            set
            {
                ThrowIfBound();

                this.colorDestinationBlend = value;
                this.nativeBlendState.ColorDestinationBlend = value;
            }
        }

        public Blend ColorSourceBlend
        {
            get { return this.colorSourceBlend; }
            set
            {
                ThrowIfBound();

                this.colorSourceBlend = value;
                this.nativeBlendState.ColorSourceBlend = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels
        {
            get { return this.colorWriteChannels0; }
            set
            {
                ThrowIfBound();

                this.colorWriteChannels0 = value;
                this.nativeBlendState.ColorWriteChannels = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels1
        {
            get { return this.colorWriteChannels1; }
            set
            {
                ThrowIfBound();

                this.colorWriteChannels1 = value;
                this.nativeBlendState.ColorWriteChannels1 = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels2
        {
            get { return this.colorWriteChannels2; }
            set
            {
                ThrowIfBound();

                this.colorWriteChannels2 = value;
                this.nativeBlendState.ColorWriteChannels2 = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels3
        {
            get { return this.colorWriteChannels3; }
            set
            {
                ThrowIfBound();

                this.colorWriteChannels3 = value;
                this.nativeBlendState.ColorWriteChannels3 = value;
            }
        }

        public int MultiSampleMask
        {
            get { return this.multiSampleMask; }
            set
            {
                ThrowIfBound();

                this.multiSampleMask = value;
                this.nativeBlendState.MultiSampleMask = value;
            }
        }

        private void ThrowIfBound()
        {
            if (nativeBlendState.IsBound)
                throw new InvalidOperationException(
                    "You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
        }

        public override void Dispose()
        {
            if (this.nativeBlendState != null)
            {
                this.nativeBlendState.Dispose();
                this.nativeBlendState = null;
            }
        }

        protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
