namespace Smart.WindowsCE
{
    /// <summary>
    /// 
    /// </summary>
    public static class MemoryManager
    {
        private static readonly int MemoryPageSize;

        /// <summary>
        /// 
        /// </summary>
        static MemoryManager()
        {
            var lpdwStorePages = 0;
            var lpdwRamPages = 0;
            NativeMethods.GetSystemMemoryDivision(ref lpdwStorePages, ref lpdwRamPages, ref MemoryPageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        public static StoreInformation StoreInformation
        {
            get
            {
                StoreInformation storeInformation;
                NativeMethods.GetStoreInformation(out storeInformation);
                return storeInformation;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static MemoryStatus MemoryStatus
        {
            get
            {
                MemoryStatus memoryStatus;
                NativeMethods.GlobalMemoryStatus(out memoryStatus);
                return memoryStatus;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static SystemMemoryDivision SystemMemoryDivision
        {
            get
            {
                var division = new SystemMemoryDivision();
                var lpdwStorePages = 0;
                var lpdwRamPages = 0;
                var lpdwPageSize = 0;
                NativeMethods.GetSystemMemoryDivision(ref lpdwStorePages, ref lpdwRamPages, ref lpdwPageSize);
                division.ProgramMemory = lpdwRamPages * (lpdwPageSize >> 10);
                division.StorageMemory = lpdwStorePages * (lpdwPageSize >> 10);
                return division;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int StorageMemory
        {
            get
            {
                return SystemMemoryDivision.StorageMemory;
            }
            set
            {
                NativeMethods.SetSystemMemoryDivision((value << 10) / MemoryPageSize);
            }
        }
    }
}
