using System;

using Process.NET.Windows;


namespace JettonPass.OverlayCore
{
    /// <summary>
    ///     Abstract class that defines basic overlay operations and values.
    /// </summary>
    /// <seealso>
    ///     <cref>PluginBase</cref>
    /// </seealso>
    /// <seealso cref="System.IDisposable" />
    public abstract class OverlayPlugin : IDisposable
    {
        private bool _isDisposed;
        private IWindow? _targetWindow;

        /// <summary>
        ///     Gets or sets the target window that the overlay is to 'attach' to.
        /// </summary>
        /// <value>
        ///     The target window.
        /// </value>
        public IWindow TargetWindow
        {
            get
            {
                if (_targetWindow is not null)
                    return _targetWindow;

                throw new NullReferenceException($"{nameof(TargetWindow)} must be initialized");
            }
            protected set => _targetWindow = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; protected set; }
        

        /// <summary>
        ///     Enables this instance.
        /// </summary>
        public virtual void Enable() =>
            IsEnabled = true;


        /// <summary>
        ///     Disables this instance.
        /// </summary>
        public virtual void Disable() =>
            IsEnabled = false;


        /// <summary>
        ///     Initializes the specified target window.
        /// </summary>
        /// <param name="targetWindow">The target window.</param>
        public virtual void Initialize(IWindow targetWindow) =>
            TargetWindow = targetWindow;


        /// <summary>
        ///     Updates this instance.
        /// </summary>
        public virtual void Update()
        {
        }


        #region IDisposable
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;
            
            if (IsEnabled)
                Disable();
            
            _isDisposed = true;
        }
        
        
        ~OverlayPlugin()
        {
            Dispose(false);
        }
        #endregion _IDisposable
    }
}