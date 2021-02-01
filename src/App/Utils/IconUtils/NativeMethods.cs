using System;
using System.Runtime.InteropServices;
using System.Text;


namespace JettonPass.App.Utils.IconUtils
{
    internal static class NativeMethods
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);
    }
}