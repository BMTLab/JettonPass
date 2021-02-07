using System;
using System.Runtime.InteropServices;


namespace JettonPass.OverlayCore.Common
{
    internal static class NativeMethods
    {
        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex) =>
            IntPtr.Size == 8 ? GetWindowLongPtr64(hWnd, nIndex) : GetWindowLongPtr32(hWnd, nIndex);
        
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong) =>
            IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWindowEx
        (
            uint dwExStyle,
            string lpClassName,
            string lpWindowName,
            uint dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam
        );


        [DllImport("user32.dll")]
        public static extern int DestroyWindow(IntPtr hwnd);


        [DllImport("user32.dll")]
        public static extern int SetWindowPos
        (
            IntPtr hWnd,
            int hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint uFlags
        );


        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();


        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int index);


        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);


        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);


        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);


        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref RawMargin pMargins);
        

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);
        

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct Rect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        public readonly struct Point
        {
            public readonly int X;
            public readonly int Y;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RawMargin
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
    }
}