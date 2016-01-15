namespace Smart.Data
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader;
            }
        }
    }
}
