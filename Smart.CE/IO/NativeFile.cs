namespace Smart.IO
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class NativeFile : IDisposable
    {
        public static readonly IntPtr InvalidHandle = new IntPtr(-1);

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return (Handle != IntPtr.Zero) && (Handle != InvalidHandle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get
            {
                return NativeMethods.GetFileSize(Handle, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NativeFile()
        {
            Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public NativeFile(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Handle = IntPtr.Zero;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        ~NativeFile()
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
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <param name="creationDisposition"></param>
        /// <param name="flagsAndAttributes"></param>
        public void Open(FileAccess access, FileShare share, FileCreateDisposition creationDisposition, int flagsAndAttributes)
        {
            uint dwDesiredAccess;
            switch (access)
            {
                case FileAccess.Read:
                    dwDesiredAccess = 0x80000000;
                    break;
                case FileAccess.Write:
                    dwDesiredAccess = 0x40000000;
                    break;
                case FileAccess.ReadWrite:
                    dwDesiredAccess = 0xC0000000;
                    break;
                default:
                    dwDesiredAccess = 0x20000000;
                    break;
            }

            var handle = NativeMethods.CreateFile(Name, dwDesiredAccess, (uint)share, 0, (uint)creationDisposition, (uint)flagsAndAttributes, 0);
            if (handle == InvalidHandle)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            Handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="share"></param>
        public void OpenExisting(FileAccess access, FileShare share)
        {
            Open(access, share, FileCreateDisposition.OpenExisting, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                if (!NativeMethods.CloseHandle(Handle))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesToRead"></param>
        /// <returns></returns>
        public byte[] Read(int bytesToRead)
        {
            var buffer = new byte[bytesToRead];
            var numberOfBytesRead = 0;
            if (!NativeMethods.ReadFile(Handle, buffer, bytesToRead, ref numberOfBytesRead, IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public int Write(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return Write(bytes, bytes.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="bytesToWrite"></param>
        /// <returns></returns>
        public int Write(byte[] bytes, int bytesToWrite)
        {
            var numberOfBytesWritten = 0;
            if (!NativeMethods.WriteFile(Handle, bytes, bytesToWrite, ref numberOfBytesWritten, IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return numberOfBytesWritten;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="seekFrom"></param>
        /// <returns></returns>
        public int Seek(int distance, SeekOrigin seekFrom)
        {
            var ret = NativeMethods.SetFilePointer(Handle, distance, 0, seekFrom);
            if (ret == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlCode"></param>
        public void DeviceIoControl(uint controlCode)
        {
            DeviceIoControl(controlCode, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlCode"></param>
        /// <param name="inData"></param>
        public void DeviceIoControl(uint controlCode, byte[] inData)
        {
            DeviceIoControl(controlCode, inData, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlCode"></param>
        /// <param name="inData"></param>
        /// <param name="outData"></param>
        public void DeviceIoControl(uint controlCode, byte[] inData, byte[] outData)
        {
            int bytesReturned;
            DeviceIoControl(controlCode, inData, outData, out bytesReturned);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlCode"></param>
        /// <param name="inData"></param>
        /// <param name="outData"></param>
        /// <param name="bytesReturned"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "P/Invoke")]
        public void DeviceIoControl(uint controlCode, byte[] inData, byte[] outData, out int bytesReturned)
        {
            if (outData == null)
            {
                if (inData != null)
                {
                    if (NativeMethods.DeviceIoControl(Handle, controlCode, inData, inData.Length, IntPtr.Zero, 0, out bytesReturned, IntPtr.Zero) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else if (NativeMethods.DeviceIoControl(Handle, controlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out bytesReturned, IntPtr.Zero) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else if (inData == null)
            {
                if (NativeMethods.DeviceIoControl(Handle, controlCode, IntPtr.Zero, 0, outData, outData.Length, out bytesReturned, IntPtr.Zero) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else if (NativeMethods.DeviceIoControl(Handle, controlCode, inData, inData.Length, outData, outData.Length, out bytesReturned, IntPtr.Zero) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}
