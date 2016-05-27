namespace Smart.Drawing
{
    using System;
    using System.IO;

    /// <summary>
    ///
    /// </summary>
    public class DibSection
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BitCount { get; private set; }

        private int pixelsPerY;

        private byte[] data;

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Performance")]
        public int GetPixel(int x, int y)
        {
            switch (BitCount)
            {
                case 1:
                case 2:
                case 4:
                case 16:
                case 32:
                    throw new NotSupportedException("Supported only 8 or 24 bpp.");

                case 8:
                    return data[((Height - y - 1) * pixelsPerY) + x];

                case 24:
                    var pos = ((Height - y - 1) * pixelsPerY) + (x * 3);
                    return (data[pos] << 16) + (data[pos + 1] << 8) + data[pos + 2];

                default:
                    throw new NotSupportedException("Supported only 8 or 24 bpp.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static DibSection GetDibData(Stream stream)
        {
            var data = new DibSection();

            var reader = new BinaryReader(stream);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            if (reader.ReadByte() != 'B' || reader.ReadByte() != 'M')
            {
                throw new ArgumentException("Invalid bitmap stream.", "stream");
            }

            reader.BaseStream.Seek(10, SeekOrigin.Begin);
            var offBits = reader.ReadInt32();

            reader.ReadInt32(); // biSize

            data.Width = reader.ReadInt32();
            data.Height = reader.ReadInt32();

            reader.ReadInt16(); // biPlanes

            data.BitCount = reader.ReadInt16();

            reader.BaseStream.Seek(offBits, SeekOrigin.Begin);
            data.pixelsPerY = (((data.Width * (data.BitCount >> 3)) + 3) >> 2) << 2;
            data.data = reader.ReadBytes(data.pixelsPerY * data.Height);

            return data;
        }
    }
}
