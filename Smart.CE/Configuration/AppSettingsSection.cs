namespace Smart.Configuration
{
    using System.Collections.Specialized;

    /// <summary>
    /// 
    /// </summary>
    public class AppSettingsSection
    {
        private NameValueCollection settings;

        /// <summary>
        /// 
        /// </summary>
        public NameValueCollection Settings
        {
            get { return settings ?? (settings = new NameValueCollection()); }
        }
    }
}
