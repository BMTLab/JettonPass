/*using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;


namespace JettonPass.App.Utils.IconUtils
{
    public static class IconInfo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Icon GetIcon(string fileName)
        {
            var sb = new StringBuilder(fileName, fileName.Length);
            var handle = NativeMethods.ExtractAssociatedIcon(IntPtr.Zero, sb, out var _);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }
    }
}*/