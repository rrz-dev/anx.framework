#region Using Statements
using System.Collections.Generic;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class GraphicsResourceTracker
	{
		#region Private members
		private static GraphicsResourceTracker instance;
		private static List<GraphicsResource> trackedGraphicsResources;
        private GraphicsDevice graphicsDevice;

		#endregion // Private members

		static GraphicsResourceTracker()
		{
			trackedGraphicsResources = new List<GraphicsResource>();
		}

		//~GraphicsResourceTracker()
		//{
		//    if (trackedGraphicsResources.Count > 0)
		//    {
		//        throw new Exception("The GraphicsResourceTracker is going to be destroyed but is still tracking some objects.");
		//    }
		//}

		public static GraphicsResourceTracker Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GraphicsResourceTracker();
				}

				return instance;
			}
		}

		public void UpdateGraphicsDeviceReference(GraphicsDevice newGraphicsDevice)
		{
            this.graphicsDevice = newGraphicsDevice;

			foreach (GraphicsResource resource in trackedGraphicsResources)
			{
				resource.GraphicsDevice = newGraphicsDevice;
				resource.GraphicsDevice.NativeDevice = newGraphicsDevice.NativeDevice;
			}
		}

		public void RegisterTrackedObject(GraphicsResource graphicsResource)
		{
			if (trackedGraphicsResources.Contains(graphicsResource) == false)
			{
                if (graphicsDevice != null && graphicsResource.GraphicsDevice == null)
                {
                    graphicsResource.GraphicsDevice = graphicsDevice;
                }
				trackedGraphicsResources.Add(graphicsResource);
			}
		}

		public void UnregisterTrackedObject(GraphicsResource graphicsResource)
		{
			trackedGraphicsResources.Remove(graphicsResource);
		}
	}
}
