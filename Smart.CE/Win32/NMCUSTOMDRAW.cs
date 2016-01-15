namespace Smart.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "P/Invoke")]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMCUSTOMDRAW
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public NMHDR Header;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int DrawStage;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible", Justification = "P/Invoke")]
        public IntPtr Hdc;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public RECT Rect;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible", Justification = "P/Invoke")]
        public IntPtr ItemSpec;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int ItemState;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible", Justification = "P/Invoke")]
        public IntPtr ItemlParam;
    }
}
