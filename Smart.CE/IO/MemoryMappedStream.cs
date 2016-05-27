namespace Smart.IO
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    public class MemoryMappedStream : Stream
    {
        private const int SECTION_ALL_ACCESS = 0xf001f;
        private const int FILE_ATTRIBUTE_NORMAL = 0x00000080;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 0x00000001;
        private const int FILE_SHARE_WRITE = 0x00000002;

        public const long DEFAULT_INMEMORY_MAPSIZE = 0x10000;

        public static readonly IntPtr InvalidHandle = new IntPtr(-1);

        private IntPtr fileHandle;
        private IntPtr mapping;
        private IntPtr view;
        private readonly long length;
        private long position;

        /// <summary>
        ///
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        ///
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        ///
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        ///
        /// </summary>
        public override bool CanTimeout
        {
            get { return false; }
        }

        /// <summary>
        ///
        /// </summary>
        public override long Length
        {
            get { return length; }
        }

        /// <summary>
        ///
        /// </summary>
        public override long Position
        {
            get { return position; }
            set
            {
                if ((value < 0) || (value > Length))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                position = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public IntPtr Pointer
        {
            get
            {
                return new IntPtr(view.ToInt32() + position);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="name"></param>
        /// <param name="maxSize"></param>
        private MemoryMappedStream(IntPtr fileHandle, string name, long maxSize)
        {
            this.fileHandle = fileHandle;
            Name = name;
            length = maxSize;

            mapping = NativeMethods.CreateFileMapping(fileHandle, IntPtr.Zero, PageProtection.ReadWrite, (uint)(maxSize >> 32), (uint)(((ulong)maxSize) & 0xffffffffL), Name);
            if (mapping == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            view = NativeMethods.MapViewOfFile(mapping, SECTION_ALL_ACCESS, 0, 0, 0);
            if (view == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                NativeMethods.CloseHandle(mapping);
                throw new Win32Exception(error);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static MemoryMappedStream CreateForFile(string fileName, int maxSize)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            var fileHandle = NativeMethods.CreateFileForMapping(fileName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero,
                                                                FileMode.OpenOrCreate, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            if (fileHandle == InvalidHandle)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (maxSize == 0)
            {
                maxSize = NativeMethods.GetFileSize(fileHandle, IntPtr.Zero);
            }

            return new MemoryMappedStream(fileHandle, fileName, maxSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MemoryMappedStream CreateForFile(string fileName)
        {
            return CreateForFile(fileName, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static MemoryMappedStream CreateInMemory(string name, long maxSize)
        {
            return new MemoryMappedStream(InvalidHandle, name, maxSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static MemoryMappedStream CreateInMemoryMap()
        {
            return CreateInMemory(null, DEFAULT_INMEMORY_MAPSIZE);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MemoryMappedStream CreateInMemoryMap(string name)
        {
            return CreateInMemory(name, DEFAULT_INMEMORY_MAPSIZE);
        }

        /// <summary>
        ///
        /// </summary>
        public override void Close()
        {
            Flush();

            if (view != IntPtr.Zero)
            {
                NativeMethods.UnmapViewOfFile(view);
                view = IntPtr.Zero;
            }

            if (mapping != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(mapping);
                mapping = IntPtr.Zero;
            }

            if ((fileHandle != IntPtr.Zero) && (fileHandle != InvalidHandle))
            {
                NativeMethods.CloseHandle(fileHandle);
                fileHandle = IntPtr.Zero;
            }

            base.Close();
        }

        /// <summary>
        ///
        /// </summary>
        public override void Flush()
        {
            NativeMethods.FlushViewOfFile(mapping, (uint)Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            var pos = 0L;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    pos = offset;
                    break;

                case SeekOrigin.Current:
                    pos = position + offset;
                    break;

                case SeekOrigin.End:
                    pos = Length + offset;
                    break;
            }

            if ((pos < 0L) || (pos > Length))
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            position = pos;

            return position;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Position + offset < 0L)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (Position + offset + count > Length)
            {
                throw new EndOfStreamException();
            }

            Marshal.Copy(Pointer, buffer, offset, count);

            position += count;

            return count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (Position + offset < 0L)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (Position + offset + count > Length)
            {
                throw new EndOfStreamException();
            }

            Marshal.Copy(buffer, offset, Pointer, count);

            position += count;
        }

        //--------------------------------------------------------------------------------
        // Not Implemented
        //--------------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public override int ReadTimeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public override int WriteTimeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
