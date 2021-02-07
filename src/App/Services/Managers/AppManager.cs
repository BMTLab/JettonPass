using System;
using System.Linq;

using JettonPass.App.Models.Apps;
using JettonPass.App.Models.Options;
using JettonPass.App.Models.Time;
using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Utils.AppUtils;

using Microsoft.Extensions.Options;


namespace JettonPass.App.Services.Managers
{
    public sealed class AppManager : IDisposable
    {
        #region Fields
        private readonly TimeManager _timeManager;
        private readonly string _appPath;
        #endregion _Fields


        #region Ctors
        public AppManager(TimeManager timeManager, IOptions<AppManagerOptions> options)
        {
            _appPath = options.Value.CustomPaths!.First();
            
            _timeManager = timeManager;
            _timeManager.StateChanging += TimeManagerOnStateChanging;
        }
        #endregion


        #region Properties
        public System.Diagnostics.Process? RunningProcess { get; private set; }
        #endregion _Properties


        #region Events
        public event EventHandler<ProcessInstanceEventArgs> NewInstanceLaunched = delegate { };
        #endregion _Events


        #region Methods
        public void RunApp()
        {
            RunningProcess = ProcessUtils.Start(_appPath);
            OnNewInstanceLaunched();
        }
        
        
        public void CloseApp()
        {
            ProcessUtils.Shutdown(RunningProcess);
            GC.Collect();
        }
        
        private void OnNewInstanceLaunched()
        {
            if (RunningProcess is not null) 
                NewInstanceLaunched.Invoke(this, new ProcessInstanceEventArgs(RunningProcess));
        }
        
        
        private void TimeManagerOnStateChanging(object? _, TimeChangedEventArgs e)
        {
            switch (e.NewState.Type)
            {
                case TimeStateType.EndTimeState when _timeManager.State.Type != TimeStateType.EndTimeState: CloseApp(); break;
                case var _ when _timeManager.State.Type == TimeStateType.EndTimeState: RunApp(); break;
            }
        }
        #endregion _Methods


        #region IDisposable
        public void Dispose()
        {
            _timeManager.StateChanging -= TimeManagerOnStateChanging;
        }
        #endregion _IDisposable
    }
}