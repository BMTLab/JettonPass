using System.Diagnostics;

using System.Drawing;

using JettonPass.App.Utils.AppUtils;


namespace JettonPass.App.Models.Apps
{
    public sealed class AppEntity
    {
        #region Fields
        private Process? _runningProcess;
        #endregion _Fields


        #region Ctors
        public AppEntity(string name, string path, Icon icon)
        {
            Name = name;
            Path = path;
            Icon = icon;
        }
        #endregion


        #region Properties
        public string Name { get; }
        public string Path { get; }
        public Icon Icon { get; }
        #endregion _Properties


        #region Methods
        public Process? Start()
        {
           return _runningProcess = ProcessManager.Start(Path);
        }


        public void Shutdown()
        {
            try
            {
                if (_runningProcess is null)
                    return;

                ProcessManager.Shutdown(_runningProcess);
            }
            finally
            {
                _runningProcess?.Dispose();
                _runningProcess = null;
            }
        }
        #endregion _Methods
    }
}