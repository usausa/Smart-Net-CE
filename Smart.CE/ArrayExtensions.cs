namespace Smart
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class ArrayExtensions
    {
        //--------------------------------------------------------------------------------
        // IndexOf
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, Func<T, bool> predicate)
        {
            return IndexOf(array, 0, array != null ? array.Length : 0, predicate);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, int offset, int length, Func<T, bool> predicate)
        {
            if (array == null)
            {
                return -1;
            }

            var end = offset + length > array.Length ? array.Length : offset + length;
            for (var i = offset; i < end; i++)
            {
                if (predicate(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        //--------------------------------------------------------------------------------
        // Fill
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] Fill<T>(this T[] array, T value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] Fill<T>(this T[] array, int offset, int length, T value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Fill(this byte[] array, byte value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Fill(this byte[] array, int offset, int length, byte value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Buffer.BlockCopy(array, offset, array, offset + copy, copy);
            }

            Buffer.BlockCopy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short[] Fill(this short[] array, short value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short[] Fill(this short[] array, int offset, int length, short value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] Fill(this int[] array, int value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] Fill(this int[] array, int offset, int length, int value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long[] Fill(this long[] array, long value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long[] Fill(this long[] array, int offset, int length, long value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float[] Fill(this float[] array, float value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float[] Fill(this float[] array, int offset, int length, float value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double[] Fill(this double[] array, double value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double[] Fill(this double[] array, int offset, int length, double value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool[] Fill(this bool[] array, bool value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool[] Fill(this bool[] array, int offset, int length, bool value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char[] Fill(this char[] array, char value)
        {
            return Fill(array, 0, array != null ? array.Length : 0, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char[] Fill(this char[] array, int offset, int length, char value)
        {
            if ((length <= 0) || (array == null))
            {
                return array;
            }

            array[offset] = value;

            int copy;
            for (copy = 1; copy <= length / 2; copy <<= 1)
            {
                Array.Copy(array, offset, array, offset + copy, copy);
            }

            Array.Copy(array, offset, array, offset + copy, length - copy);

            return array;
        }

        //--------------------------------------------------------------------------------
        // Combine
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] array, params T[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new T[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static byte[] Combine(this byte[] array, params byte[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new byte[length.Value];

            int offset;
            if (array != null)
            {
                Buffer.BlockCopy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Buffer.BlockCopy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static short[] Combine(this short[] array, params short[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new short[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static int[] Combine(this int[] array, params int[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new int[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static long[] Combine(this long[] array, params long[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new long[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static float[] Combine(this float[] array, params float[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new float[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static double[] Combine(this double[] array, params double[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new double[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static bool[] Combine(this bool[] array, params bool[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new bool[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static char[] Combine(this char[] array, params char[][] others)
        {
            var length = array != null ? array.Length : (int?)null;
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    length = (length ?? 0) + other.Length;
                }
            }

            if (!length.HasValue)
            {
                return null;
            }

            var result = new char[length.Value];

            int offset;
            if (array != null)
            {
                Array.Copy(array, 0, result, 0, array.Length);
                offset = array.Length;
            }
            else
            {
                offset = 0;
            }

            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                if (other != null)
                {
                    Array.Copy(other, 0, result, offset, other.Length);
                    offset += other.Length;
                }
            }

            return result;
        }

        //--------------------------------------------------------------------------------
        // SubArray
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<T>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new T[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] SubArray(this byte[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<byte>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new byte[fixedLength];

            Buffer.BlockCopy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static short[] SubArray(this short[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<short>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new short[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int[] SubArray(this int[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<int>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new int[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static long[] SubArray(this long[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<long>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new long[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float[] SubArray(this float[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<float>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new float[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double[] SubArray(this double[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<double>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new double[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool[] SubArray(this bool[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<bool>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new bool[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static char[] SubArray(this char[] array, int offset, int length)
        {
            if (array == null)
            {
                return null;
            }

            if (offset >= array.Length)
            {
                return Empty<char>.Array;
            }

            var fixedLength = array.Length - offset < length ? array.Length - offset : length;
            var result = new char[fixedLength];

            Array.Copy(array, offset, result, 0, fixedLength);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] RemoveRange<T>(this T[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new T[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] RemoveAt(this byte[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] RemoveRange(this byte[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new byte[start + remainLength];

            if (start > 0)
            {
                Buffer.BlockCopy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Buffer.BlockCopy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static short[] RemoveAt(this short[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static short[] RemoveRange(this short[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new short[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int[] RemoveAt(this int[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int[] RemoveRange(this int[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new int[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long[] RemoveAt(this long[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static long[] RemoveRange(this long[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new long[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static float[] RemoveAt(this float[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float[] RemoveRange(this float[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new float[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static double[] RemoveAt(this double[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double[] RemoveRange(this double[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new double[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static bool[] RemoveAt(this bool[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool[] RemoveRange(this bool[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new bool[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static char[] RemoveAt(this char[] array, int offset)
        {
            return RemoveRange(array, offset, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static char[] RemoveRange(this char[] array, int start, int length)
        {
            if ((array == null) || (array.Length == 0) || (length <= 0) || (start < 0) || start >= array.Length)
            {
                return array;
            }

            var reaminStart = start + length;
            var remainLength = reaminStart > array.Length ? 0 : array.Length - reaminStart;
            var result = new char[start + remainLength];

            if (start > 0)
            {
                Array.Copy(array, 0, result, 0, start);
            }

            if (remainLength > 0)
            {
                Array.Copy(array, reaminStart, result, start, remainLength);
            }

            return result;
        }

        //--------------------------------------------------------------------------------
        // ArrayEqual
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals<T>(this T[] array, int offset, T[] other, int otherOffset, int length)
        {
            return ArrayEquals(array, offset, other, otherOffset, length, EqualityComparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool ArrayEquals<T>(this T[] array, int offset, T[] other, int otherOffset, int length, IEqualityComparer<T> comparer)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (!comparer.Equals(array[offset + i], other[otherOffset + i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this byte[] array, int offset, byte[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this short[] array, int offset, short[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this int[] array, int offset, int[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this long[] array, int offset, long[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this float[] array, int offset, float[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this double[] array, int offset, double[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this bool[] array, int offset, bool[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ArrayEquals(this char[] array, int offset, char[] other, int otherOffset, int length)
        {
            if ((array == null) && (other == null))
            {
                return true;
            }

            if ((array == null) || (other == null))
            {
                return false;
            }

            for (var i = 0; i < length; i++)
            {
                if (array[offset + i].CompareTo(other[otherOffset + i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
