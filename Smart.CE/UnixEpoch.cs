namespace Smart
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public static class UnixEpoch
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToUnixTime(this DateTime value)
        {
            var elapsedTime = value.ToUniversalTime() - Epoch;
            return (int)elapsedTime.TotalSeconds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToUtc(int timestamp)
        {
            return Epoch + TimeSpan.FromSeconds(timestamp);
        }
    }
}
