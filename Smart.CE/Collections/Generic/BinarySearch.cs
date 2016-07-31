namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class BinarySearch
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int Find<T>(IList<T> list, Func<T, int> comparer)
        {
            return Find(list, 0, list.Count, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int Find<T>(IList<T> list, int index, int length, Func<T, int> comparer)
        {
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer(list[mid]);

                if (c == 0)
                {
                    return mid;
                }

                if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return ~lo;
        }
    }
}
