using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
	public class GraphicsDeviceInformation
	{
		#region Public
	    public GraphicsAdapter Adapter { get; set; }
	    public GraphicsProfile GraphicsProfile { get; set; }
	    public PresentationParameters PresentationParameters { get; set; }
	    #endregion

		#region Constructor
		public GraphicsDeviceInformation()
		{
			PresentationParameters = new PresentationParameters();
			Adapter = GraphicsAdapter.DefaultAdapter;
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return GraphicsProfile.GetHashCode() ^
				Adapter.GetHashCode() ^
				PresentationParameters.BackBufferWidth.GetHashCode() ^
				PresentationParameters.BackBufferHeight.GetHashCode() ^
				PresentationParameters.BackBufferFormat.GetHashCode() ^
				PresentationParameters.DepthStencilFormat.GetHashCode() ^
				PresentationParameters.MultiSampleCount.GetHashCode() ^
				PresentationParameters.DisplayOrientation.GetHashCode() ^
				PresentationParameters.PresentationInterval.GetHashCode() ^
				PresentationParameters.RenderTargetUsage.GetHashCode() ^
				PresentationParameters.DeviceWindowHandle.GetHashCode() ^
				PresentationParameters.IsFullScreen.GetHashCode();
		}
		#endregion

		#region Equals
		public override bool Equals(object obj)
		{
			GraphicsDeviceInformation other = obj as GraphicsDeviceInformation;

			if (other != null)
			{
				return Adapter.Equals(other.Adapter) &&
					GraphicsProfile.Equals(other.GraphicsProfile) &&
					PresentationParameters.Equals(other.PresentationParameters);
			}

			return false;
		}
		#endregion

		#region Clone
		public GraphicsDeviceInformation Clone()
		{
			return new GraphicsDeviceInformation()
			{
				PresentationParameters = PresentationParameters.Clone(),
				Adapter = Adapter,
				GraphicsProfile = GraphicsProfile
			};
		}
		#endregion
	}
}
