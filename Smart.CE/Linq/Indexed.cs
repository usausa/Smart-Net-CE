namespace Smart.Linq
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Indexed<T>
    {
        public T Item { get; private set; }

        public int Index { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public Indexed(T item, int index)
        {
            Item = item;
            Index = index;
        }
    }
}
