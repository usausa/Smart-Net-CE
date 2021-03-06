﻿namespace Smart.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static TValue GetOr<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static TValue GetOr<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException("valueFactory");
            }

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : valueFactory();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            TValue ret;
            if (dictionary.TryGetValue(key, out ret))
            {
                return ret;
            }

            ret = value;
            dictionary[key] = ret;

            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException("valueFactory");
            }

            TValue ret;
            if (dictionary.TryGetValue(key, out ret))
            {
                return ret;
            }

            ret = valueFactory();
            dictionary[key] = ret;

            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void CopyTo<TKey, TValue>(this IDictionary<TKey, TValue> src, IDictionary<TKey, TValue> dst)
        {
            CopyTo(src, dst, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="replace"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void CopyTo<TKey, TValue>(this IDictionary<TKey, TValue> src, IDictionary<TKey, TValue> dst, bool replace)
        {
            if (dst == null)
            {
                throw new ArgumentNullException("dst");
            }

            foreach (var key in src.Keys)
            {
                if (replace || !dst.ContainsKey(key))
                {
                    dst[key] = src[key];
                }
            }
        }
    }
}
