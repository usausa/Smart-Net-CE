namespace Smart.WindowsCE
{
    using System.Collections.Generic;

    using Microsoft.Win32;

    /// <summary>
    /// 
    /// </summary>
    public class DeviceInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Module { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="module"></param>
        private DeviceInformation(string deviceId, string name, string key, string module)
        {
            DeviceId = deviceId;
            Name = name;
            Key = key;
            Module = module;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DeviceInformation[] GetActiveDvices()
        {
            using (var registryKey = Registry.LocalMachine.OpenSubKey("Drivers\\Active"))
            {
                if (registryKey == null)
                {
                    return new DeviceInformation[0];
                }

                var list = new List<DeviceInformation>();

                var subKeys = registryKey.GetSubKeyNames();
                foreach (var subKey in subKeys)
                {
                    using (var keyId = registryKey.OpenSubKey(subKey))
                    {
                        if (keyId == null)
                        {
                            continue;
                        }

                        var key = keyId.GetValue("Key").ToString();
                        var name = keyId.GetValue("Name");
                        using (var keyDriver = Registry.LocalMachine.OpenSubKey(key))
                        {
                            if (keyDriver == null)
                            {
                                continue;
                            }

                            var dll = keyDriver.GetValue("Dll").ToString();
                            list.Add(new DeviceInformation(subKey, name == null ? string.Empty : name.ToString(), key, dll));
                        }
                    }
                }

                return list.ToArray();
            }
        }
    }
}
