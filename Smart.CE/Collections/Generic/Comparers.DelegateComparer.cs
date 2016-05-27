namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> comparer;

        /// <summary>
        ///
        /// </summary>
        /// <param name="comparer"></param>
        public DelegateComparer(Func<T, T, int> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            return comparer(x, y);
        }
    }
}
