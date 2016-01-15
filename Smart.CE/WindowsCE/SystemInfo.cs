namespace Smart.WindowsCE
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "P/Invoke")]
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public ProcessorArchitecture ProcessorArchitecture;
        internal ushort Reserved;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int PageSize;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int MinimumApplicationAddress;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int MaximumApplicationAddress;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int ActiveProcessorMask;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int NumberOfProcessors;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public ProcessorType ProcessorType;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public int AllocationGranularity;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public short ProcessorLevel;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
        public short ProcessorRevision;
    }
}
