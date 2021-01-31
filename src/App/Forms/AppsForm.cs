using System;
using System.IO;
using System.Windows.Forms;

using JettonPass.App.Controls;
using JettonPass.App.Services.Managers;


namespace JettonPass.App.Forms
{
    public sealed partial class AppsForm : Form
    {
        #region Fields
        private readonly TimeManager _timeManager;
        #endregion


        #region Ctors
        public AppsForm(TimeManager timeManager, AppManager appManager)
        {
            InitializeComponent();
            
            _timeManager = timeManager;
            _timeManager.TimeEnd += ManagerOnTimeEnd;
            _timeManager.TimeChanged += ManagerOnTimeChanged;
            
            var apps = appManager.LoadApps();
            
            foreach (var app in apps)
            {
                appsContainer?.Controls.Add(new AppControl(app));
            }
        }
        #endregion _Ctors


        #region Methods
        private void ManagerOnTimeEnd(object? sender, EventArgs _)
        {
            foreach (AppControl control in appsContainer.Controls)
            {
                control.IsBlocked = true;
            }
        }
        
        
        private void ManagerOnTimeChanged(object? sender, EventArgs _)
        {
            if (_timeManager.NoTime)
                return;
            
            foreach (AppControl control in appsContainer.Controls)
            {
                control.IsBlocked = false;
            }
        }
        #endregion _Methods
    }
}