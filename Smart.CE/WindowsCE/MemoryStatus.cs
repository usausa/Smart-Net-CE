namespace Smart.WindowsCE
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "P/Invoke")]
    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryStatus
    {
        internal int Length;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int MemoryLoad;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int TotalPhysical;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int AvailablePhysical;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int TotalPageFile;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int AvailablePageFile;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int TotalVirtual;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int AvailableVirtual;
    }
}
