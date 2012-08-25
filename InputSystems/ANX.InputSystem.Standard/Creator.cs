using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Standard
{
	public class Creator : IInputSystemCreator
	{
		private IKeyboard keyboard;
		private IMouse mouse;
		private IGamePad gamePad;
		private ITouchPanel touchPanel;

		public string Name
		{
			get
			{
				return "Standard";
			}
		}

		public int Priority
		{
			get
			{
				return 0;
			}
		}

		public bool IsSupported
		{
			get
			{
				return true;
			}
		}

		public ITouchPanel TouchPanel
		{
			get
			{
				if (touchPanel == null)
				{
					Logger.Info("Creating a new TouchPanel device.");
					PreventSystemChange();
					touchPanel = InputDeviceFactory.Instance.GetDefaultTouchPanel();
				}

				return touchPanel;
			}
		}

		public IGamePad GamePad
		{
			get
			{
				if (gamePad == null)
				{
					Logger.Info("Creating a new GamePad device.");
					PreventSystemChange();
					gamePad = InputDeviceFactory.Instance.CreateDefaultGamePad();
				}

				return gamePad;
			}
		}

		public IMouse Mouse
		{
			get
			{
				if (mouse == null)
				{
					mouse = InputDeviceFactory.Instance.CreateDefaultMouse();
					if (mouse == null)
						throw new NoInputDeviceException("Couldn't find a default mouse device creator. Unable to create a mouse instance.");

					Logger.Info("created a new Mouse device");
					PreventSystemChange();
				}

				return this.mouse;
			}
		}

		public IKeyboard Keyboard
		{
			get
			{
				if (keyboard == null)
				{
					keyboard = InputDeviceFactory.Instance.CreateDefaultKeyboard();
					if (keyboard == null)
						throw new NoInputDeviceException("Couldn't find a default keyboard device creator. Unable to create a keyboard instance.");

					Logger.Info("created a new Keyboard device");
					PreventSystemChange();
				}

				return this.keyboard;
			}
		}

#if XNAEXT
		public IMotionSensingDevice MotionSensingDevice
		{
			get
			{
				Logger.Info("Creating a new MotionSensingDevice device.");
				PreventSystemChange();
				return InputDeviceFactory.Instance.CreateDefaultMotionSensingDevice();
			}
		}
#endif

		private void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
		}
	}
}
