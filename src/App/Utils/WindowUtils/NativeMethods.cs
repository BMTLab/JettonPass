using System;
using System.Runtime.InteropServices;

using JettonPass.App.Models.WinApiEnums;


namespace JettonPass.App.Utils.WindowUtils
{
    internal static class NativeMethods
    {
        /// <summary>
        ///     This static method is required because legacy OSes do not support SetWindowLongPtr 
        /// </summary>
        public static WindowStyles SetWindowLong(IntPtr hWnd, GetWindowLongConst nIndex, WindowStyles dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLong64(hWnd, nIndex, dwNewLong)
                : SetWindowLong32(hWnd, nIndex, dwNewLong);
        }
        
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetCursor();
        
        
        [DllImport("user32.dll")]
        public static extern bool SetSystemCursor(IntPtr hcur, OcrSystemCursorType id);
        
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);
        
        
        [DllImport("User32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern IntPtr LoadCursorFromFile(string str);
        
        
        
        
        
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        internal static extern WindowStyles SetWindowLong32(IntPtr hWnd, GetWindowLongConst nIndex, WindowStyles dwNewLong);

        
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        internal static extern WindowStyles SetWindowLong64(IntPtr hWnd, GetWindowLongConst nIndex, WindowStyles dwNewLong);
        
        
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BringWindowToTop(IntPtr hWnd);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        
        
        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        

        [DllImport("user32.dll")]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
        
        
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}