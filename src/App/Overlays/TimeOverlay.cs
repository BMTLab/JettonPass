using System;

using JettonPass.App.Models.Time;
using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Services.Managers;
using JettonPass.OverlayCore.Directx;

using Process.NET.Windows;


namespace JettonPass.App.Overlays
{
    public sealed class TimeOverlay : BaseDirectXOverlay
    {
        #region Fields
        private readonly TimeManager _timeManager;
        
        private Action<Direct2DRenderer> _timeChangeAction = delegate {};
        private string? _currentText;
        private int _currentBrush;
        private int _currentFont;

        private bool _isDisposed;
        private bool _isInitialized;
        #endregion _Fields


        #region Ctors
        public TimeOverlay(TimeManager timeManager)
        {
            _timeManager = timeManager;
            _timeManager.TimeChanged += TimeManagerOnTimeChanged;
            _timeManager.StateChanged += TimeManagerOnStateChanged;
            
            UpdateTextRender();
        }

        
        #region Overrides of BaseDirectXOverlay
        public override void Initialize(IWindow targetWindow)
        {
            if (_isInitialized)
                return;
            
            _isInitialized = true;
            
            base.Initialize(targetWindow);
            Enable();
        }
        
        
        protected override void OnDraw(Direct2DRenderer renderer)
        {
            _timeChangeAction.Invoke(renderer);
        }
        #endregion


        private void TimeManagerOnTimeChanged(object? _, TimeChangedEventArgs e)
        {
            Update();
            
            UpdateTextRender();
        }
        
        private void TimeManagerOnStateChanged(object? _, TimeChangedEventArgs e)
        {
            UpdateTextRender();
        }
        #endregion


        #region Methods
        private void UpdateTextRender()
        {
            _currentBrush = _timeManager.State.Type == TimeStateType.NormalTimeState ? YellowBrush : RedBrush;
            _currentFont = _timeManager.State.Type == TimeStateType.NormalTimeState ? Font : HugeFont;
            _currentText = _timeManager.State.Type != TimeStateType.EndTimeState ? _timeManager.LeftTime.ToString() : " Время вышло \r\n Киньте жетон!";
            _timeChangeAction = renderer => renderer.DrawText(_currentText, _currentFont, _currentBrush, 350, 52, false);
        }
        #endregion _Methods


        #region IDisposable
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;
            
            if (isDisposing)
            {
                _timeManager.TimeChanged -= TimeManagerOnTimeChanged;
                _timeManager.StateChanged -= TimeManagerOnStateChanged;
                _timeChangeAction = delegate { };
                _currentText = null;
            }
            
            base.Dispose(isDisposing);
            _isDisposed = true;
            _isInitialized = false;
        }
        #endregion _IDisposable
    }
}