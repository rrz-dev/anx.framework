using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	public static class TouchPanel
	{
		private static readonly ITouchPanel nativeTouchPanel;

		#region Public
	    public static GestureType EnabledGestures
	    {
	        get { return nativeTouchPanel.EnabledGestures; }
	        set { nativeTouchPanel.EnabledGestures = value; }
	    }

	    public static bool IsGestureAvailable
	    {
	        get { return nativeTouchPanel.IsGestureAvailable; }
	    }

	    public static IntPtr WindowHandle
	    {
	        get { return nativeTouchPanel.WindowHandle; }
	        set { nativeTouchPanel.WindowHandle = value; }
	    }

	    public static DisplayOrientation DisplayOrientation
	    {
	        get { return nativeTouchPanel.DisplayOrientation; }
	        set { nativeTouchPanel.DisplayOrientation = value; }
	    }

	    public static int DisplayWidth
	    {
	        get { return nativeTouchPanel.DisplayWidth; }
	        set { nativeTouchPanel.DisplayWidth = value; }
	    }

	    public static int DisplayHeight
	    {
	        get { return nativeTouchPanel.DisplayHeight; }
	        set { nativeTouchPanel.DisplayHeight = value; }
	    }
	    #endregion

		static TouchPanel()
		{
			nativeTouchPanel = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().TouchPanel;
		}

		public static TouchPanelCapabilities GetCapabilities()
		{
			return nativeTouchPanel.GetCapabilities();
		}

		public static GestureSample ReadGesture()
		{
			return nativeTouchPanel.ReadGesture();
		}

		public static TouchCollection GetState()
		{
			return nativeTouchPanel.GetState();
		}
	}
}
