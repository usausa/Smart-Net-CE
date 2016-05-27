namespace Smart.Collections.Generic
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChainComparers<T> : IComparer<T>
    {
        private readonly IComparer<T>[] comparers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="comparers"></param>
        public ChainComparers(params IComparer<T>[] comparers)
        {
            this.comparers = comparers;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            return comparers.Select(comparer => comparer.Compare(x, y)).FirstOrDefault(ret => ret != 0);
        }
    }
}
