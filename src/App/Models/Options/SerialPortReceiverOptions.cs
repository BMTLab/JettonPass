using JettonPass.SerialPortListener.Models.Enums;

using NullGuard;


namespace JettonPass.App.Models.Options
{
    [NullGuard(ValidationFlags.None)]
    public sealed record SerialPortReceiverOptions
    {
        public PinType TrackingPin { get; init; }
        public bool OnFrontSignal { get; init; }
    }
}