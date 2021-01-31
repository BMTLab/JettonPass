﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

using JettonPass.App.Models.Apps;
using JettonPass.App.Models.WinApiEnums;
using JettonPass.App.Utils.WindowUtils;


namespace JettonPass.App.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public sealed partial class AppControl : UserControl
    {
        #region Fields
        private readonly AppEntity _app;
        #endregion _Fields


        #region Ctors
        public AppControl(AppEntity app)
        {
            _app = app;
            
            InitializeComponent();

            appName.Text = app.Name;
            appIcon.BackgroundImage = app.Icon.ToBitmap();
        }
        #endregion _Ctors


        #region Properties
        public bool IsBlocked { get; set; }
        #endregion _Properties


        private void OnClick(object? sender, EventArgs _)
        {
            if (IsBlocked)
                return;

            var process = _app.Start();

            if (ParentForm is not null)
                ParentForm.WindowState = FormWindowState.Maximized;

            if (process is null)
                return;
            
            NativeMethods.ShowWindow(process.MainWindowHandle, (int) ShowWindow.SW_SHOWMAXIMIZED);
        }
    }
}