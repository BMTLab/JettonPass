using System;

using JettonPass.App.Models.Time.States.Abstractions;


namespace JettonPass.App.Models.Time
{
    public sealed class TimeChangedEventArgs : EventArgs
    {
        #region Ctors
        public TimeChangedEventArgs(TimeSpan newTime, TimeState newState)
        {
            NewTime = newTime;
            NewState = newState;
        }
        #endregion


        #region Properties
        public TimeSpan NewTime { get; }
        public TimeState NewState { get; }
        #endregion _Properties


        #region Methods
        #region Overrides of Object
        public override string ToString() =>
            NewTime.ToString();
        #endregion
        #endregion _Methods
    }
}