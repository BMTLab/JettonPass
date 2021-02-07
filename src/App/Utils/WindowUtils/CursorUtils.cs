using JettonPass.App.Utils.AppUtils;


namespace JettonPass.App.Utils.WindowUtils
{
    public static class CursorUtils
    {
        public static bool MouseCursorIsHidden { get; set; }
        
        public static async void ToggleMouseCursorVisibility()
        {
            var process = ProcessUtils.Start("Resources\\HideCursor.exe");
            

            /*if (MouseCursorIsHidden)
            {
                NativeMethods.SetSystemCursor(_hCursorOriginal, OcrSystemCursorType.OcrNormal);
                NativeMethods.DestroyIcon(_hCursorOriginal);
                _hCursorOriginal = IntPtr.Zero;
 
                MouseCursorIsHidden = false;
            }
            else
            {
               var blankPtr = NativeMethods.LoadCursorFromFile(Path.Combine(Directory.GetCurrentDirectory(), "blank.cur"));
               NativeMethods.SetSystemCursor(blankPtr, OcrSystemCursorType.OcrNormal);
            }*/
        }
    }
    
}