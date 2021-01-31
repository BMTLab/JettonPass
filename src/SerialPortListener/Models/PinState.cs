using JettonPass.SerialPortListener.Models.Enums;


namespace JettonPass.SerialPortListener.Models
{
    public class PinState
    {
        public PinType PinType { get; init; }
        public bool IsHolding { get; set; }
        public int HoldingStableTime { get; set; }
    }
}