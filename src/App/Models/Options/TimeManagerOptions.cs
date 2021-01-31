using NullGuard;


namespace JettonPass.App.Models.Options
{
    [NullGuard(ValidationFlags.None)]
    public sealed class TimeManagerOptions
    {
        public double TimeAfterStartMinutes { get; init; }
        public double JettonCostMinutes { get; init; }
        public double TimeRunsOutThresholdMinutes { get; init; }
        public bool SubtractingTimeEnabled { get; init; }
    }
}