namespace Smart.IO
{
    using System;
    using System.IO;

    public static class StreamExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void CopyTo(this Stream input, Stream output)
        {
            if (ReferenceEquals(output, null))
            {
                throw new ArgumentNullException("output");
            }

            const int BufferSize = 4096;
            var buffer = new byte[BufferSize];
            while (true)
            {
                var read = input.Read(buffer, 0, BufferSize);
                if (read == 0)
                {
                    break;
                }
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="bufferSize"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void CopyTo(this Stream input, Stream output, int bufferSize)
        {
            if (ReferenceEquals(output, null))
            {
                throw new ArgumentNullException("output");
            }

            var buffer = new byte[bufferSize];
            while (true)
            {
                var read = input.Read(buffer, 0, bufferSize);
                if (read == 0)
                {
                    break;
                }
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static byte[] ReadAllBytes(this Stream stream)
        {
            var memoryStream = stream as MemoryStream;
            if (memoryStream != null)
            {
                return memoryStream.ToArray();
            }

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static byte[] ReadBytes(this Stream stream, int size)
        {
            var buffer = new byte[size];
            var offset = 0;
            while (offset < size)
            {
                var read = stream.Read(buffer, offset, size - offset);
                if (read == 0)
                {
                    return null;
                }
                offset += read;
            }
            return buffer;
        }
    }
}
