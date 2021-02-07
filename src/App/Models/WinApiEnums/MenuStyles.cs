using System;


namespace JettonPass.App.Models.WinApiEnums
{
    [Flags]
    public enum MenuStyles
    {
        ByPosition = 0x00000400,
        Remove = 0x00001000
    }
}