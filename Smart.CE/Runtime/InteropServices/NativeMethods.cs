namespace Smart.Runtime.InteropServices
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string modName);
    }
}
