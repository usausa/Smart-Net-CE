namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class Comparers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparers"></param>
        /// <returns></returns>
        public static ChainComparers<T> Chain<T>(params IComparer<T>[] comparers)
        {
            return new ChainComparers<T>(comparers);
        }

            /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparison"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "Ignore")]
        public static ComparisonComparer<T> Comparison<T>(Comparison<T> comparison)
        {
            return new ComparisonComparer<T>(comparison);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DelegateComparer<T> Delegate<T>(Func<T, T, int> comparer)
        {
            return new DelegateComparer<T>(comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="equals"></param>
        /// <param name="getHashCode"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> DelegateEquality<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            return new DelegateEqualityComparer<T>(equals, getHashCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static ProjectionComparer<TSource, TKey> Projection<TSource, TKey>(Func<TSource, TKey> keySelector)
        {
            return new ProjectionComparer<TSource, TKey>(keySelector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static ProjectionEqualityComparer<TSource, TKey> ProjectionEquality<TSource, TKey>(Func<TSource, TKey> keySelector)
        {
            return new ProjectionEqualityComparer<TSource, TKey>(keySelector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static IComparer<T> Reverse<T>(this IComparer<T> original)
        {
            var originalAsReverse = original as ReverseComparer<T>;
            if (originalAsReverse != null)
            {
                return originalAsReverse.OriginalComparer;
            }

            return new ReverseComparer<T>(original);
        }
    }
}
