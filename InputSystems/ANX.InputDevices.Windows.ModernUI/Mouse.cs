using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using System;
using Windows.Devices.Input;
using Windows.UI.Core;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.ModernUI
{
    [PercentageComplete(80)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("rene87")]
    class Mouse : IMouse
    {
        private int _wheel;
        private int _x;
        private int _y;
        private ButtonState _left;
        private ButtonState _right;
        private ButtonState _middle;
        private ButtonState _xButton1;
        private ButtonState _xButton2;

        public IntPtr WindowHandle
        {
            get;
            set;
        }

        public Mouse()
        {
            CoreWindow.GetForCurrentThread().PointerPressed += Mouse_PointerPressed;
            CoreWindow.GetForCurrentThread().PointerReleased += Mouse_PointerReleased;
            CoreWindow.GetForCurrentThread().PointerWheelChanged += Mouse_PointerWheelChanged;
            this._wheel = 0;
            this._x = 0;
            this._y = 0;
            this._left = ButtonState.Released;
            this._right = ButtonState.Released;
            this._middle = ButtonState.Released;
            this._xButton1 = ButtonState.Released;
            this._xButton2 = ButtonState.Released;
        }

        void Mouse_PointerWheelChanged(CoreWindow sender, PointerEventArgs args)
        {
            this._wheel += args.CurrentPoint.Properties.MouseWheelDelta;
        }

        void Mouse_PointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            if (args.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Mouse)
            {
                if (!args.CurrentPoint.Properties.IsLeftButtonPressed)
                {
                    this._left = ButtonState.Released;
                }

                if (!args.CurrentPoint.Properties.IsMiddleButtonPressed)
                {
                    this._middle = ButtonState.Released;
                }

                if (!args.CurrentPoint.Properties.IsRightButtonPressed)
                {
                    this._right = ButtonState.Released;
                }

                if (!args.CurrentPoint.Properties.IsXButton1Pressed)
                {
                    this._xButton1 = ButtonState.Released;
                }

                if (!args.CurrentPoint.Properties.IsXButton2Pressed)
                {
                    this._xButton2 = ButtonState.Released;
                }
            }
            else
            {
                this._left = ButtonState.Released;
            }
        }

        void Mouse_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (args.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Mouse)
            {
                if (args.CurrentPoint.Properties.IsLeftButtonPressed)
                {
                    this._left = ButtonState.Pressed;
                }

                if (args.CurrentPoint.Properties.IsMiddleButtonPressed)
                {
                    this._middle = ButtonState.Pressed;
                }

                if (args.CurrentPoint.Properties.IsRightButtonPressed)
                {
                    this._right = ButtonState.Pressed;
                }

                if (args.CurrentPoint.Properties.IsXButton1Pressed)
                {
                    this._xButton1 = ButtonState.Pressed;
                }

                if (args.CurrentPoint.Properties.IsXButton2Pressed)
                {
                    this._xButton2 = ButtonState.Pressed;
                }

                this._x = (int)args.CurrentPoint.Position.X;
                this._y = (int)args.CurrentPoint.Position.Y;
            }
            else
            {
                this._left = ButtonState.Pressed;
                this._x = (int)args.CurrentPoint.Position.X;
                this._y = (int)args.CurrentPoint.Position.Y;
            }
        }

        void Mouse_MouseMoved(MouseDevice sender, MouseEventArgs args)
        {
            this._x = args.MouseDelta.X;
            this._y = args.MouseDelta.Y;
        }

        public MouseState GetState()
        {
            return new MouseState(this._x, this._y, this._wheel, this._left, this._middle, this._right, this._xButton1, this._xButton2);
        }

        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
