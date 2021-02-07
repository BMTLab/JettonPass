using System;

using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Services.Managers;


namespace JettonPass.App.Models.Time.States
{
    public class EndTimeState : TimeState
    {
        #region Ctors
        public EndTimeState(TimeManager context) : base(context)
        {
            Type = TimeStateType.EndTimeState;
            NoTime = true;
        }
        #endregion
        
        
        #region Overrides of TimeState
        public override void UpdateTime()
        {
            if (Context.LeftTime <= TimeSpan.FromMinutes(Context.Options.TimeRunsOutThresholdMinutes) &&
                Context.LeftTime >= Context.DiscreteUnit)
            {
                Context.TransitionTo(new RunOutTimeState(Context));
            }
            
            if (Context.LeftTime > TimeSpan.FromMinutes(Context.Options.TimeRunsOutThresholdMinutes))
            {
                Context.TransitionTo(new NormalTimeState(Context));
            }
        }
        #endregion
    }
}