/*using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace JettonPass.Launcher
{
    public static class ProcessManager
    {
        #region Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool IsRunning(Process? process)
        {
            if (process is null)
                return false;
            
            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                return false;
            }
            
            return true;
        }
        #endregion _Methods
    }
}*/