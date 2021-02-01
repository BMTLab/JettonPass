using System.Collections.Generic;

using JetBrains.Annotations;

using JettonPass.SerialPortListener.Models.Enums;

using NullGuard;


namespace JettonPass.SerialPortListener.Models.Options
{
    [NullGuard(ValidationFlags.None)]
    public record SerialPortOptions
    {
        public string PortName { get; init; } = string.Empty;
        public int BaudRate { get; init; }
        public byte ReadBufferSize { get; init; }
        public byte WriteBufferSize { get; init; }
        public bool DtrEnable { get; init; }
        public bool RtsEnable { get; init; }
        public int Priority { get; init; }
        public int Filter { get; init; }
        public IDictionary<PinType, bool> TrackedPins { get; [UsedImplicitly] init; } = default!;
    }
}