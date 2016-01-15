namespace Smart
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class Inflector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Pascalize(string word)
        {
            return Camelize(word, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Camelize(string word)
        {
            return Camelize(word, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public static string Camelize(string word, bool isUpper)
        {
            if (String.IsNullOrEmpty(word))
            {
                return word;
            }

            var sb = new StringBuilder();
            foreach (var c in word)
            {
                if (c == '_')
                {
                    isUpper = true;
                }
                else
                {
                    if (isUpper)
                    {
                        sb.Append(Char.ToUpper(c, CultureInfo.InvariantCulture));
                        isUpper = false;
                    }
                    else
                    {
                        sb.Append(Char.ToLower(c, CultureInfo.InvariantCulture));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Underscore(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                return word;
            }

            var sb = new StringBuilder();
            foreach (var c in word)
            {
                if (Char.IsUpper(c) && (sb.Length > 0))
                {
                    sb.Append("_");
                    sb.Append(Char.ToLower(c, CultureInfo.InvariantCulture));
                }
                else
                {
                    sb.Append(Char.ToLower(c, CultureInfo.InvariantCulture));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Pluralize(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                return word;
            }
            if (word[word.Length - 1] == 's')
            {
                return word;
            }
            if (word[word.Length - 1] != 'y')
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}s", word);
            }
            return string.Format(CultureInfo.InvariantCulture, "{0}ies", word.Substring(0, word.Length - 1));
        }
    }
}
