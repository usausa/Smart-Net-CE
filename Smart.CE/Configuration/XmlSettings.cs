namespace Smart.Configuration
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// 
    /// </summary>
    public static class XmlSettings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T Load<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return default(T);
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new XmlTextReader(fileName))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T Load<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new XmlTextReader(stream))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public static void Save(string filename, object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        public static void Save(Stream stream, object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, data);
            }
        }
    }
}
