using System;


namespace JettonPass.App.Models.Time
{
    public sealed class TimeChangedEventArgs : EventArgs
    {
        #region Ctors
        public TimeChangedEventArgs(TimeSpan newTime)
        {
            NewValue = newTime;
        }
        #endregion


        #region Properties
        public TimeSpan NewValue { get; }
        #endregion _Properties
    }
}