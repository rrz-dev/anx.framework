using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.PsVita
{
	public class TouchPanelCreator : ITouchPanelCreator
	{
		public string Name
		{
			get
			{
				return "PsVita.TouchPanel";
			}
		}

		public int Priority
		{
			get
			{
				return 10;
			}
		}

		public ITouchPanel CreateDevice()
		{
			return new TouchPanel();
		}
	}
}
