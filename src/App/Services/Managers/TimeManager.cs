using System;
using System.Timers;

using JettonPass.App.Models.Options;
using JettonPass.App.Models.Time;
using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Services.Receivers.Abstractions;

using Microsoft.Extensions.Options;


namespace JettonPass.App.Services.Managers
{
    public class TimeManager : IDisposable
    {
        #region Consts&Fields
        public const int OneSecond = 1000;
        
        public readonly TimeSpan DiscreteUnit;
        private readonly IJettonReceiver _jettonReceiver;
        private readonly Timer _timer;
        #endregion _Consts&Fields


        #region Ctors
        public TimeManager(IJettonReceiver jettonReceiver, IOptions<TimeManagerOptions> options)
        {
            Options = options.Value;

            _jettonReceiver = jettonReceiver;
            _jettonReceiver.JettonPassed += ReceiverOnPassed;

            DiscreteUnit = TimeSpan.FromMilliseconds(OneSecond);
            LeftTime = TimeSpan.FromMinutes(Options.TimeAfterStartMinutes);
            JettonCost = TimeSpan.FromMinutes(Options.JettonCostMinutes);
            State = TimeStateFactory.CreateNew(this);
            
            _timer = new Timer(OneSecond)
            {
                Enabled = Options.SubtractingTimeEnabled
            };

            _timer.Elapsed += TimerOnTick;
        }
        #endregion


        #region Properties
        public TimeSpan LeftTime { get; private set; }
        public TimeState State { get; private set; }
        public TimeSpan JettonCost { get; }
        public TimeManagerOptions Options { get; }

        #endregion _Properties


        #region Events
        public event EventHandler<TimeChangedEventArgs> StateChanged = delegate { };
        public event EventHandler<TimeChangedEventArgs> StateChanging = delegate { };
        public event EventHandler<TimeChangedEventArgs> TimeChanged = delegate { };
        #endregion _Events


        #region Methods
        public void TransitionTo(TimeState state)
        {
            StateChanging.Invoke(this, new TimeChangedEventArgs(LeftTime, state));
            State = state;
            State.SetContext(this);
            StateChanged.Invoke(this, new TimeChangedEventArgs(LeftTime, state));
        }
        
        public void UpdateTime(TimeSpan newTime)
        {
            LeftTime = newTime;
            State.UpdateTime();
            TimeChanged.Invoke(this, new TimeChangedEventArgs(LeftTime, State));
        }
                
        private void ReceiverOnPassed(object? _, EventArgs e)
        {
            UpdateTime(LeftTime + JettonCost);
        }
        
        
        private void TimerOnTick(object? _, EventArgs e)
        {
            if (LeftTime >= DiscreteUnit)
                UpdateTime(LeftTime - DiscreteUnit);
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
                _timer.Elapsed -= TimerOnTick;
                
                _timer.Stop();
                _timer.Dispose();
            }

            _isDisposed = true;
        }
        #endregion
    }
}