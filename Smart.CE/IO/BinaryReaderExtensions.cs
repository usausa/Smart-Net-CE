namespace Smart.IO
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static T ReadStruct<T>(this BinaryReader reader) where T : struct
        {
            var bytes = new Byte[Marshal.SizeOf(typeof(T))];
            reader.Read(bytes, 0, bytes.Length);

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
