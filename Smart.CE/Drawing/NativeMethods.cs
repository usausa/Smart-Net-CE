namespace Smart.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    internal struct TRIVERTEX
    {
        internal int X;
        internal int Y;
        internal ushort Red;
        internal ushort Green;
        internal ushort Blue;
        internal ushort Alpha;

        internal TRIVERTEX(int x, int y, Color color)
            : this(x, y, color.R, color.G, color.B, color.A)
        {
        }

        internal TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
        {
            X = x;
            Y = y;
            Red = (ushort)(red << 8);
            Green = (ushort)(green << 8);
            Blue = (ushort)(blue << 8);
            Alpha = (ushort)(alpha << 8);
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal struct GRADIENT_RECT
    {
        internal uint UpperLeft;
        internal uint LowerRight;

        internal GRADIENT_RECT(uint ul, uint lr)
        {
            UpperLeft = ul;
            LowerRight = lr;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal struct BlendFunction
    {
        internal byte BlendOp;
        internal byte BlendFlags;
        internal byte SourceConstantAlpha;
        internal byte AlphaFormat;
    }

    /// <summary>
    ///
    /// </summary>
    public enum RasterOp : uint
    {
        SRCCOPY = 0x00CC0020,
        SRCPAINT = 0x00EE0086,
        SRCAND = 0x008800C6,
        SRCINVERT = 0x00660046,
        SRCERASE = 0x00440328,
        NOTSRCCOPY = 0x00330008,
        NOTSRCERASE = 0x001100A6,
        MERGECOPY = 0x00C000CA,
        MERGEPAINT = 0x00BB0226,
        PATCOPY = 0x00F00021,
        PATPAINT = 0x00FB0A09,
        PATINVERT = 0x005A0049,
        DSTINVERT = 0x00550009,
        BLACKNESS = 0x00000042,
        WHITENESS = 0x00FF0062,
    }

    /// <summary>
    ///
    /// </summary>
    internal enum PenStyle
    {
        SOLID = 0,
        DASH = 1,
    }

    /// <summary>
    ///
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct LOGFONT
    {
        internal int Height;
        internal int Width;
        internal int Escapement;
        internal int Orientation;
        internal int Weight;
        internal byte Italic;
        internal byte Underline;
        internal byte StrikeOut;
        internal byte CharSet;
        internal byte OutPrecision;
        internal byte ClipPrecision;
        internal byte Quality;
        internal byte PitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string FaceName;
    }

    /// <summary>
    ///
    /// </summary>
    internal enum RgnCombineMode
    {
        RGN_AND = 1,
        RGN_OR = 2,
        RGN_XOR = 3,
        RGN_DIFF = 4,
        RGN_COPY = 5,
        RGN_MIN = RGN_AND,
        RGN_MAX = RGN_COPY,
    }

    /// <summary>
    ///
    /// </summary>
    internal static class NativeMethods
    {
        // DC

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);

        // Draw

        [DllImport("coredll.dll", SetLastError = true, EntryPoint = "GradientFill")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern bool AlphaBlend(IntPtr hdcDest, int xDest, int yDest, int cxDest, int cyDest, IntPtr hdcSrc, int xSrc, int ySrc, int cxSrc, int cySrc, BlendFunction blendFunction);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, RasterOp dwRop);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, RasterOp dwRop);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern int MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern int LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern uint Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        // Object

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, int crColor);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CreateFontIndirect(ref LOGFONT pLogFont);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int GetObject(IntPtr hObj, int cb, ref LOGFONT objdata);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern IntPtr GetStockObject(int fnObject);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // Region

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, int bRedraw);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int CombineRgn(IntPtr hRgnDest, IntPtr hRgnSrc1, IntPtr hRgnSrc2, RgnCombineMode fnCombineMode);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern IntPtr ExtCreateRegion(IntPtr lpXform, int nCount, int[] lpRgnData);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern int GetRgnBox(IntPtr hRgn, out RECT rc);

        //[DllImport("coredll.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool PtInRegion(IntPtr hRgn, int c, int y);
    }
}
