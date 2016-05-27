namespace Smart.Runtime.InteropServices
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class MarshalEx
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static IntPtr GetHINSTANCE(Module m)
        {
            if (m == null)
            {
                throw new ArgumentNullException("m");
            }

            var hInstance = NativeMethods.GetModuleHandle(Equals(m.Assembly, Assembly.GetCallingAssembly()) ? null : m.Assembly.GetName().CodeBase);
            return hInstance == IntPtr.Zero ? new IntPtr(-1) : hInstance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IntPtr StringToHGlobalUni(string s)
        {
            if (s == null)
            {
                return IntPtr.Zero;
            }
            var bytes = Encoding.Unicode.GetBytes(s + '\0');
            var destination = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, destination, bytes.Length);
            return destination;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(object value)
        {
            var size = Marshal.SizeOf(value);
            var ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(value, ptr, true);
                var data = new byte[size];
                Marshal.Copy(ptr, data, 0, size);
                return data;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T BytesToStruct<T>(byte[] data) where T : struct
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            return BytesToStruct<T>(data, 0, data.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T BytesToStruct<T>(byte[] data, int offset, int size) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(data, offset, ptr, size);
                return (T)Marshal.PtrToStructure(ptr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
