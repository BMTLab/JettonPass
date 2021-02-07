using System;
using System.Windows;
using System.Windows.Interop;


namespace JettonPass.OverlayCore.Common
{
    public static class WindowExtensions
    {
        private const int GwlExstyle = -20;
        private const int WsExTransparent = 0x00000020;


        public static void MakeWindowTransparent(this Window wnd)
        {
            if (!wnd.IsInitialized)
                throw new Exception("The extension method MakeWindowTransparent can not be called prior to the window being initialized.");

            var hwnd = new WindowInteropHelper(wnd).Handle;
            var extendedStyle = NativeMethods.GetWindowLongPtr(hwnd, GwlExstyle);
            NativeMethods.SetWindowLongPtr(hwnd, GwlExstyle, new IntPtr(extendedStyle.ToInt32() | WsExTransparent));
        }
    }
}