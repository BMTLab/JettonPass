using System;

using JettonPass.App.Models.Time.States.Abstractions;
using JettonPass.App.Services.Managers;


namespace JettonPass.App.Models.Time.States
{
    public class RunOutTimeState : TimeState
    {
        #region Ctors
        public RunOutTimeState(TimeManager context) : base(context)
        {
            Type = TimeStateType.RunOutTimeState;
        }
        #endregion


        #region Overrides of TimeState
        public override void UpdateTime()
        {
            if (Context.LeftTime > TimeSpan.FromMinutes(Context.Options.TimeRunsOutThresholdMinutes))
            {
                Context.TransitionTo(new NormalTimeState(Context));
            }
            
            if (Context.LeftTime < Context.DiscreteUnit)
            {
                Context.TransitionTo(new EndTimeState(Context));
            }
        }
        #endregion
    }
}