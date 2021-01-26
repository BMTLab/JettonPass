using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace JettonPass.App.Forms
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class MainForm : Form
    {
        public enum GetWindowLongConst
        {
            NONE = 0,
            GWL_WNDPROC = -4,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_STYLE = -16,
            GWL_EXSTYLE = -20,
            GWL_USERDATA = -21,
            GWL_ID = -12
        }


        /*public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }*/


        private const int WM_SYSCOMMAND = 0x0112; //used for maximizing the screen.
        private const int W_PARAM = 0xf120;      //used for maximizing the screen.
        private const int L_PARAM = 0x5073d;     //used for maximizing the screen.

        private readonly Random _rnd = new();
        
        private int _oldWindowLong;


        public MainForm()
        {
            InitializeComponent();

            MaximizeEverything();
            SetFormTransparent(Handle);
            SetTheLayeredWindowAttribute();

            var backWorker = new BackgroundWorker();
            backWorker.DoWork += DoWork;

            backWorker.RunWorkerAsync();

            throw new ApplicationException(@"Cusom exception");
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BringWindowToTop(IntPtr hWnd);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        
        
        /*
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        */


        /// <summary>
        ///     Make the form (specified by its handle) a window that supports transparency.
        /// </summary>
        /// <param name="handle">The window to make transparency supporting</param>
        public void SetFormTransparent(IntPtr handle)
        {
            _oldWindowLong = GetWindowLong(handle, (int) GetWindowLongConst.GWL_EXSTYLE);
            var hResult = SetWindowLong
            (
                handle,
                (int) GetWindowLongConst.GWL_EXSTYLE,
                Convert.ToInt32(_oldWindowLong | (uint) WindowStyles.WS_EX_LAYERED | (uint) WindowStyles.WS_EX_TRANSPARENT)
            );
        }


        /// <summary>
        ///     Make the form (specified by its handle) a normal type of window (doesn't support transparency).
        /// </summary>
        /// <param name="handle">The Window to make normal</param>
        public void SetFormNormal(IntPtr handle)
        {
            var hResult = SetWindowLong(handle, (int) GetWindowLongConst.GWL_EXSTYLE, Convert.ToInt32(_oldWindowLong | (uint) WindowStyles.WS_EX_LAYERED));
        }


        /// <summary>
        ///     Makes the form change White to Transparent and clickthrough-able
        ///     Can be modified to make the form translucent (with different opacities) and change the Transparency Color.
        /// </summary>
        public void SetTheLayeredWindowAttribute()
        {
            const uint transparentColor = 0xff_ff_ff_ff;

            SetLayeredWindowAttributes(Handle, transparentColor, 125, 0x2);

            TransparencyKey = Color.White;
        }


        /// <summary>
        ///     Finds the Size of all computer screens combined (assumes screens are left to right, not above and below).
        /// </summary>
        /// <returns>The width and height of all screens combined</returns>
        public static Size GetFullScreensSize()
        {
            var height = int.MinValue;
            var width = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                //take largest height
                height = Math.Max(screen.WorkingArea.Height, height);
                width += screen.Bounds.Width;
            }

            return new Size(width, height);
        }


        /// <summary>
        ///     Finds the top left pixel position (with multiple screens this is often not 0,0)
        /// </summary>
        /// <returns>Position of top left pixel</returns>
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


        private void MaximizeEverything()
        {
            Location = GetTopLeft();
            Size = GetFullScreensSize();

            var hResult = SendMessage(Handle, WM_SYSCOMMAND, (UIntPtr) W_PARAM, (IntPtr) L_PARAM);
        }


        private void DoWork(object? sender, DoWorkEventArgs e)
        {
            //var worker = sender as BackgroundWorker;

            const int numRedLines = 500;
            const int numWhiteLines = 1000;

            var fullSize = GetFullScreensSize();
            var topLeft = GetTopLeft();

            using var redPen = new Pen(Color.Red, 10f);
            using var whitePen = new Pen(Color.White, 10f);

            using var formGraphics = CreateGraphics();

            while (true)
            {
                var makeRedLines = true;

                for (var i = 0; i < numRedLines + numWhiteLines; i++)
                {
                    if (i > numRedLines)
                        makeRedLines = false;

                    //Choose points for random lines...but don't draw over the top 100 px of the screen so you can 
                    //still find the stop run button.
                    var pX = _rnd.Next(0, -1 * topLeft.X + fullSize.Width);
                    var pY = _rnd.Next(100, -1 * topLeft.Y + fullSize.Height);

                    var qX = _rnd.Next(0, -1 * topLeft.X + fullSize.Width);
                    var qY = _rnd.Next(100, -1 * topLeft.Y + fullSize.Height);

                    formGraphics.DrawLine(makeRedLines ? redPen : whitePen, pX, pY, qX, qY);
                    
                    Thread.Sleep(10);
                }
            }
        }


        [Flags]
        [SuppressMessage("ReSharper", "CA1069")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = WS_GROUP,
            WS_MAXIMIZEBOX = WS_TABSTOP,

            WS_CAPTION = WS_BORDER | WS_DLGFRAME,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_CHILDWINDOW = WS_CHILD,

            //Extended Window Styles

            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            //#if(WINVER >= 0x0400)
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            //#endif /* WINVER >= 0x0400 */

            //#if(WIN32WINNT >= 0x0500)
            WS_EX_LAYERED = 0x00080000,
            //#endif /* WIN32WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)
            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000,       // Right to left mirroring
            //#endif /* WINVER >= 0x0500 */

            //#if(WIN32WINNT >= 0x0500)
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
            //#endif /* WIN32WINNT >= 0x0500 */
        }
    }
}