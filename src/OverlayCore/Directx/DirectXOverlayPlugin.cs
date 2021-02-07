using System;


namespace JettonPass.OverlayCore.Directx
{
    /// <summary>
    /// </summary>
    /// <seealso cref="OverlayPlugin" />
    public abstract class DirectXOverlayPlugin : OverlayPlugin
    {
        private DirectXOverlayWindow? _overlayWindow;

        /// <summary>
        ///     Gets or sets the overlay window.
        /// </summary>
        /// <value>
        ///     The overlay window.
        /// </value>
        public DirectXOverlayWindow OverlayWindow
        {
            get
            {
                if (_overlayWindow is not null)
                    return _overlayWindow;

                throw new NullReferenceException($"{nameof(OverlayWindow)} must be initialized in the Initialize method");
            }
            protected set => _overlayWindow = value;
        }
    }
}