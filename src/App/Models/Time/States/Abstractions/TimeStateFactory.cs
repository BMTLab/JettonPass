using System;

using JettonPass.App.Services.Managers;


namespace JettonPass.App.Models.Time.States.Abstractions
{
    public static class TimeStateFactory
    {
        public static TimeState CreateNew(TimeManager timeManager)
        {
            if (timeManager.LeftTime < timeManager.DiscreteUnit)
                return new EndTimeState(timeManager);
            
            if (timeManager.LeftTime <= TimeSpan.FromMinutes(timeManager.Options.TimeRunsOutThresholdMinutes))
                return new EndTimeState(timeManager);

            return new NormalTimeState(timeManager);
        }
    }
}