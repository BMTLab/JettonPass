namespace JettonPass.SerialPortListener.Models.Enums
{
    /// <summary>
    ///     3 signal pins in the serial port
    /// </summary>
    public enum PinType : byte
    {
        None = 0,
        Cd = 1,
        Dtr = 4,
        Dsr = 6,
        Cts = 8
    }
}