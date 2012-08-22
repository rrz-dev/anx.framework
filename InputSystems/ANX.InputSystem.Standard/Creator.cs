using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Standard
{
	public class Creator : IInputSystemCreator
	{
		private IKeyboard keyboard;
		private IMouse mouse;

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
				Logger.Info("Creating a new TouchPanel device.");
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				return InputDeviceFactory.Instance.GetDefaultTouchPanel();
			}
		}

		public IGamePad GamePad
		{
			get
			{
				Logger.Info("Creating a new GamePad device.");
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				return InputDeviceFactory.Instance.GetDefaultGamePad();
			}
		}

		public IMouse Mouse
		{
			get
			{
				if (this.mouse == null)
				{
					this.mouse = InputDeviceFactory.Instance.GetDefaultMouse();
					if (this.mouse == null)
					{
						throw new NoInputDeviceException("couldn't find a default mouse device creator. Unable to create a mouse instance.");
					}
					Logger.Info("created a new Mouse device");
					AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				}

				return this.mouse;
			}
		}

		public IKeyboard Keyboard
		{
			get
			{
				if (this.keyboard == null)
				{
					this.keyboard = InputDeviceFactory.Instance.GetDefaultKeyboard();
					if (this.keyboard == null)
					{
						throw new NoInputDeviceException("couldn't find a default keyboard device creator. Unable to create a keyboard instance.");
					}
					Logger.Info("created a new Keyboard device");
					AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
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
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				return InputDeviceFactory.Instance.GetDefaultMotionSensingDevice();
			}
		}
#endif

		public void RegisterCreator(AddInSystemFactory factory)
		{
			Logger.Info("adding Standard InputSystem creator to creator collection of AddInSystemFactory");
			factory.AddCreator(this);
		}
	}
}
