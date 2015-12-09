using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.PsVita
{
	public class MouseCreator : IMouseCreator
	{
		public string Name
		{
			get
			{
				return "PsVita.Mouse";
			}
		}

		public int Priority
		{
			get
			{
				return 10;
			}
		}

		public IMouse CreateDevice()
		{
			throw new NotImplementedException();
		}


        public string Provider
        {
            get { return "Sce"; }
        }
    }
}
