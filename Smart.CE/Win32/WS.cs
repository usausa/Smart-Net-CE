namespace Smart.Win32
{
    /// <summary>
    /// Window Style
    /// </summary>
    public enum WS
    {
        CHILD        = 0x40000000,
        VISIBLE      = 0x10000000,
        DISABLED     = 0x08000000,
        CLIPSIBLINGS = 0x04000000,
        CLIPCHILDREN = 0x02000000,
        CAPTION      = 0x00C00000,
        BORDER       = 0x00800000,
        DLGFRAME     = 0x00400000,
        VSCROLL      = 0x00200000,
        HSCROLL      = 0x00100000,
        SYSMENU      = 0x00080000,
        THICKFRAME   = 0x00040000,

        GROUP        = 0x00020000,
        TABSTOP      = 0x00010000,

        MAXIMIZEBOX  = 0x00020000,
        MINIMIZEBOX  = 0x00010000,

        SIZEBOX      = THICKFRAME,
        POPUP        = -0x7fffffff,
        OVERLAPPED   = BORDER | CAPTION,
    }
}
