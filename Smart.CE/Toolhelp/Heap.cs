namespace Smart.ToolHelp
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    public static class Heap
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static uint CalcHeapUsage(uint processId)
        {
            uint usage = 0;

            var hHeapSnapshot = NativeMethods.CreateToolhelp32Snapshot(CreateToolhelp32Flags.SNAPHEAPLIST, processId);
            if (hHeapSnapshot != (IntPtr)(-1))
            {
                var heaplist = new HEAPLIST32();
                heaplist.Size = (uint)Marshal.SizeOf(heaplist);

                if (NativeMethods.Heap32ListFirst(hHeapSnapshot, ref heaplist))
                {
                    do
                    {
                        var heapentry = new HEAPENTRY32();
                        heapentry.Size = (uint)Marshal.SizeOf(heapentry);

                        if (NativeMethods.Heap32First(hHeapSnapshot, ref heapentry, heaplist.ProcessId, heaplist.HeapId))
                        {
                            do
                            {
                                usage += heapentry.BlockSize;
                            } while (NativeMethods.Heap32Next(hHeapSnapshot, ref heapentry));
                        }
                    } while (NativeMethods.Heap32ListNext(hHeapSnapshot, ref heaplist));
                }

                NativeMethods.CloseToolhelp32Snapshot(hHeapSnapshot);
            }

            return usage;
        }
    }
}
