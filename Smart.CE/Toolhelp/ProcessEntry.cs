namespace Smart.ToolHelp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public class ProcessEntry
    {
        private const int SIZE = 564;

        private const int SIZE_OFFSET = 0;
        private const int USAGE_COUNT_OFFSET = 4;
        private const int PROCESS_ID_OFFSET = 8;
        //private const int DEFAULT_HEAP_ID_OFFSET = 12;
        //private const int MODULE_ID_OFFSET = 16;
        private const int THREAD_COUNT_OFFSET = 20;
        private const int PARENT_PROCESS_ID_OFFSET = 24;
        private const int BASE_PRIORITY_OFFSET = 28;
        //private const int FLAGS_OFFSET = 32;
        private const int EXE_FILE_OFFSET = 36;
        private const int BASE_ADDRESS_OFFSET = 556;
        //private const int ACCESS_KEY_OFFSET = 560;

        private const int MAX_PATH = 260;

        /// <summary>
        ///
        /// </summary>
        public uint UsageCount { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint ProcessId { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint ThreadCount { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint ParentProcessId { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public long BasePriority { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ExeFile { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint BaseAddress { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "Ignore")]
        public static ProcessEntry[] GetProcesses()
        {
            var handle = NativeMethods.CreateToolhelp32Snapshot(CreateToolhelp32Flags.SNAPNOHEAPS | CreateToolhelp32Flags.SNAPPROCESS, 0);
            if ((int)handle <= 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var list = new List<ProcessEntry>();

            var pe = new byte[SIZE];
            var bytes = BitConverter.GetBytes(SIZE);
            Buffer.BlockCopy(bytes, 0, pe, SIZE_OFFSET, bytes.Length);

            for (var i = NativeMethods.Process32First(handle, pe); i == 1; i = NativeMethods.Process32Next(handle, pe))
            {
                list.Add(new ProcessEntry(pe));
            }

            NativeMethods.CloseToolhelp32Snapshot(handle);

            return list.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        private ProcessEntry(byte[] data)
        {
            UsageCount = BitConverter.ToUInt32(data, USAGE_COUNT_OFFSET);
            ProcessId = BitConverter.ToUInt32(data, PROCESS_ID_OFFSET);
            ThreadCount = BitConverter.ToUInt32(data, THREAD_COUNT_OFFSET);
            ParentProcessId = BitConverter.ToUInt32(data, PARENT_PROCESS_ID_OFFSET);
            BasePriority = BitConverter.ToUInt32(data, BASE_PRIORITY_OFFSET);
            ExeFile = Encoding.Unicode.GetString(data, EXE_FILE_OFFSET, MAX_PATH).TrimEnd(new char[1]);
            BaseAddress = BitConverter.ToUInt32(data, BASE_ADDRESS_OFFSET);
        }

        /// <summary>
        ///
        /// </summary>
        public void Kill()
        {
            var hProcess = NativeMethods.OpenProcess(1, false, ProcessId);
            if (hProcess.ToInt32() != -1)
            {
                NativeMethods.TerminateProcess(hProcess, 0);
                NativeMethods.CloseHandle(hProcess);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ThreadEntry[] GetThreads()
        {
            return ThreadEntry.GetThreads(ProcessId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ModuleEntry[] GetModules()
        {
            return ModuleEntry.GetModules(ProcessId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public uint CalcHeapUsage()
        {
            return Heap.CalcHeapUsage(ProcessId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path.GetFileName(ExeFile);
        }
    }
}
