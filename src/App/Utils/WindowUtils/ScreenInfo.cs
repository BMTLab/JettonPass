using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using JettonPass.App.Models.WinApiEnums;


namespace JettonPass.App.Utils.WindowUtils
{
    public static class ScreenInfo
    {
        /// <summary>
        ///     Finds the Size of all computer screens combined (assumes screens are left to right, not above and below).
        /// </summary>
        /// <returns>The width and height of all screens combined</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Size GetFullScreensSize()
        {
            var height = int.MinValue;
            var width = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                // Take largest height
                height = Math.Max(screen.WorkingArea.Height, height);
                width += screen.Bounds.Width;
            }

            return new Size(width, height);
        }


        /// <summary>
        ///     Finds the top left pixel position (with multiple screens this is often not 0,0)
        /// </summary>
        /// <returns>Position of top left pixel</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Point GetTopLeft()
        {
            var minX = int.MaxValue;
            var minY = int.MaxValue;

            foreach (Screen screen in Screen.AllScreens)
            {
                minX = Math.Min(screen.WorkingArea.Left, minX);
                minY = Math.Min(screen.WorkingArea.Top, minY);
            }

            return new Point(minX, minY);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void TopOnOwner(IntPtr ownedHandle, IntPtr ownerHandle)
        {
            var hResult = NativeMethods.SetWindowLong(ownedHandle, (int) GetWindowLongConst.GWL_HWNDPARENT, (int) ownerHandle);
        }
    }
}