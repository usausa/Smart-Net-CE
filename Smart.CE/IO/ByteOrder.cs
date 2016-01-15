namespace Smart.IO
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IByteOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void PutShort(byte[] bytes, int index, short value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void PutInt(byte[] bytes, int index, int value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        short GetShort(byte[] bytes, int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int GetInt(byte[] bytes, int index);
    }

    /// <summary>
    /// 
    /// </summary>
    internal class DefaultEndian : IByteOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void PutShort(byte[] bytes, int index, short value)
        {
            var data = BitConverter.GetBytes(value);
            Buffer.BlockCopy(data, 0, bytes, index, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void PutInt(byte[] bytes, int index, int value)
        {
            var data = BitConverter.GetBytes(value);
            Buffer.BlockCopy(data, 0, bytes, index, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(byte[] bytes, int index)
        {
            return BitConverter.ToInt16(bytes, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(byte[] bytes, int index)
        {
            return BitConverter.ToInt32(bytes, index);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class ReverseEndian : IByteOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void PutShort(byte[] bytes, int index, short value)
        {
            var data = BitConverter.GetBytes(value);
            Array.Reverse(data);
            Buffer.BlockCopy(data, 0, bytes, index, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void PutInt(byte[] bytes, int index, int value)
        {
            var data = BitConverter.GetBytes(value);
            Array.Reverse(data);
            Buffer.BlockCopy(data, 0, bytes, index, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(byte[] bytes, int index)
        {
            var data = new byte[2];
            Buffer.BlockCopy(bytes, index, data, 0, data.Length);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(byte[] bytes, int index)
        {
            var data = new byte[4];
            Buffer.BlockCopy(bytes, index, data, 0, data.Length);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ByteOrders
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Mutable")]
        public static readonly IByteOrder Default = new DefaultEndian();

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Mutable")]
        public static readonly IByteOrder LittleEndian = BitConverter.IsLittleEndian ? (IByteOrder)new DefaultEndian() : new ReverseEndian();

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Mutable")]
        public static readonly IByteOrder BigEndian = BitConverter.IsLittleEndian ? (IByteOrder)new ReverseEndian() : new DefaultEndian();
    }
}
