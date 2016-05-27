namespace Smart.Win32
{
    using System;

    using Microsoft.Win32;

    /// <summary>
    ///
    /// </summary>
    public static class RegistryKeyExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keyHive"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void WriteToRegistry(this RegistryKey keyHive, string path, string name, object value)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var strings = path.Split('\\');
            var keys = new RegistryKey[strings.Length + 1];
            keys[0] = keyHive;

            try
            {
                for (var index = 0; index < strings.Length; index++)
                {
                    keys[index + 1] = keys[index].OpenSubKey(strings[index], true);

                    if (keys[index + 1] == null)
                    {
                        keys[index + 1] = keys[index].CreateSubKey(strings[index]);
                    }
                }

                keys[strings.Length].SetValue(name, value);
            }
            finally
            {
                for (var index = 1; index < keys.Length; index++)
                {
                    keys[index].Close();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyHive"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static object ReadFromRegistry(this RegistryKey keyHive, string path, string name, object defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var strings = path.Split('\\');
            var keys = new RegistryKey[strings.Length + 1];
            keys[0] = keyHive;

            try
            {
                for (var index = 0; index < strings.Length; index++)
                {
                    keys[index + 1] = keys[index].OpenSubKey(strings[index]);
                    if (keys[index + 1] == null)
                    {
                        return null;
                    }
                }

                return keys[keys.Length - 1].GetValue(name, defaultValue);
            }
            finally
            {
                for (var index = 1; index < keys.Length; index++)
                {
                    if (keys[index] != null)
                    {
                        keys[index].Close();
                    }
                }
            }
        }
    }
}