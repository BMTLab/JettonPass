using System;
using System.Diagnostics;
using System.Linq;


namespace JettonPass.App.Utils.AppUtils
{
    public static class ProcessUtils
    {
        #region Methods
        public static System.Diagnostics.Process? GetMainWindowProcess(string processName)
        {
            var processes = System.Diagnostics.Process.GetProcessesByName(processName);

            return processes.FirstOrDefault(process => process.MainWindowHandle != IntPtr.Zero);
        }
        
        
        public static System.Diagnostics.Process? Start(string fileName)
        {
            try
            {
                return System.Diagnostics.Process.Start
                (
                    new ProcessStartInfo
                    {
                        FileName = fileName,
                        ErrorDialog = false,
                        WindowStyle = ProcessWindowStyle.Maximized,
                        CreateNoWindow = true
                    }
                );
            }
            finally
            {
                GC.Collect();
            }
        }


        public static void Shutdown(System.Diagnostics.Process? runningProcess)
        {
            try
            {
                if (runningProcess is null)
                    return;

                var isSafeClosed = runningProcess.CloseMainWindow();

                if (!isSafeClosed)
                {
                    using var killProcess = System.Diagnostics.Process.Start
                    (
                        new ProcessStartInfo
                        {
                            FileName = @"taskkill",
                            Arguments = @$"/PID {runningProcess.Id.ToString()} /T",
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        }
                    );
                }
            }
            finally
            {
                runningProcess?.Dispose();
                GC.Collect();
            }
        }
        
        
        public static void Shutdown(string nameProcess)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameProcess))
                    return;

                using var killProcess = System.Diagnostics.Process.Start
                (
                    new ProcessStartInfo
                    {
                        FileName = @"taskkill",
                        Arguments = @$"/F /IM {nameProcess}",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                );
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion _Methods
    }
}