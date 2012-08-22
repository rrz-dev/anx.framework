using System;
using System.Collections.Generic;
using ANX.Framework;
using ANX.Framework.Input.Touch;
using ANX.Framework.NonXNA;
using Sce.PlayStation.Core.Input;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.PsVita
{
	public class TouchPanel : ITouchPanel
	{
		#region Constants
		private static readonly TouchPanelCapabilities caps = new TouchPanelCapabilities()
		{
			IsConnected = true,
			MaximumTouchCount = 6,
		};
		#endregion

		#region Public
		public GestureType EnabledGestures
		{
			get;
			set;
		}

		public bool IsGestureAvailable
		{
			get
			{
				// TODO
				return false;
			}
		}

		public IntPtr WindowHandle
		{
			get;
			set;
		}

		public DisplayOrientation DisplayOrientation
		{
			get;
			set;
		}

		public int DisplayWidth
		{
			get;
			set;
		}

		public int DisplayHeight
		{
			get;
			set;
		}
		#endregion

		#region GetCapabilities
		public TouchPanelCapabilities GetCapabilities()
		{
			return caps;
		}
		#endregion

		#region ReadGesture
		public GestureSample ReadGesture()
		{
			//return new GestureSample()
			//{
			//    GestureType = GestureType.
			//};
			//MotionData motion = Motion.GetData(0);
			throw new NotImplementedException();
		}
		#endregion

		#region GetState
		public TouchCollection GetState()
		{
			var touchLocations = new List<TouchLocation>();

			var allTouches = Touch.GetData(0);
			foreach (var touch in allTouches)
			{
				touchLocations.Add(new TouchLocation(touch.ID, TranslateTouchStatus(touch.Status),
					new Vector2(touch.X, touch.Y)));
			}
			
			return new TouchCollection(touchLocations.ToArray());
		}
		#endregion

		#region TranslateTouchStatus
		private TouchLocationState TranslateTouchStatus(TouchStatus status)
		{
			switch (status)
			{
				case TouchStatus.Down:
					return TouchLocationState.Pressed;
				case TouchStatus.Up:
				case TouchStatus.Canceled:
					return TouchLocationState.Released;
				case TouchStatus.Move:
					return TouchLocationState.Moved;
				case TouchStatus.None:
					return TouchLocationState.Invalid;
			}

			throw new ArgumentException("Can't translate TouchStatus '" + status + "'!");
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
		}
		#endregion
	}
}
