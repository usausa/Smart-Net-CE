namespace Smart.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ReverseComparer<T> : IComparer<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public IComparer<T> OriginalComparer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        public ReverseComparer(IComparer<T> original)
        {
            OriginalComparer = original;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            return OriginalComparer.Compare(y, x);
        }
    }
}
