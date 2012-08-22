using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Test
{
	public class Creator : IInputSystemCreator
	{
		private Mouse _mouse;
		private Keyboard _keyboard;
		private GamePad _gamePad;

		public IGamePad GamePad
		{
			get
			{
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				if (_gamePad == null)
					_gamePad = new GamePad();
				return _gamePad;
			}
		}

		public IMouse Mouse
		{
			get
			{
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				if (_mouse == null)
					_mouse = new Mouse();
				return _mouse;
			}
		}

		public IKeyboard Keyboard
		{
			get
			{
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				if (_keyboard == null)
					_keyboard = new Keyboard();
				return _keyboard;
			}
		}

		public ITouchPanel TouchPanel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMotionSensingDevice MotionSensingDevice
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Name
		{
			get
			{
				return "Test";
			}
		}

		public int Priority
		{
			get
			{
				return int.MaxValue;
			}
		}

		public bool IsSupported
		{
			get
			{
				// This is just for test, so it runs on all plattforms
				return true;
			}
		}

		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
	}
}
