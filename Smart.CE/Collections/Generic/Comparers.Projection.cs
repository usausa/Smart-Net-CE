namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class ProjectionComparer<TSource, TKey> : IComparer<TSource>
    {
        private readonly Func<TSource, TKey> keySelector;

        private readonly IComparer<TKey> comparer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        public ProjectionComparer(Func<TSource, TKey> keySelector)
            : this(keySelector, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        public ProjectionComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            this.comparer = comparer ?? Comparer<TKey>.Default;
            this.keySelector = keySelector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(TSource x, TSource y)
        {
            if (Equals(x, default(TSource)) && Equals(y, default(TSource)))
            {
                return 0;
            }
            if (Equals(x, default(TSource)))
            {
                return -1;
            }
            if (Equals(y, default(TSource)))
            {
                return 1;
            }
            return comparer.Compare(keySelector(x), keySelector(y));
        }
    }
}
