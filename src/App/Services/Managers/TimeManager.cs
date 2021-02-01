using System;
using System.Windows.Forms;

using JettonPass.App.Models.Options;
using JettonPass.App.Models.Time;
using JettonPass.App.Services.Receivers.Abstractions;

using Microsoft.Extensions.Options;


namespace JettonPass.App.Services.Managers
{
    public class TimeManager : IDisposable
    {
        #region Consts&Fields
        private const int OneSecond = 1000;
        
        internal readonly TimeSpan DiscreteUnit = TimeSpan.FromMilliseconds(OneSecond);

        private readonly TimeManagerOptions _options;
        private readonly Timer _timer;
        private readonly IJettonReceiver _jettonReceiver;

        private TimeSpan _leftTime;
        private bool _isNotifiedLackTime;
        private bool _isNotifiedEndTime;
        #endregion _Consts&Fields


        #region Ctors
        public TimeManager(IJettonReceiver jettonReceiver, IOptions<TimeManagerOptions> options)
        {
            _options = options.Value;

            _jettonReceiver = jettonReceiver;
            _jettonReceiver.JettonPassed += ReceiverOnPassed;

            LeftTime = TimeSpan.FromMinutes(_options.TimeAfterStartMinutes);
            JettonCost = TimeSpan.FromMinutes(_options.JettonCostMinutes);
            

            _timer = new Timer
            {
                Interval = OneSecond,
                Enabled = _options.SubtractingTimeEnabled
            };

            _timer.Tick += TimerOnTick;
        }
        #endregion


        #region Properties
        public TimeSpan LeftTime
        {
            get => _leftTime;
            protected set
            {
                if (_leftTime == value)
                    return;
                
                _leftTime = value;

                UpdateTime(value);
            }
        }

        public TimeSpan JettonCost { get; }
        public bool NoTime { get; private set; } = true;
        #endregion _Properties


        #region Events
        public event EventHandler<TimeChangedEventArgs> TimeChanged = delegate { };
        public event EventHandler<TimeChangedEventArgs> TimeRunsOut = delegate { };
        public event EventHandler<TimeChangedEventArgs> TimeEnd = delegate { };
        #endregion _Events


        #region Methods
        public void UpdateTime(TimeSpan newTime)
        {
            _leftTime = newTime;
            
            NoTime = LeftTime <= DiscreteUnit;

            if (NoTime)
            {
                _leftTime = TimeSpan.Zero;
            }
            
            var args = new TimeChangedEventArgs(_leftTime);
            TimeChanged.Invoke(this, args);

            
            if (!_isNotifiedEndTime && NoTime)
            {
                _isNotifiedEndTime = true;
                TimeEnd.Invoke(this, args);
            }
            
            if (!_isNotifiedLackTime && _leftTime <= TimeSpan.FromMinutes(_options.TimeRunsOutThresholdMinutes))
            {
                _isNotifiedLackTime = true;
                TimeRunsOut.Invoke(this, args);
            }

            if (_isNotifiedLackTime && _leftTime > TimeSpan.FromMinutes(_options.TimeRunsOutThresholdMinutes))
            {
                _isNotifiedLackTime = false;
            }
            
            if (_isNotifiedEndTime && !NoTime)
            {
                _isNotifiedEndTime = false;
            }
        }
        
        private void OnLeftTimeChanged(TimeSpan newTime)
        {
            UpdateTime(_leftTime - newTime);
        }
        
        
        private void ReceiverOnPassed(object? sender, EventArgs _)
        {
            UpdateTime(_leftTime + JettonCost);
        }
        
        
        private void TimerOnTick(object? sender, EventArgs _)
        {
            if (LeftTime >= DiscreteUnit)
                UpdateTime(_leftTime - DiscreteUnit);
        }
        #endregion _Methods
        
        
        #region IDisposable
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _jettonReceiver.JettonPassed -= ReceiverOnPassed;
                _timer.Tick -= TimerOnTick;
                
                _timer.Stop();
                _timer.Dispose();
            }

            _isDisposed = true;
        }


        ~TimeManager()
        {
            Dispose(false);
        }
        #endregion
    }
}