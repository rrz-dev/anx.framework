using System;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
	public class Mouse : IMouse
	{
		private IntPtr windowHandle;

		public IntPtr WindowHandle
		{
			get
			{
				return this.windowHandle;
			}
			set
			{
				this.windowHandle = value;
			}
		}

		public MouseState GetState()
		{
			throw new NotImplementedException();
		}

		public void SetPosition(int x, int y)
		{
			throw new NotImplementedException();
		}
	}
}
