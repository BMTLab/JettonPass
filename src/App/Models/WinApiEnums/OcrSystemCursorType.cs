using System.Diagnostics.CodeAnalysis;


namespace JettonPass.App.Models.WinApiEnums
{
    [SuppressMessage("None add", "CA1008")]
    public enum OcrSystemCursorType : uint
    {
        /// <summary>
        ///     Standard arrow and small hourglass
        /// </summary>
        OcrAppstarting = 32650,

        /// <summary>
        ///     Standard arrow
        /// </summary>
        OcrNormal = 32512,

        /// <summary>
        ///     Crosshair
        /// </summary>
        OcrCross = 32515,

        /// <summary>
        ///     Windows 2000/XP: Hand
        /// </summary>
        OcrHand = 32649,

        /// <summary>
        ///     Arrow and question mark
        /// </summary>
        OcrHelp = 32651,

        /// <summary>
        ///     I-beam
        /// </summary>
        OcrIbeam = 32513,

        /// <summary>
        ///     Slashed circle
        /// </summary>
        OcrNo = 32648,

        /// <summary>
        ///     Four-pointed arrow pointing north, south, east, and west
        /// </summary>
        OcrSizeall = 32646,

        /// <summary>
        ///     Double-pointed arrow pointing northeast and southwest
        /// </summary>
        OcrSizenesw = 32643,

        /// <summary>
        ///     Double-pointed arrow pointing north and south
        /// </summary>
        OcrSizens = 32645,

        /// <summary>
        ///     Double-pointed arrow pointing northwest and southeast
        /// </summary>
        OcrSizenwse = 32642,

        /// <summary>
        ///     Double-pointed arrow pointing west and east
        /// </summary>
        OcrSizewe = 32644,

        /// <summary>
        ///     Vertical arrow
        /// </summary>
        OcrUp = 32516,

        /// <summary>
        ///     Hourglass
        /// </summary>
        OcrWait = 32514
    }

}