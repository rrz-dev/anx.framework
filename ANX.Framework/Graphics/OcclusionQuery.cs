using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("AstrorEnales")]
	public class OcclusionQuery : GraphicsResource, IGraphicsResource
	{
		#region Private
		private bool hasBegun;
		private bool completeCallPending;

		private IOcclusionQuery nativeQuery;
		#endregion

		#region Public
		public bool IsComplete
		{
			get
			{
				return nativeQuery.IsComplete;
			}
		}

		public int PixelCount
		{
			get
			{
				if (this.completeCallPending)
					throw new InvalidOperationException("The status of the query data is unknown. Use the IsComplete " +
						"property to determine if the data is available before attempting to retrieve it.");

				return nativeQuery.PixelCount;
			}
		}
		#endregion

		#region Constructor
		public OcclusionQuery(GraphicsDevice graphicsDevice)
			: base(graphicsDevice)
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			nativeQuery = creator.CreateOcclusionQuery();
		}

		~OcclusionQuery()
		{
			Dispose(true);
		}
		#endregion

		#region Begin
		public void Begin()
		{
			if (hasBegun)
				throw new InvalidOperationException("Begin cannot be called again until End is called.");

			if (completeCallPending)
				throw new InvalidOperationException("Begin may not be called on this query object again before IsComplete " +
					"is checked.");

			hasBegun = true;
			completeCallPending = true;

			nativeQuery.Begin();
		}
		#endregion

		#region End
		public void End()
		{
			if (this.hasBegun == false)
				throw new InvalidOperationException("Begin must be called before End can be called.");

			nativeQuery.End();
		}
		#endregion

		#region Dispose
		protected override void Dispose(bool disposeManaged)
		{
            if (disposeManaged)
            {
                if (nativeQuery != null)
                {
                    nativeQuery.Dispose();
                    nativeQuery = null;
                }
            }

            base.Dispose(disposeManaged);
		}
		#endregion
	}
}
