using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class DepthStencilState_Metro : INativeDepthStencilState
	{
		#region Private Members
		private Dx11.DepthStencilStateDescription description;
		private Dx11.DepthStencilState nativeDepthStencilState;
		private bool nativeDepthStencilStateDirty;
		private bool bound;

		private int referenceStencil;

		#endregion // Private Members

		public DepthStencilState_Metro()
		{
			this.description = new Dx11.DepthStencilStateDescription();

			this.nativeDepthStencilStateDirty = true;
		}

		public void Apply(ANX.Framework.Graphics.GraphicsDevice graphicsDevice)
		{
			GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice.NativeDevice;
			var context = gdMetro.NativeDevice.NativeContext;

			UpdateNativeDepthStencilState(device);
			this.bound = true;

			context.OutputMerger.SetDepthStencilState(nativeDepthStencilState, this.referenceStencil);
		}

		public void Release()
		{
			this.bound = false;
		}

		public void Dispose()
		{
			if (this.nativeDepthStencilState != null)
			{
				this.nativeDepthStencilState.Dispose();
				this.nativeDepthStencilState = null;
			}
		}

		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		public ANX.Framework.Graphics.StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.BackFace.DepthFailOperation != operation)
				{
					description.BackFace.DepthFailOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public ANX.Framework.Graphics.StencilOperation CounterClockwiseStencilFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.BackFace.FailOperation != operation)
				{
					description.BackFace.FailOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public CompareFunction CounterClockwiseStencilFunction
		{
			set
			{
				Dx11.Comparison comparison = FormatConverter.Translate(value);

				if (description.BackFace.Comparison != comparison)
				{
					description.BackFace.Comparison = comparison;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public StencilOperation CounterClockwiseStencilPass
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.BackFace.PassOperation != operation)
				{
					description.BackFace.PassOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public bool DepthBufferEnable
		{
			set
			{
				if (description.IsDepthEnabled != value)
				{
					description.IsDepthEnabled = value;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public CompareFunction DepthBufferFunction
		{
			set
			{
				Dx11.Comparison comparison = FormatConverter.Translate(value);

				if (description.DepthComparison != comparison)
				{
					description.DepthComparison = comparison;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public bool DepthBufferWriteEnable
		{
			set
			{
				Dx11.DepthWriteMask writeMask = value ?
					Dx11.DepthWriteMask.All :
					Dx11.DepthWriteMask.Zero;

				if (description.DepthWriteMask != writeMask)
				{
					description.DepthWriteMask = writeMask;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public int ReferenceStencil
		{
			set
			{
				if (this.referenceStencil != value)
				{
					this.referenceStencil = value;
					this.nativeDepthStencilStateDirty = true;
				}
			}
		}

		public StencilOperation StencilDepthBufferFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.FrontFace.DepthFailOperation != operation)
				{
					description.FrontFace.DepthFailOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public bool StencilEnable
		{
			set
			{
				if (description.IsStencilEnabled != value)
				{
					description.IsStencilEnabled = value;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public ANX.Framework.Graphics.StencilOperation StencilFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.FrontFace.FailOperation != operation)
				{
					description.FrontFace.FailOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public ANX.Framework.Graphics.CompareFunction StencilFunction
		{
			set
			{
				Dx11.Comparison comparison = FormatConverter.Translate(value);

				if (description.FrontFace.Comparison != comparison)
				{
					description.FrontFace.Comparison = comparison;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public int StencilMask
		{
			set
			{
				byte stencilMask = (byte)value;         //TODO: check range

				if (description.StencilReadMask != stencilMask)
				{
					description.StencilReadMask = stencilMask;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public ANX.Framework.Graphics.StencilOperation StencilPass
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);

				if (description.FrontFace.PassOperation != operation)
				{
					description.FrontFace.PassOperation = operation;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public int StencilWriteMask
		{
			set
			{
				byte stencilWriteMask = (byte)value;        //TODO: check range

				if (description.StencilWriteMask != stencilWriteMask)
				{
					description.StencilWriteMask = stencilWriteMask;
					nativeDepthStencilStateDirty = true;
				}
			}
		}

		public bool TwoSidedStencilMode
		{
			set
			{
				//TODO: check if we really need this. in xna this enables only counter clockwise stencil operations
			}
		}

		private void UpdateNativeDepthStencilState(Dx11.Device1 device)
		{
			if (this.nativeDepthStencilStateDirty == true || this.nativeDepthStencilState == null)
			{
				if (this.nativeDepthStencilState != null)
				{
					this.nativeDepthStencilState.Dispose();
					this.nativeDepthStencilState = null;
				}

				this.nativeDepthStencilState = new Dx11.DepthStencilState(device, ref this.description);

				this.nativeDepthStencilStateDirty = false;
			}
		}
	}
}
