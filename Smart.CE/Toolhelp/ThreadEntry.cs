namespace Smart.ToolHelp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class ThreadEntry
    {
        private const int SIZE = 44;

        private const int SIZE_OFFSET = 0;
        private const int USAGE_COUNT_OFFSET = 4;
        private const int THREAD_ID_OFFSET = 8;
        private const int OWNER_PROCESS_ID_OFFSET = 12;
        private const int BASE_PRIORITY_OFFSET = 16;
        private const int DELTA_PRIORITY_OFFSET = 24;
        //private const int FLAGS_OFFSET = 32;
        //private const int ACCESS_KEY_OFFSET = 36;
        private const int CURRENT_PROCESS_ID_OFFSET = 40;

        /// <summary>
        /// 
        /// </summary>
        public uint UsageCount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint ThreadId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint OwnerProcessId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int BasePriority { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int DeltaPriority { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint CurrentProcessId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ThreadPriority Priority
        {
            get
            {
                return (ThreadPriority)NativeMethods.GetThreadPriority(ThreadId);
            }
            set
            {
                NativeMethods.SetThreadPriority(ThreadId, (int)value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Quantum
        {
            get
            {
                return NativeMethods.GetThreadQuantum(ThreadId);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                NativeMethods.SetThreadQuantum(ThreadId, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ThreadEntry[] GetThreads()
        {
            return GetThreads(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        public static ThreadEntry[] GetThreads(uint processId)
        {
            var handle = NativeMethods.CreateToolhelp32Snapshot(CreateToolhelp32Flags.SNAPTHREAD, processId);
            if ((int)handle <= 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var list = new List<ThreadEntry>();

            var te = new byte[SIZE];
            var bytes = BitConverter.GetBytes(SIZE);
            Buffer.BlockCopy(bytes, 0, te, SIZE_OFFSET, bytes.Length);

            for (var i = NativeMethods.Thread32First(handle, te); i == 1; i = NativeMethods.Thread32Next(handle, te))
            {
                var entry = new ThreadEntry(te);
                if ((processId == 0) || (entry.OwnerProcessId == processId))
                {
                    list.Add(entry);
                }
            }

            NativeMethods.CloseToolhelp32Snapshot(handle);

            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private ThreadEntry(byte[] data)
        {
            UsageCount = BitConverter.ToUInt32(data, USAGE_COUNT_OFFSET);
            ThreadId = BitConverter.ToUInt32(data, THREAD_ID_OFFSET);
            OwnerProcessId = BitConverter.ToUInt32(data, OWNER_PROCESS_ID_OFFSET);
            BasePriority = BitConverter.ToInt32(data, BASE_PRIORITY_OFFSET);
            DeltaPriority = BitConverter.ToInt32(data, DELTA_PRIORITY_OFFSET);
            CurrentProcessId = BitConverter.ToUInt32(data, CURRENT_PROCESS_ID_OFFSET);
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "Ignore")]
        public void Suspend()
        {
            NativeMethods.SuspendThread(ThreadId);
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "Ignore")]
        public void Resume()
        {
            NativeMethods.ResumeThread(ThreadId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Terminate()
        {
            return NativeMethods.TerminateThread(ThreadId, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ThreadId.ToString(CultureInfo.InvariantCulture);
        }
    }
}
