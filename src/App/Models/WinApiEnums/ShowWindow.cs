using System.Diagnostics.CodeAnalysis;


namespace JettonPass.App.Models.WinApiEnums
{
    [SuppressMessage("Enum must not have duplicate values", "CA1069")]
    [SuppressMessage("Remove all but one null member from WindowStyles, named 'None'", "CA1008")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ShowWindow : byte
    {
        SW_SHOWMAXIMIZED = 3
    }
}