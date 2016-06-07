namespace Smart.Collections.Specialized
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static string GetString(this NameValueCollection collection, string key)
        {
            return collection[key];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static string GetStringOr(this NameValueCollection collection, string key, string value)
        {
            var str = collection[key];
            return String.IsNullOrEmpty(str) ? value : str;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static string GetStringOr(this NameValueCollection collection, string key, Func<string> valueFactory)
        {
            var str = collection[key];
            return String.IsNullOrEmpty(str) ? valueFactory() : str;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static string GetStringOrAdd(this NameValueCollection collection, string key, string value)
        {
            var str = collection[key];
            if (String.IsNullOrEmpty(str))
            {
                collection[key] = value;
                return value;
            }
            return str;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static string GetStringOrAdd(this NameValueCollection collection, string key, Func<string> valueFactory)
        {
            var str = collection[key];
            if (String.IsNullOrEmpty(str))
            {
                var value = valueFactory();
                collection[key] = value;
                return value;
            }
            return str;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetString(this NameValueCollection collection, string key, string value)
        {
            collection[key] = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetBool(this NameValueCollection collection, string key)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return false;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetBoolOr(this NameValueCollection collection, string key, bool value)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetBoolOr(this NameValueCollection collection, string key, Func<bool> valueFactory)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return valueFactory();
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetBoolOrAdd(this NameValueCollection collection, string key, bool value)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                collection[key] = value.ToString(CultureInfo.InvariantCulture);
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetBoolOrAdd(this NameValueCollection collection, string key, Func<bool> valueFactory)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                var value = valueFactory();
                collection[key] = value.ToString(CultureInfo.InvariantCulture);
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetBool(this NameValueCollection collection, string key, bool value)
        {
            collection[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool? GetBoolNullable(this NameValueCollection collection, string key)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool? GetBoolNullableOr(this NameValueCollection collection, string key, bool? value)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool? GetBoolNullableOr(this NameValueCollection collection, string key, Func<bool?> valueFactory)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return valueFactory();
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool? GetBoolNullableOrAdd(this NameValueCollection collection, string key, bool? value)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool? GetBoolNullableOrAdd(this NameValueCollection collection, string key, Func<bool?> valueFactory)
        {
            var str = collection[key];
            bool ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                var value = valueFactory();
                collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetBoolNullable(this NameValueCollection collection, string key, bool? value)
        {
            collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int GetInteger(this NameValueCollection collection, string key)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return 0;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int GetIntegerOr(this NameValueCollection collection, string key, int value)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int GetIntegerOr(this NameValueCollection collection, string key, Func<int> valueFactory)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return valueFactory();
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int GetIntegerOrAdd(this NameValueCollection collection, string key, int value)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                collection[key] = value.ToString(CultureInfo.InvariantCulture);
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int GetIntegerOrAdd(this NameValueCollection collection, string key, Func<int> valueFactory)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                var value = valueFactory();
                collection[key] = value.ToString(CultureInfo.InvariantCulture);
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetInteger(this NameValueCollection collection, string key, int value)
        {
            collection[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int? GetIntegerNullable(this NameValueCollection collection, string key)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int? GetIntegerNullableOr(this NameValueCollection collection, string key, int? value)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int? GetIntegerNullableOr(this NameValueCollection collection, string key, Func<int?> valueFactory)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                return valueFactory();
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int? GetIntegerNullableOrAdd(this NameValueCollection collection, string key, int? value)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static int? GetIntegerNullableOrAdd(this NameValueCollection collection, string key, Func<int?> valueFactory)
        {
            var str = collection[key];
            int ret;
#if WindowsCE
            if (String.IsNullOrEmpty(str) || !TryParse(str, out ret))
#else
            if (String.IsNullOrEmpty(str) || !Boolean.TryParse(str, out ret))
#endif
            {
                var value = valueFactory();
                collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                return value;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetIntegerNullable(this NameValueCollection collection, string key, int? value)
        {
            collection[key] = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

#if WindowsCE
        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryParse(string s, out bool result)
        {
            try
            {
                result = Boolean.Parse(s);
                return true;
            }
            catch (FormatException)
            {
                result = false;
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryParse(string s, out int result)
        {
            try
            {
                result = Int32.Parse(s, CultureInfo.CurrentCulture);
                return true;
            }
            catch (FormatException)
            {
                result = 0;
                return false;
            }
        }
#endif
    }
}
