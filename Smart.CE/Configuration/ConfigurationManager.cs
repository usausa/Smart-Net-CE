namespace Smart.Configuration
{
    using System;
    using System.Globalization;

    using Smart.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Configuration OpenConfiguration(string filePath)
        {
            return new Configuration(filePath);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Configuration OpenExeConfiguration()
        {
            return new Configuration(String.Format(CultureInfo.InvariantCulture, "{0}.config", ApplicationEx.ExecutablePath));
        }
    }
}
