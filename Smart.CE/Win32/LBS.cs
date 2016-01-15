namespace Smart.Win32
{
    /// <summary>
    /// ListBox Style
    /// </summary>
    public enum LBS
    {
        NOTIFY            = 0x0001,
        SORT              = 0x0002,
        NOREDRAW          = 0x0004,
        MULTIPLESEL       = 0x0008,
        HASSTRINGS        = 0x0040,
        USETABSTOPS       = 0x0080,
        NOINTEGRALHEIGHT  = 0x0100,
        MULTICOLUMN       = 0x0200,
        WANTKEYBOARDINPUT = 0x0400,
        EXTENDEDSEL       = 0x0800,
        DISABLENOSCROLL   = 0x1000,
        NODATA            = 0x2000,
        STANDARD          = (NOTIFY | SORT | WS.VSCROLL | WS.BORDER)
    }
}
