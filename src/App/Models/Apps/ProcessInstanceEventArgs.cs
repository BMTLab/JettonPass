using System;


namespace JettonPass.App.Models.Apps
{
    public class ProcessInstanceEventArgs : EventArgs
    {
        #region Ctors
        public ProcessInstanceEventArgs(System.Diagnostics.Process? process)
        {
            Process = process;
        }
        #endregion
        
        
        public System.Diagnostics.Process? Process { get; }
    }
}