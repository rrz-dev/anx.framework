using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINDOWSMETRO
using Windows.UI.Core;
#endif

namespace ANX.Framework.NonXNA
{
    public struct WindowHandle : IEquatable<WindowHandle>
    {
#if WINDOWSMETRO
        CoreWindow window;
#else
        IntPtr handle;
#endif

#if WINDOWSMETRO
        public WindowHandle(CoreWindow window)
        {
            this.window = window;
        }
#else
        public WindowHandle(IntPtr ptr)
        {
            this.handle = ptr;
        }
#endif

#if WINDOWSMETRO
        public CoreWindow Window
        {
            get { return this.window; }
        }
#else
        public IntPtr Handle
        {
            get { return this.handle; }
        }
#endif

        public bool IsValid
        {
            get
            {
#if WINDOWSMETRO
                return window != null;
#else
                return handle != IntPtr.Zero;
#endif
            }
        }

#if WINDOWSMETRO
        public static implicit operator CoreWindow(WindowHandle handle)
        {
            return handle.window;
        }
#else
        public static implicit operator IntPtr(WindowHandle handle)
        {
            return handle.Handle;
        }
#endif

/*
#if WINDOWSMETRO
        public static implicit operator WindowHandle(CoreWindow handle)
        {
            return new WindowHandle(handle);
        }
#else
        public static implicit operator WindowHandle(IntPtr handle)
        {
            return new WindowHandle(handle);
        }
#endif
*/

        public bool Equals(WindowHandle other)
        {
#if WINDOWSMETRO
            return this.Window == other.Window;
#else
            return this.Handle == other.Handle;
#endif
        }

        public override bool Equals(object obj)
        {
            if (obj is WindowHandle)
                return this.Equals((WindowHandle)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(WindowHandle left, WindowHandle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowHandle left, WindowHandle right)
        {
            return !(left == right);
        }
    }
}
