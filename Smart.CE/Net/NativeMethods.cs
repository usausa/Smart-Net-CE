namespace Smart.Net
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    internal struct NETRESOURCE
    {
        internal int Scope;
        internal int Type;
        internal int DisplayType;
        internal int Usage;
        internal IntPtr LocalName;
        internal IntPtr RemoteName;
        internal IntPtr Comment;
        internal IntPtr Provider;
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        // MapDrive

        [DllImport("coredll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int WNetAddConnection3(IntPtr hwndOwner, IntPtr lpNetResource, string lpPassword, string lpUserName, int dwFlags);

        [DllImport("coredll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int WNetCancelConnection2(string lpName, int dwFlags, int fForce);
    }
}
