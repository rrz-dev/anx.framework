using System;
using System.Runtime.InteropServices;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using DInput = SharpDX.DirectInput;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license


namespace ANX.InputDevices.Windows.XInput
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
    public class Mouse : IMouse
    {
		private DInput.DirectInput directInput;
		private DInput.Mouse mouse;

        public IntPtr WindowHandle { get; set; }

        public Mouse()
        {
			directInput = new DInput.DirectInput();
			mouse = new DInput.Mouse(directInput);
			mouse.Properties.AxisMode = DInput.DeviceAxisMode.Absolute;
            mouse.Acquire();
        }

		public MouseState GetState()
		{
			var state = this.mouse.GetCurrentState();

			Point cursorPos = new Point();
			GetCursorPos(ref cursorPos);
			if (WindowHandle != IntPtr.Zero)
				ScreenToClient(WindowHandle, ref cursorPos);

			ButtonState left = state.Buttons[0] ? ButtonState.Pressed : ButtonState.Released;
			ButtonState middle = state.Buttons[1] ? ButtonState.Pressed : ButtonState.Released;
			ButtonState right = state.Buttons[2] ? ButtonState.Pressed : ButtonState.Released;
			ButtonState x1 = state.Buttons[3] ? ButtonState.Pressed : ButtonState.Released;
			ButtonState x2 = state.Buttons[4] ? ButtonState.Pressed : ButtonState.Released;

			return new MouseState(cursorPos.X, cursorPos.Y, state.Z, left, middle, right, x1, x2);
		}

        public void SetPosition(int x, int y)
        {
            Point currentPosition = new Point(x, y);
            if (WindowHandle != IntPtr.Zero)
				ClientToScreen(WindowHandle, ref currentPosition);
            SetCursorPos(currentPosition.X, currentPosition.Y);
        }

		[DllImport("user32.dll")]
		static extern bool GetCursorPos(ref Point lpPoint);

		[DllImport("user32.dll")]
		static extern void SetCursorPos(int x, int y);

		[DllImport("user32.dll")]
		static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

		[DllImport("user32.dll")]
		static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
    }
}
