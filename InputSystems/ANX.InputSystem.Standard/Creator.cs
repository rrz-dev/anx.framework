using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Standard
{
    public class Creator : IInputSystemCreator
    {
        private const string CreationLogMessage = "Creating a new {0} device.";
        private const string CreationThrowMessage =
            "Couldn't find a default {0} device creator. Unable to create a {0} instance.";

#if XNAEXT
        private IMotionSensingDevice motionSensingDevice;
#endif
        private IKeyboard keyboard;
        private IMouse mouse;
        private IGamePad gamePad;
        private ITouchPanel touchPanel;

        public string Name
        {
            get { return "Standard"; }
        }

        public int Priority
        {
            get { return 0; }
        }

        public bool IsSupported
        {
            get { return true; }
        }

        public ITouchPanel TouchPanel
        {
            get
            {
                if (touchPanel == null)
                {
                    Logger.Info(string.Format(CreationLogMessage, "TouchPanel"));
                    PreventSystemChange();
                    touchPanel = InputDeviceFactory.Instance.GetDefaultTouchPanel();
                    if (touchPanel == null)
                        throw new NoInputDeviceException(string.Format(CreationThrowMessage, "touchPanel"));
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
                    Logger.Info(string.Format(CreationLogMessage, "GamePad"));
                    PreventSystemChange();
                    gamePad = InputDeviceFactory.Instance.CreateDefaultGamePad();
                    if (gamePad == null)
                        throw new NoInputDeviceException(string.Format(CreationThrowMessage, "gamePad"));
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
                    Logger.Info(string.Format(CreationLogMessage, "Mouse"));
                    PreventSystemChange();
                    mouse = InputDeviceFactory.Instance.CreateDefaultMouse();
                    if (mouse == null)
                        throw new NoInputDeviceException(string.Format(CreationThrowMessage, "mouse"));
                }

                return mouse;
            }
        }

        public IKeyboard Keyboard
        {
            get
            {
                if (keyboard == null)
                {
                    Logger.Info(string.Format(CreationLogMessage, "Keyboard"));
                    PreventSystemChange();
                    keyboard = InputDeviceFactory.Instance.CreateDefaultKeyboard();
                    if (keyboard == null)
                        throw new NoInputDeviceException(string.Format(CreationThrowMessage, "keyboard"));
                }

                return keyboard;
            }
        }

#if XNAEXT
        public IMotionSensingDevice MotionSensingDevice
        {
            get
            {
                if (motionSensingDevice == null)
                {
                    Logger.Info(string.Format(CreationLogMessage, "MotionSensing"));
                    PreventSystemChange();
                    motionSensingDevice = InputDeviceFactory.Instance.CreateDefaultMotionSensingDevice();
                    if (motionSensingDevice == null)
                        throw new NoInputDeviceException(string.Format(CreationThrowMessage, "motionSensing"));
                }

                return motionSensingDevice;
            }
        }
#endif

        private static void PreventSystemChange()
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
        }
    }
}
