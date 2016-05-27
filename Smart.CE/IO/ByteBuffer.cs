namespace Smart.IO
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class ByteBuffer
    {
        // position <= limit <= array.Length

        private readonly byte[] array;

        private readonly int capacity;

        private int position;

        private int limit;

        private IByteOrder order = ByteOrders.Default;

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Performance")]
        public byte[] Array
        {
            get { return array; }
        }

        /// <summary>
        ///
        /// </summary>
        public int Position
        {
            get { return position; }
            set
            {
                if ((value > limit) || (value < 0))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                position = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int Limit
        {
            get { return limit; }
            set
            {
                if ((value > capacity) || (value < 0))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                limit = value;
                if (position > limit)
                {
                    position = limit;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int Remaining
        {
            get
            {
                return limit - position;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool HasRemaining
        {
            get
            {
                return position < limit;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="capacity"></param>
        public ByteBuffer(int capacity)
        {
            array = new byte[capacity];
            this.capacity = capacity;
            limit = capacity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        public ByteBuffer(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            this.array = array;
            capacity = array.Length;
            limit = capacity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public ByteBuffer(byte[] array, int offset, int length)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            this.array = array;
            position = offset;
            capacity = length;
            limit = length;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static ByteBuffer CopyOf(ByteBuffer array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            return CopyOf(array.array, array.position, array.Remaining);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static ByteBuffer CopyOf(byte[] array, int offset, int length)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            var copy = new byte[length];
            Buffer.BlockCopy(array, offset, copy, 0, length);
            return new ByteBuffer(copy, 0, length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ByteBuffer Clear()
        {
            position = 0;
            limit = capacity;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ByteBuffer Flip()
        {
            limit = position;
            position = 0;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="byteOrder"></param>
        /// <returns></returns>
        public ByteBuffer Order(IByteOrder byteOrder)
        {
            if (byteOrder == null)
            {
                throw new ArgumentNullException("byteOrder");
            }

            order = byteOrder;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBuffer Put(int index, byte value)
        {
            array[index] = value;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBuffer PutBytes(int index, byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Buffer.BlockCopy(value, 0, array, index, value.Length);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBuffer PutShort(int index, short value)
        {
            order.PutShort(array, index, value);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBuffer PutInt(int index, int value)
        {
            order.PutInt(array, index, value);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte Get(int index)
        {
            return array[index];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBytes(int index, int length)
        {
            var bytes = new byte[length];
            Buffer.BlockCopy(array, index, bytes, 0, length);
            return bytes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(int index)
        {
            return order.GetShort(array, index);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(int index)
        {
            return order.GetInt(array, index);
        }
    }
}
