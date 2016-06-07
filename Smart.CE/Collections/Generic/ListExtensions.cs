namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class ListExtensions
    {
        //--------------------------------------------------------------------------------
        // Compare
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, IList<T> other) where T : IComparable<T>
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            var length = Math.Min(list.Count, other.Count);

            var ret = CompareInternal(list, 0, other, 0, length);

            return ret != 0 ? ret : list.Count - other.Count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="other"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, IList<T> other, int length) where T : IComparable<T>
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            return CompareInternal(list, 0, other, 0, length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="listOffset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, int listOffset, IList<T> other, int otherOffset, int length) where T : IComparable<T>
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            return CompareInternal(list, listOffset, other, otherOffset, length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="listOffset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static int CompareInternal<T>(IList<T> list, int listOffset, IList<T> other, int otherOffset, int length) where T : IComparable<T>
        {
            for (var i = 0; i < length; i++)
            {
                var ret = list[listOffset + i].CompareTo(other[otherOffset + i]);
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="other"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, IList<T> other, Comparison<T> comparison)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            var length = Math.Min(list.Count, other.Count);

            var ret = CompareInternal(list, 0, other, 0, length, comparison);

            return ret != 0 ? ret : list.Count - other.Count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="other"></param>
        /// <param name="length"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, IList<T> other, int length, Comparison<T> comparison)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            return CompareInternal(list, 0, other, 0, length, comparison);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="listOffset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int Compare<T>(this IList<T> list, int listOffset, IList<T> other, int otherOffset, int length, Comparison<T> comparison)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            return CompareInternal(list, listOffset, other, otherOffset, length, comparison);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="listOffset"></param>
        /// <param name="other"></param>
        /// <param name="otherOffset"></param>
        /// <param name="length"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        private static int CompareInternal<T>(IList<T> list, int listOffset, IList<T> other, int otherOffset, int length, Comparison<T> comparison)
        {
            for (var i = 0; i < length; i++)
            {
                var ret = comparison(list[listOffset + i], other[otherOffset + i]);
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <param name="descending"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Ignore")]
        public static void Sort<T, TValue>(this List<T> source, Func<T, TValue> selector, IComparer<TValue> comparer, bool descending)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (comparer == null)
            {
                comparer = Comparer<TValue>.Default;
            }

            var itemComparer = new ProjectionComparer<T, TValue>(selector, comparer);
            source.Sort(descending ? itemComparer.Reverse() : itemComparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Ignore")]
        public static void Sort<T, TValue>(this List<T> source, Func<T, TValue> selector)
        {
            Sort(source, selector, null, false);
        }
    }
}
