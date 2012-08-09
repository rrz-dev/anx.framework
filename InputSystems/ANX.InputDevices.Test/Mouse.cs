using System;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.InputDevices.Test
{
	public class Mouse : IMouse
	{
		private int x;
		private int y;

		public IntPtr WindowHandle
		{
			get;
			set;
		}

		public MouseState GetState()
		{
			return new MouseState(x, y, 0, ButtonState.Released, ButtonState.Released,
				ButtonState.Released, ButtonState.Released, ButtonState.Released);
		}

		public void SetPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
