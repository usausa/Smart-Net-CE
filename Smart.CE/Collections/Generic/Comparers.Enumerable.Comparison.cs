namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public static partial class ComparersEnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Comparison<TKey> comparison)
        {
            return source.OrderBy(keySelector, Comparers.Comparison(comparison));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Comparison<TKey> comparison)
        {
            return source.OrderByDescending(keySelector, Comparers.Comparison(comparison));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, Comparison<TKey> comparison)
        {
            return source.ThenBy(keySelector, Comparers.Comparison(comparison));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, Comparison<TKey> comparison)
        {
            return source.ThenByDescending(keySelector, Comparers.Comparison(comparison));
        }
    }
}
