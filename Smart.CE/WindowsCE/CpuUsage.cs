namespace Smart.WindowsCE
{
    /// <summary>
    /// 
    /// </summary>
    public class CpuUsage
    {
        private uint prevTick;
        private uint prevIdle;

        /// <summary>
        /// 
        /// </summary>
        public CpuUsage()
        {
            prevIdle = NativeMethods.GetIdleTime();
            prevTick = NativeMethods.GetTickCount();
        }

        /// <summary>
        /// 
        /// </summary>
        public double UsageDouble
        {
            get
            {
                var idle = NativeMethods.GetIdleTime();
                var tick = NativeMethods.GetTickCount();

                if (tick == prevTick)
                {
                    return 0;
                }

                var usage = (100 * (idle - prevIdle)) / (double)(tick - prevTick);

                prevTick = tick;
                prevIdle = idle;

                return 100 - usage;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Usage
        {
            get
            {
                return (int)UsageDouble;
            }
        }
    }
}
