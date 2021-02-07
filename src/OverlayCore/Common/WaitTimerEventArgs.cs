using System;


namespace JettonPass.OverlayCore.Common
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public sealed class WaitTimerEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets the time finished.
        /// </summary>
        /// <value>
        ///     The time finished.
        /// </value>
        public DateTime TimeFinished { get; set; }

        /// <summary>
        ///     Gets or sets the wait time.
        /// </summary>
        /// <value>
        ///     The wait time.
        /// </value>
        public TimeSpan WaitTime { get; set; }

        /// <summary>
        ///     Gets or sets the time started.
        /// </summary>
        /// <value>
        ///     The time started.
        /// </value>
        public DateTime TimeStarted { get; set; }
    }
}