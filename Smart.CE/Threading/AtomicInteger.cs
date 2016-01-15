namespace Smart.Threading
{
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class AtomicInteger
    {
        private int currentValue;

        public int Value
        {
            get
            {
                return Interlocked.Exchange(ref currentValue, currentValue);
            }
            set
            {
                Interlocked.Exchange(ref currentValue, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AtomicInteger()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue"></param>
        public AtomicInteger(int initialValue)
        {
            currentValue = initialValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetAndSet(int value)
        {
            return Interlocked.Exchange(ref currentValue, value);
        }
    }
}
