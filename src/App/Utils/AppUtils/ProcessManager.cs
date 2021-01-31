using System;
using System.Diagnostics;


namespace JettonPass.App.Utils.AppUtils
{
    public static class ProcessManager
    {
        #region Methods
        public static Process? Start(string fileName)
        {
            try
            {
                return Process.Start
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


        public static void Shutdown(Process? runningProcess)
        {
            try
            {
                if (runningProcess is null)
                    return;

                var isSafeClosed = runningProcess.CloseMainWindow();

                if (!isSafeClosed)
                {
                    using var killProcess = Process.Start
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

                using var killProcess = Process.Start
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