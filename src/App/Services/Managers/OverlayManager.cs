using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using JettonPass.App.Models.Apps;
using JettonPass.App.Overlays;
using JettonPass.App.Utils.AppUtils;
using JettonPass.App.Utils.WindowUtils;

using Process.NET;
using Process.NET.Memory;
using Process.NET.Windows.Mouse;


namespace JettonPass.App.Services.Managers
{
    public sealed class OverlayManager : IDisposable
    {
        #region Fields
        private readonly TimeManager _timeManager;
        private readonly AppManager _appManager;
        private ProcessSharp? _processSharp;
        private TimeOverlay? _timeOverlay;
        #endregion _Fields


        #region Ctors
        public OverlayManager(TimeManager timeManager, AppManager appManager)
        {
            _timeManager = timeManager;
            _appManager = appManager;
            
            _appManager.NewInstanceLaunched += AppManagerOnNewInstanceLaunched;
        }
        #endregion


        #region Methods
        public async Task InitializeAsync()
        {
            while (true)
            {
                if (_appManager.RunningProcess is null || ProcessUtils.GetMainWindowProcess(_appManager.RunningProcess.ProcessName) is null)
                    await Task.Delay(1000);
                else
                    break;
            }
            
            _processSharp = new ProcessSharp(ProcessUtils.GetMainWindowProcess(_appManager.RunningProcess.ProcessName), MemoryType.Remote);
            
            _timeOverlay = new TimeOverlay(_timeManager);
            _timeOverlay.Initialize(_processSharp.WindowFactory.MainWindow);
        }
        
        
        private async void AppManagerOnNewInstanceLaunched(object? _, ProcessInstanceEventArgs e)
        {
            if (e.Process is null)
                return;
            
            await InitializeAsync();
        }
        #endregion _Methods


        #region IDisposable
        public void Dispose()
        {
            _appManager.NewInstanceLaunched -= AppManagerOnNewInstanceLaunched;
        }
        #endregion _IDisposable
    }
}