#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.XInput;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace Xinput
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller[] controller = new Controller[5];
            controller[0] = new Controller(UserIndex.One);
            controller[1] = new Controller(UserIndex.Two);
            controller[2] = new Controller(UserIndex.Three);
            controller[3] = new Controller(UserIndex.Four);
            controller[4] = new Controller(UserIndex.Any);
            
            foreach (Controller item in controller)
            {
                Console.WriteLine(item.IsConnected);
                if (item.IsConnected)
                {
                    Console.WriteLine(item.GetState().Gamepad.ToString());
                }
            }
            
            if (controller[0].IsConnected)
            {
                var test = controller[0].GetCapabilities(DeviceQueryType.Gamepad);
                for (int i = 0; i < 16; i++)
                {
                    Console.WriteLine("{0,3} - {1}", i, ((GamepadButtonFlags)i).ToString());
                }
            }

            Console.Read();
        }
    }
}
