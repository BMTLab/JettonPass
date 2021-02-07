using System;

using JettonPass.OverlayCore.Common;
using JettonPass.OverlayCore.Directx;

using Process.NET.Windows;


namespace JettonPass.App.Overlays
{
    public class BaseDirectXOverlay : DirectXOverlayPlugin
    {
        #region Ctors
        public BaseDirectXOverlay() 
        {
            _tickEngine = new TickEngine();
        } 
        #endregion


        #region Fields
        private readonly TickEngine _tickEngine;
        
        protected int Font;
        protected int HugeFont;
        protected int RedBrush;
        protected int YellowBrush;
        
        private bool _isDisposed;
        #endregion _Fields


        #region Methods
        public override void Initialize(IWindow targetWindow)
        {
            // Set target window by calling the base method
            base.Initialize(targetWindow);

            OverlayWindow = new DirectXOverlayWindow(targetWindow.Handle);
            
            YellowBrush = OverlayWindow.Graphics.CreateBrush(0x7FFFFF00);
            RedBrush = OverlayWindow.Graphics.CreateBrush(0x7FFF0080);

            Font = OverlayWindow.Graphics.CreateFont("Segoe UI", 20);
            HugeFont = OverlayWindow.Graphics.CreateFont("Segoe UI", 50, true);
            
            // Set up update interval and register events for the tick engine.
            //_tickEngine.Interval = TimeSpan.FromMilliseconds(1000) / (Fps * 0.5);
            _tickEngine.PreTick += OnPreTick;
            _tickEngine.Tick += OnTick;
        }


        public override void Enable()
        {
            _tickEngine.IsTicking = true;
            base.Enable();
        }


        public override void Disable()
        {
            _tickEngine.IsTicking = false;
            base.Disable();
        }


        public override void Update() =>
            _tickEngine.Pulse();


        protected void InternalRender()
        {
            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();
            OnDraw(OverlayWindow.Graphics);
            OverlayWindow.Graphics.EndScene();
        }


        protected virtual void OnDraw(Direct2DRenderer render)
        {
        }


        private void OnTick(object? _, EventArgs e)
        {
            // This will only be true if the target window is active
            // (or very recently has been, depends on your update rate)
            if (!OverlayWindow.IsVisible)
                return;

            OverlayWindow.Update();
            InternalRender();
        }


        private void OnPreTick(object? _, EventArgs e)
        {
            var targetWindowIsActivated = TargetWindow.IsActivated;

            switch (targetWindowIsActivated)
            {
                case false when OverlayWindow.IsVisible:
                    ClearScreen();
                    OverlayWindow.Hide();

                    break;
                case true when !OverlayWindow.IsVisible:
                    OverlayWindow.Show();

                    break;
            }
        }


        private void ClearScreen()
        {
            if (OverlayWindow == null!)
                return;
            
            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();
            OverlayWindow.Graphics.EndScene();
        }


        #region IDisposable
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                _tickEngine.PreTick -= OnPreTick;
                _tickEngine.Tick -= OnTick;
                
                ClearScreen();
                OverlayWindow?.Dispose();
            }

            base.Dispose();
            _isDisposed = true;
        }
        #endregion _IDisposable
        #endregion _Methods
    }
}