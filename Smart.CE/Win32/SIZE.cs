namespace Smart.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "P/Invoke")]
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int Width;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int Height;
    }
}
