﻿using System.Diagnostics.CodeAnalysis;


namespace JettonPass.App.Models.WinApiEnums
{
    [SuppressMessage("Enum must not have duplicate values", "CA1069")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum GetWindowLongConst : int
    {
        NONE = 0,
        GWL_WNDPROC = -4,
        GWL_HINSTANCE = -6,
        GWL_HWNDPARENT = -8,
        GWL_STYLE = -16,
        GWL_EXSTYLE = -20,
        GWL_USERDATA = -21,
        GWL_ID = -12
    }
}