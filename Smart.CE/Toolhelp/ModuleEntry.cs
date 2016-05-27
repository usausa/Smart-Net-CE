namespace Smart.ToolHelp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public class ModuleEntry
    {
        private const int SIZE = 1076;

        private const int SIZE_OFFSET = 0;
        private const int MODULE_ID_OFFSET = 4;
        //private const int PROCESS_ID_OFFSET = 8;
        private const int GLOBAL_COUNT_OFFSET = 12;
        private const int PROCESS_COUNT_OFFSET = 16;
        private const int BASE_ADDRESS_OFFSET = 20;
        private const int BASE_SIZE_OFFSET = 24;
        //private const int HMODULE_OFFSET = 28;
        private const int MODULE_OFFSET = 32;
        private const int EXE_PATH_OFFSET = 552;
        //private const int FLAGS_OFFSET = 1072;

        /// <summary>
        ///
        /// </summary>
        public uint ModuleId { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint GlobalCount { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint ProcessCount { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint BaseAddress { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint BaseSize { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Module { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string ExePath { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        public static ModuleEntry[] GetModules(uint processId)
        {
            var handle = NativeMethods.CreateToolhelp32Snapshot(CreateToolhelp32Flags.SNAPMODULE, processId);
            if ((int)handle <= 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var list = new List<ModuleEntry>();

            var me = new byte[SIZE];
            var bytes = BitConverter.GetBytes(SIZE);
            Buffer.BlockCopy(bytes, 0, me, SIZE_OFFSET, bytes.Length);

            for (var i = NativeMethods.Module32First(handle, me); i == 1; i = NativeMethods.Module32Next(handle, me))
            {
                list.Add(new ModuleEntry(me));
            }

            NativeMethods.CloseToolhelp32Snapshot(handle);

            return list.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        private ModuleEntry(byte[] data)
        {
            ModuleId = BitConverter.ToUInt32(data, MODULE_ID_OFFSET);
            GlobalCount = BitConverter.ToUInt32(data, GLOBAL_COUNT_OFFSET);
            ProcessCount = BitConverter.ToUInt32(data, PROCESS_COUNT_OFFSET);
            BaseAddress = BitConverter.ToUInt32(data, BASE_ADDRESS_OFFSET);
            BaseSize = BitConverter.ToUInt32(data, BASE_SIZE_OFFSET);
            Module = Encoding.Unicode.GetString(data, MODULE_OFFSET, 520).TrimEnd(new char[1]);
            ExePath = Encoding.Unicode.GetString(data, EXE_PATH_OFFSET, 520).TrimEnd(new char[1]);
        }
    }
}
