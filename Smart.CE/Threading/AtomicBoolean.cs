namespace Smart.Threading
{
    using System.Threading;

    /// <summary>
    ///
    /// </summary>
    public class AtomicBoolean
    {
        private const int True = -1;
        private const int False = 0;

        private int currentValue;

        public bool Value
        {
            get
            {
                return Interlocked.Exchange(ref currentValue, currentValue) == True;
            }
            set
            {
                Interlocked.Exchange(ref currentValue, value ? True : False);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public AtomicBoolean()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="initialValue"></param>
        public AtomicBoolean(bool initialValue)
        {
            currentValue = initialValue ? True : False;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetAndSet(bool value)
        {
            return Interlocked.Exchange(ref currentValue, value ? True : False) == True;
        }
    }
}
