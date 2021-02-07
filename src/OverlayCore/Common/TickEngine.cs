using System;

using JetBrains.Annotations;


namespace JettonPass.OverlayCore.Common
{
    /// <summary>
    ///     Pipeline engine OnPreTick -> OnTick -> OnPostTick with a specific time interval
    /// </summary>
    [PublicAPI]
    public class TickEngine
    {
        /// <summary>
        ///     The wait timer
        /// </summary>
        private readonly WaitTimer _waitTimer = new();

        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        public TimeSpan Interval
        {
            get => _waitTimer.WaitTime;
            set => _waitTimer.WaitTime = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is ticking.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is ticking; otherwise, <c>false</c>.
        /// </value>
        public bool IsTicking { get; set; }
        
        
        /// <summary>
        ///     Gets or sets a value indicating manual control of the Pusle call pause.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsManualLimitation { get; set; }

        /// <summary>
        ///     Occurs when [pre tick].
        /// </summary>
        public event EventHandler PreTick = delegate { };

        /// <summary>
        ///     Occurs when [tick].
        /// </summary>
        public event EventHandler Tick = delegate { };

        /// <summary>
        ///     Occurs when [post tick].
        /// </summary>
        public event EventHandler PostTick = delegate { };


        #region Methods
        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop() =>
            _waitTimer.Stop();


        /// <summary>
        ///     Pulses this instance.
        /// </summary>
        public void Pulse()
        {
            if (!IsTicking)
                return;
            
            if (!IsManualLimitation && !_waitTimer.Update())
                return;
            
            OnPreTick();

            OnTick();

            OnPostTick();
            
            if (!IsManualLimitation)
            {
                _waitTimer.Reset();
            }
        }


        /// <summary>
        ///     Called when [tick].
        /// </summary>
        protected virtual void OnTick() =>
            Tick.Invoke(this, EventArgs.Empty);


        /// <summary>
        ///     Called when [pre tick].
        /// </summary>
        protected virtual void OnPreTick() =>
            PreTick.Invoke(this, EventArgs.Empty);


        /// <summary>
        ///     Called when [post tick].
        /// </summary>
        protected virtual void OnPostTick() =>
            PostTick.Invoke(this, EventArgs.Empty);
        #endregion _Methods
    }
}