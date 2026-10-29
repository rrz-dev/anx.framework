using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class RasterizerState : GraphicsResource
	{
		#region Constants
		public static readonly RasterizerState CullClockwise;
		public static readonly RasterizerState CullCounterClockwise;
		public static readonly RasterizerState CullNone;
		#endregion

		#region Private
		private INativeRasterizerState nativeRasterizerState;

		private CullMode cullMode;
		private float depthBias;
		private FillMode fillMode;
		private bool multiSampleAntiAlias;
		private bool scissorTestEnable;
		private float slopeScaleDepthBias;
		#endregion

		#region Public
		internal INativeRasterizerState NativeRasterizerState
		{
			get
			{
				return this.nativeRasterizerState;
			}
		}

		public CullMode CullMode
		{
			get
			{
				return this.cullMode;
			}
			set
			{
				ThrowIfBound();
				this.cullMode = value;
				this.nativeRasterizerState.CullMode = value;
			}
		}

		public float DepthBias
		{
			get
			{
				return this.depthBias;
			}
			set
			{
				ThrowIfBound();
				this.depthBias = value;
				this.nativeRasterizerState.DepthBias = value;
			}
		}

		public FillMode FillMode
		{
			get
			{
				return this.fillMode;
			}
			set
			{
				ThrowIfBound();
				this.fillMode = value;
				this.nativeRasterizerState.FillMode = value;
			}
		}

		public bool MultiSampleAntiAlias
		{
			get
			{
				return this.multiSampleAntiAlias;
			}
			set
			{
				ThrowIfBound();
				this.multiSampleAntiAlias = value;
				this.nativeRasterizerState.MultiSampleAntiAlias = value;
			}
		}

		public bool ScissorTestEnable
		{
			get
			{
				return this.scissorTestEnable;
			}
			set
			{
				ThrowIfBound();
				this.scissorTestEnable = value;
				this.nativeRasterizerState.ScissorTestEnable = value;
			}
		}

		public float SlopeScaleDepthBias
		{
			get
			{
				return this.slopeScaleDepthBias;
			}
			set
			{
				ThrowIfBound();
				this.slopeScaleDepthBias = value;
				this.nativeRasterizerState.SlopeScaleDepthBias = value;
			}
		}
		#endregion

		#region Constructor
		public RasterizerState()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.nativeRasterizerState = creator.CreateRasterizerState();

			this.CullMode = CullMode.CullCounterClockwiseFace;
			this.DepthBias = 0f;
			this.FillMode = FillMode.Solid;
			this.MultiSampleAntiAlias = true;
			this.ScissorTestEnable = false;
			this.SlopeScaleDepthBias = 0f;
		}

		private RasterizerState(CullMode cullMode, string name)
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.nativeRasterizerState = creator.CreateRasterizerState();

			this.CullMode = cullMode;
			this.DepthBias = 0f;
			this.FillMode = FillMode.Solid;
			this.MultiSampleAntiAlias = true;
			this.ScissorTestEnable = false;
			this.SlopeScaleDepthBias = 0f;
			Name = name;
		}

		static RasterizerState()
		{
			CullClockwise = new RasterizerState(CullMode.CullClockwiseFace, "RasterizerState.CullClockwise");
			CullCounterClockwise = new RasterizerState(CullMode.CullCounterClockwiseFace, "RasterizerState.CullCounterClockwise");
			CullNone = new RasterizerState(CullMode.None, "RasterizerState.CullNone");
		}
		#endregion

		#region ThrowIfBound
		private void ThrowIfBound()
		{
			if (this.nativeRasterizerState.IsBound)
				throw new InvalidOperationException("You are not allowed to change RasterizerState properties " +
					"while it is bound to the GraphicsDevice.");
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			if (this.nativeRasterizerState != null)
			{
				this.nativeRasterizerState.Dispose();
				this.nativeRasterizerState = null;
			}
		}

		protected override void Dispose(
			[MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			base.Dispose(disposeManaged);
		}
		#endregion
	}
}
