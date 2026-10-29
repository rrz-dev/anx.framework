using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
    public class Creator : IInputSystemCreator
    {
        //It not a good idea to have more than one RecordingDevice per Input Device, so we cache the request.
        RecordingMouse mouse;
        RecordingKeyboard keyboard;
        RecordingGamePad gamePad;
#if XNAEXT
        RecordingMotionSensingDevice msd;
#endif

		public ITouchPanel TouchPanel
		{
			get
			{
				AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
				throw new NotImplementedException();
			}
		}

        public IGamePad GamePad
        {
            get
            {
                AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
                if (gamePad == null)
                    gamePad = new RecordingGamePad();
                return gamePad;
            }
        }

        public IMouse Mouse
        {
            get
            {
                AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
                if (mouse == null)
                    mouse = new RecordingMouse();
                return mouse;
            }
        }

        public IKeyboard Keyboard
        {
            get
            {
                AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
                if (keyboard == null)
                    keyboard = new RecordingKeyboard();
                return keyboard;
            }
        }

#if XNAEXT
        public IMotionSensingDevice MotionSensingDevice
        {
            get
            {
                AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
                if (msd == null)
                    msd = new RecordingMotionSensingDevice();
                return msd;
            }
        }
#endif

		public string Name
		{
			get
			{
				return "Recording";
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
				// This is just a proxy, so it runs on all plattforms
				return true;
			}
		}
    }
}
