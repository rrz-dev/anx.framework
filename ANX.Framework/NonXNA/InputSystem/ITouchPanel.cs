using System;
using ANX.Framework.Input.Touch;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public interface ITouchPanel : IDisposable
	{
		GestureType EnabledGestures { get; set; }
		bool IsGestureAvailable { get; }
		IntPtr WindowHandle { get; set; }
		DisplayOrientation DisplayOrientation { get; set; }
		int DisplayWidth { get; set; }
		int DisplayHeight { get; set; }

		TouchPanelCapabilities GetCapabilities();
		GestureSample ReadGesture();
		TouchCollection GetState();
    }
}
