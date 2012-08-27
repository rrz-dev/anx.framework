using ANX.Framework.NonXNA;
// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InpuDevices.Windows.ModernUI
{
    public class Creator : IInputSystemCreator
    {
        private IKeyboard keyboard;
        private IMouse mouse;
        private IGamePad gamePad;
        private ITouchPanel touchPanel;

        public IGamePad GamePad
        {
            get { throw new System.NotImplementedException(); }
        }

        public IMouse Mouse
        {
            get { throw new System.NotImplementedException(); }
        }

        public IKeyboard Keyboard
        {
            get { throw new System.NotImplementedException(); }
        }

        public ITouchPanel TouchPanel
        {
            get { throw new System.NotImplementedException(); }
        }

        public IMotionSensingDevice MotionSensingDevice
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public int Priority
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsSupported
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
