namespace Smart.IO
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    public class PhysicalAddress : IDisposable
    {
        private const uint PAGE_SIZE = 0x1000;

        private const uint MEM_RESERVE = 0x00002000;
        private const uint MEM_RELEASE = 0x00008000;

        private const uint PAGE_NOACCESS = 0x001;
        private const uint PAGE_READWRITE = 0x004;
        private const uint PAGE_NOCACHE = 0x200;
        private const uint PAGE_WRITECOMBINE = 0x400;

        private readonly IntPtr addressPointer;
        private IntPtr virtualAddress;

        /// <summary>
        ///
        /// </summary>
        /// <param name="physicalAddress"></param>
        /// <param name="size"></param>
        public PhysicalAddress(uint physicalAddress, int size)
            : this(physicalAddress, (uint)size)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="physicalAddress"></param>
        /// <param name="size"></param>
        public PhysicalAddress(uint physicalAddress, uint size)
        {
            var alignAddress = physicalAddress & 0xfffff000;
            var offset = physicalAddress - alignAddress;

            var dwSize = ((size + offset + PAGE_SIZE) - 1) & 0xfffff000;

            virtualAddress = NativeMethods.VirtualAlloc(0, dwSize, MEM_RESERVE, PAGE_NOACCESS);
            if (virtualAddress == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (NativeMethods.VirtualCopy(virtualAddress, (IntPtr)(alignAddress >> 8), dwSize,
                                          PAGE_READWRITE | PAGE_NOCACHE | PAGE_WRITECOMBINE) == 0)
            {
                var error = Marshal.GetLastWin32Error();
                NativeMethods.VirtualFree(virtualAddress, 0, MEM_RELEASE);
                throw new Win32Exception(error);
            }

            addressPointer = new IntPtr(virtualAddress.ToInt32() + offset);
        }

        /// <summary>
        ///
        /// </summary>
        ~PhysicalAddress()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (virtualAddress != IntPtr.Zero)
            {
                NativeMethods.VirtualFree(virtualAddress, 0, MEM_RELEASE);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "By Design")]
        public unsafe void* GetUnsafePointer()
        {
            return virtualAddress.ToPointer();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Ignore")]
        public static explicit operator IntPtr(PhysicalAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            return address.virtualAddress;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return Marshal.ReadByte(addressPointer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadBytes(int length)
        {
            var buffer = new byte[length];
            Marshal.Copy(addressPointer, buffer, 0, length);
            return buffer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            return Marshal.ReadInt16(addressPointer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            return Marshal.ReadInt32(addressPointer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int ReadInt32(int offset)
        {
            return Marshal.ReadInt32(addressPointer, offset);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        public void WriteByte(byte data)
        {
            Marshal.WriteByte(addressPointer, data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        public void WriteBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            Marshal.Copy(bytes, 0, addressPointer, bytes.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        public void WriteInt16(short data)
        {
            Marshal.WriteInt16(addressPointer, data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        public void WriteInt32(int data)
        {
            Marshal.WriteInt32(addressPointer, data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public void WriteInt32(int data, int offset)
        {
            Marshal.WriteInt32(addressPointer, offset, data);
        }
    }
}
