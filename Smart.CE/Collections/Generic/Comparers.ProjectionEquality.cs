namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class ProjectionEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
    {
        private readonly Func<TSource, TKey> keySelector;

        private readonly IEqualityComparer<TKey> comparer;

        /// <summary>
        ///
        /// </summary>
        /// <param name="keySelector"></param>
        public ProjectionEqualityComparer(Func<TSource, TKey> keySelector)
            : this(keySelector, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        public ProjectionEqualityComparer(Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            this.comparer = comparer ?? EqualityComparer<TKey>.Default;
            this.keySelector = keySelector;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(TSource x, TSource y)
        {
            if (Object.Equals(x, default(TSource)) && Object.Equals(y, default(TSource)))
            {
                return true;
            }
            if (Object.Equals(x, default(TSource)) || Object.Equals(y, default(TSource)))
            {
                return false;
            }
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(TSource obj)
        {
            if (Object.Equals(obj, default(TSource)))
            {
                throw new ArgumentNullException("obj");
            }
            return comparer.GetHashCode(keySelector(obj));
        }
    }
}
