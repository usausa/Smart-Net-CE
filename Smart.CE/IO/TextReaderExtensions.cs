namespace Smart.IO
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///
    /// </summary>
    public static class TextReaderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            string result;
            while ((result = reader.ReadLine()) != null)
            {
                yield return result;
            }
        }
    }
}
