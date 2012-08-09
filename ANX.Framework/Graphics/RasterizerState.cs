using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	public class RasterizerState : GraphicsResource
	{
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
		public static readonly RasterizerState CullClockwise;
		public static readonly RasterizerState CullCounterClockwise;
		public static readonly RasterizerState CullNone;

		#region NativeRasterizerState
		internal INativeRasterizerState NativeRasterizerState
		{
			get
			{
				return this.nativeRasterizerState;
			}
		}
		#endregion

		#region CullMode
		public CullMode CullMode
		{
			get
			{
				return this.cullMode;
			}
			set
			{
				ValidateSetProperty();

				this.cullMode = value;
				this.nativeRasterizerState.CullMode = value;
			}
		}
		#endregion

		#region DepthBias
		public float DepthBias
		{
			get
			{
				return this.depthBias;
			}
			set
			{
				ValidateSetProperty();

				this.depthBias = value;
				this.nativeRasterizerState.DepthBias = value;
			}
		}
		#endregion

		#region FillMode
		public FillMode FillMode
		{
			get
			{
				return this.fillMode;
			}
			set
			{
				ValidateSetProperty();

				this.fillMode = value;
				this.nativeRasterizerState.FillMode = value;
			}
		}
		#endregion

		#region MultiSampleAntiAlias
		public bool MultiSampleAntiAlias
		{
			get
			{
				return this.multiSampleAntiAlias;
			}
			set
			{
				ValidateSetProperty();

				this.multiSampleAntiAlias = value;
				this.nativeRasterizerState.MultiSampleAntiAlias = value;
			}
		}
		#endregion

		#region SlopeScaleDepthBias
		public bool ScissorTestEnable
		{
			get
			{
				return this.scissorTestEnable;
			}
			set
			{
				ValidateSetProperty();

				this.scissorTestEnable = value;
				this.nativeRasterizerState.ScissorTestEnable = value;
			}
		}
		#endregion

		#region SlopeScaleDepthBias
		public float SlopeScaleDepthBias
		{
			get
			{
				return this.slopeScaleDepthBias;
			}
			set
			{
				ValidateSetProperty();

				this.slopeScaleDepthBias = value;
				this.nativeRasterizerState.SlopeScaleDepthBias = value;
			}
		}
		#endregion
		#endregion

		#region Constructor
		public RasterizerState()
		{
			this.nativeRasterizerState =
				AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>()
				.CreateRasterizerState();

			this.CullMode = CullMode.CullCounterClockwiseFace;
			this.DepthBias = 0f;
			this.FillMode = FillMode.Solid;
			this.MultiSampleAntiAlias = true;
			this.ScissorTestEnable = false;
			this.SlopeScaleDepthBias = 0f;
		}

		private RasterizerState(CullMode cullMode, string name)
		{
			this.nativeRasterizerState =
				AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>()
				.CreateRasterizerState();

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
			CullClockwise = new RasterizerState(CullMode.CullClockwiseFace,
				"RasterizerState.CullClockwise");
			CullCounterClockwise = new RasterizerState(CullMode.CullCounterClockwiseFace,
					"RasterizerState.CullCounterClockwise");
			CullNone = new RasterizerState(CullMode.None, "RasterizerState.CullNone");
		}
		#endregion

		#region ValidateSetProperty (private helper)
		private void ValidateSetProperty()
		{
			if (this.nativeRasterizerState.IsBound)
			{
				throw new InvalidOperationException(
					"You are not allowed to change RasterizerState properties " +
					"while it is bound to the GraphicsDevice.");
			}
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
