using System;
using System.Runtime.InteropServices;

namespace NAS4FreeConsole
{
    public class Win32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int W, int H, uint uFlags);

        public static class HWND
        {
            public static readonly IntPtr NOTOPMOST = new IntPtr(-2);
            public static readonly IntPtr BROADCAST = new IntPtr(0xffff);
            public static readonly IntPtr TOPMOST = new IntPtr(-1);
            public static readonly IntPtr TOP = new IntPtr(0);
            public static readonly IntPtr BOTTOM = new IntPtr(1);
        }

        public static class SWP
        {
            public static readonly uint NOSIZE = 0x0001;
            public static readonly uint NOMOVE = 0x0002;
            public static readonly uint NOZORDER = 0x0004;
            public static readonly uint NOREDRAW = 0x0008;
            public static readonly uint NOACTIVATE = 0x0010;
            public static readonly uint DRAWFRAME = 0x0020;
            public static readonly uint FRAMECHANGED = 0x0020;
            public static readonly uint SHOWWINDOW = 0x0040;
            public static readonly uint HIDEWINDOW = 0x0080;
            public static readonly uint NOCOPYBITS = 0x0100;
            public static readonly uint NOOWNERZORDER = 0x0200;
            public static readonly uint NOREPOSITION = 0x0200;
            public static readonly uint NOSENDCHANGING = 0x0400;
            public static readonly uint DEFERERASE = 0x2000;
            public static readonly uint ASYNCWINDOWPOS = 0x4000;
        }
    }
}
