using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Input
{
    public static class Mouse
    {
        public static IntPtr WindowHandle { get; set; }
        public static MouseState GetState() 
        {
            throw new NotImplementedException();
        }
        public static void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
