#region Using Statements
using System;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using SharpDX.XInput;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
    public class GamePad : IGamePad
    {
        #region Private Members
        private Controller[] controller;
        private const float thumbstickRangeFactor = 1.0f / short.MaxValue;

        #endregion // Private Members

        public GamePad()
        {
            controller = new Controller[4];
            controller[0] = new Controller(UserIndex.One);
            controller[1] = new Controller(UserIndex.Two);
            controller[2] = new Controller(UserIndex.Three);
            controller[3] = new Controller(UserIndex.Four);
        }
        public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
        {
            Capabilities result;
            GamePadCapabilities returnres;
            //SharpDX.XInput.Capabilities = new SharpDX.XInput.Capabilities();
            try
            {
                result = controller[(int)playerIndex].GetCapabilities(DeviceQueryType.Gamepad);
                returnres = new GamePadCapabilities();

            }
            catch (Exception)
            {

                returnres = new GamePadCapabilities();
            } return returnres;
        }

        public GamePadState GetState(PlayerIndex playerIndex, out bool isConnected, out int packetNumber)
        {
            State result;
            GamePadState returnres;
            if(controller[(int)playerIndex].IsConnected)
            {
                result = controller[(int)playerIndex].GetState();
                //returnres = new GamePadCapabilities(result.Type,result.Gamepad.Buttons.)
                returnres = new GamePadState(new Vector2(result.Gamepad.LeftThumbX * thumbstickRangeFactor, result.Gamepad.LeftThumbY * thumbstickRangeFactor), new Vector2(result.Gamepad.RightThumbX * thumbstickRangeFactor, result.Gamepad.RightThumbY * thumbstickRangeFactor), (float)result.Gamepad.LeftTrigger, (float)result.Gamepad.RightTrigger, FormatConverter.Translate(result.Gamepad.Buttons));
                packetNumber = result.PacketNumber;
                isConnected = true;
            }
            else
            {
                isConnected = false;
                packetNumber = 0;
                returnres = new GamePadState();
            }


            return returnres;
        }

        public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode, out bool isConnected, out int packetNumber)
        {
            throw new NotImplementedException();
        }

        public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            short left;
            short right;
            if (Math.Abs(leftMotor)>1)
            {
                left = 1;
            }
            else
            {
                left = Convert.ToInt16(Math.Abs(leftMotor) * short.MaxValue);
            }
            if (Math.Abs(rightMotor) > 1)
            {
                right = 1;
            }
            else
            {
                right = Convert.ToInt16(Math.Abs(rightMotor) * short.MaxValue);
            }

            if (controller[(int)playerIndex].IsConnected)
            {
                Vibration vib = new Vibration();
                vib.LeftMotorSpeed = left;
                vib.RightMotorSpeed = right;
                controller[(int)playerIndex].SetVibration(vib);
                return true;
            }
            return false;

        }
    }
}
