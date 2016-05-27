namespace Smart
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class BetweenExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this byte value, byte minValue, byte maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this short value, short minValue, short maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this int value, int minValue, int maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this long value, long minValue, long maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this float value, float minValue, float maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this double value, double minValue, double maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(this char value, char minValue, char maxValue)
        {
            return minValue < maxValue
                ? value >= minValue && value <= maxValue
                : value >= maxValue && value <= minValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return IsBetween(value, minValue, maxValue, Comparer<T>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, IComparer<T> comparer) where T : IComparable<T>
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            return (comparer.Compare(minValue, maxValue) < 0)
                       ? ((comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0))
                       : ((comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0));
        }
    }
}
