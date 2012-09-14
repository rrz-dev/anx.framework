using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("AstrorEnales")]
	public class OcclusionQueryGL3 : IOcclusionQuery
	{
		private uint[] handle;

		#region Public
		public bool IsComplete
		{
			get
			{
				int state;
				GL.GetQueryObject(handle[0], GetQueryObjectParam.QueryResultAvailable, out state);
				return state != 0;
			}
		}

		public int PixelCount
		{
			get
			{
				int result;
				GL.GetQueryObject(handle[0], GetQueryObjectParam.QueryResult, out result);
				return result;
			}
		}
		#endregion

		#region Constructor
		public OcclusionQueryGL3()
		{
			handle = new uint[1];
			GL.GenQueries(1, handle);
		}
		#endregion

		#region Begin
		public void Begin()
		{
			//GLCore.ColorMask(false, false, false, false);
			//GLCore.DepthMask(false);
			GL.BeginQuery(QueryTarget.SamplesPassed, handle[0]);
		}
		#endregion

		#region End
		public void End()
		{
			GL.EndQuery(QueryTarget.SamplesPassed);
			//GLCore.DepthMask(true);
			//GLCore.ColorMask(true, true, true, true);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			GL.DeleteQueries(1, handle);
		}
		#endregion
	}
}
