namespace Smart.Win32
{
    using System;

    /// <summary>
    /// Extend Window Style
    /// </summary>
    [Flags]
    public enum WS_EX : uint
    {
        NONE             = 0,
        DLGMODALFRAME    = 0x00000001,
        TOPMOST          = 0x00000008,
        TOOLWINDOW       = 0x00000080,
        WINDOWEDGE       = 0x00000100,
        CLIENTEDGE       = 0x00000200,
        CONTEXTHELP      = 0x00000400,
        STATICEDGE       = 0x00020000,
        NOACTIVATE       = 0x08000000,
        NOANIMATION      = 0x04000000,
        INK              = 0x10000000,
        ABOVESTARTUP     = 0x20000000,
        NODRAG           = 0x40000000,

        CAPTIONOKBUTTON  = 0x80000000,

        OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE),
    }
}
