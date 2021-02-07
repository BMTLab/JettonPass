using System;

using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Services.Managers;


namespace JettonPass.App.Models.Time.States
{
    public class NormalTimeState : TimeState
    {
        #region Ctors
        public NormalTimeState(TimeManager context) : base(context)
        {
            Type = TimeStateType.NormalTimeState;
        }
        #endregion _Ctors


        #region Overrides of TimeState
        public override void UpdateTime()
        {
            if (Context.LeftTime <= TimeSpan.FromMinutes(Context.Options.TimeRunsOutThresholdMinutes) &&
                Context.LeftTime >= Context.DiscreteUnit)
            {
                Context.TransitionTo(new RunOutTimeState(Context));
            }
            
            if (Context.LeftTime < Context.DiscreteUnit)
            {
                Context.TransitionTo(new EndTimeState(Context));
            }
        }
        #endregion
    }
}