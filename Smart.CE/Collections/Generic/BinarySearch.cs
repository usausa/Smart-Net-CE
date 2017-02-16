namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class BinarySearch
    {
        //--------------------------------------------------------------------------------
        // Func version
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
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

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T>(IList<T> list, Func<T, int> comparer)
        {
            return FindFirst(list, 0, list.Count, comparer);
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T>(IList<T> list, int index, int length, Func<T, int> comparer)
        {
            var find = -1;
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer(list[mid]);

                if (c == 0)
                {
                    find = mid;
                    hi = mid - 1;
                }
                else if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return find >= 0 ? find : ~lo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T>(IList<T> list, Func<T, int> comparer)
        {
            return FindLast(list, 0, list.Count, comparer);
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T>(IList<T> list, int index, int length, Func<T, int> comparer)
        {
            var find = -1;
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer(list[mid]);

                if (c == 0)
                {
                    find = mid;
                    lo = mid + 1;
                }
                else if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return find >= 0 ? find : ~lo;
        }

        //--------------------------------------------------------------------------------
        // IComparer version Find
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int Find<T>(IList<T> list, T key)
        {
            return Find(list, 0, list.Count, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int Find<T>(IList<T> list, T key, IComparer<T> comparer)
        {
            return Find(list, 0, list.Count, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int Find<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector)
        {
            return Find(list, 0, list.Count, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int Find<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            return Find(list, 0, list.Count, key, selector, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int Find<T>(IList<T> list, int index, int length, T key)
        {
            return Find(list, index, length, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int Find<T>(IList<T> list, int index, int length, T key, IComparer<T> comparer)
        {
            return Find(list, index, length, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static int Find<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector)
        {
            return Find(list, index, length, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int Find<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer.Compare(selector(list[mid]), key);

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

        //--------------------------------------------------------------------------------
        // IComparer version FindFirst
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T>(IList<T> list, T key)
        {
            return FindFirst(list, 0, list.Count, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T>(IList<T> list, T key, IComparer<T> comparer)
        {
            return FindFirst(list, 0, list.Count, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector)
        {
            return FindFirst(list, 0, list.Count, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            return FindFirst(list, 0, list.Count, key, selector, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int FindFirst<T>(IList<T> list, int index, int length, T key)
        {
            return FindFirst(list, index, length, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int FindFirst<T>(IList<T> list, int index, int length, T key, IComparer<T> comparer)
        {
            return FindFirst(list, index, length, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static int FindFirst<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector)
        {
            return FindFirst(list, index, length, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindFirst<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            var find = -1;
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer.Compare(selector(list[mid]), key);

                if (c == 0)
                {
                    find = mid;
                    hi = mid - 1;
                }
                else if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return find >= 0 ? find : ~lo;
        }

        //--------------------------------------------------------------------------------
        // IComparer version FindLast
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T>(IList<T> list, T key)
        {
            return FindLast(list, 0, list.Count, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T>(IList<T> list, T key, IComparer<T> comparer)
        {
            return FindLast(list, 0, list.Count, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector)
        {
            return FindLast(list, 0, list.Count, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T, TKey>(IList<T> list, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            return FindLast(list, 0, list.Count, key, selector, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int FindLast<T>(IList<T> list, int index, int length, T key)
        {
            return FindLast(list, index, length, key, Functions<T>.Identify, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int FindLast<T>(IList<T> list, int index, int length, T key, IComparer<T> comparer)
        {
            return FindLast(list, index, length, key, Functions<T>.Identify, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static int FindLast<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector)
        {
            return FindLast(list, index, length, key, selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public static int FindLast<T, TKey>(IList<T> list, int index, int length, TKey key, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            var find = -1;
            var lo = index;
            var hi = index + length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer.Compare(selector(list[mid]), key);

                if (c == 0)
                {
                    find = mid;
                    lo = mid + 1;
                }
                else if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return find >= 0 ? find : ~lo;
        }
    }
}
