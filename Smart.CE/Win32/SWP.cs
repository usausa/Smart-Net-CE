namespace Smart.Win32
{
    using System;

    /// <summary>
    /// SetWindowPos
    /// </summary>
    [Flags]
    public enum SWP 
    {
        NOSIZE          = 0x0001,
        NOMOVE          = 0x0002,
        NOZORDER        = 0x0004,
        NOREDRAW        = 0x0008,
        NOACTIVATE      = 0x0010,
        FRAMECHANGED    = 0x0020,
        SHOWWINDOW      = 0x0040,
        HIDEWINDOW      = 0x0080,
        NOCOPYBITS      = 0x0100,
        NOOWNERZORDER   = 0x0200, 
        NOSENDCHANGING  = 0x0400,
        DRAWFRAME       = FRAMECHANGED,
        NOREPOSITION    = NOOWNERZORDER,
        DEFERERASE      = 0x2000,
        ASYNCWINDOWPOS  = 0x4000
    }
}
