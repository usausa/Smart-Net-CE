namespace Smart.Configuration
{
    using System.IO;
    using System.Linq;
    using System.Xml;

    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Compatibility")]
    public class Configuration
    {
        public AppSettingsSection AppSettings { get; private set; }

        public string FilePath { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath"></param>
        internal Configuration(string filePath)
        {
            FilePath = filePath;
            AppSettings = new AppSettingsSection();

            Load();
        }

        /// <summary>
        ///
        /// </summary>
        private void Load()
        {
            AppSettings.Settings.Clear();

            if (!File.Exists(FilePath))
            {
                return;
            }

            var doc = new XmlDocument();
            doc.Load(FilePath);

            var appSetting = doc.GetElementsByTagName("appSettings").Cast<XmlNode>().FirstOrDefault();
            if (appSetting != null)
            {
                foreach (XmlNode entry in appSetting.ChildNodes)
                {
                    if (entry.Attributes != null)
                    {
                        AppSettings.Settings[entry.Attributes["key"].Value] = entry.Attributes["value"].Value;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Save()
        {
            var doc = new XmlDocument();

            if (File.Exists(FilePath))
            {
                doc.Load(FilePath);
            }

            var configuration = doc.ChildNodes.Cast<XmlNode>().FirstOrDefault(_ => _.Name == "configuration");
            if (configuration == null)
            {
                configuration = doc.CreateElement("configuration");
                doc.AppendChild(configuration);
            }

            var appSetting = configuration.ChildNodes.Cast<XmlNode>().FirstOrDefault(_ => _.Name == "appSettings");
            if (appSetting == null)
            {
                appSetting = doc.CreateElement("appSettings");
                configuration.AppendChild(appSetting);
            }
            else
            {
                for (var i = appSetting.ChildNodes.Count - 1; i >= 0; i--)
                {
                    var entry = appSetting.ChildNodes[i];
                    appSetting.RemoveChild(entry);
                }
            }

            foreach (var key in AppSettings.Settings.AllKeys)
            {
                var entry = doc.CreateElement("add");
                entry.SetAttribute("key", string.Empty, key);
                entry.SetAttribute("value", string.Empty, AppSettings.Settings[key]);

                appSetting.AppendChild(entry);
            }

            doc.Save(FilePath);
        }
    }
}
