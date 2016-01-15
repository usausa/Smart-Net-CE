namespace Smart.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// プラグインコンテキスト
    /// </summary>
    public class ForwadingPluginContext
    {
        private readonly Dictionary<Type, object> store = new Dictionary<Type, object>();

        /// <summary>
        /// データ保存
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="type">型</param>
        /// <param name="data">データ</param>
        public void Save<T>(Type type, T data)
        {
            store[type] = data;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="type">型</param>
        /// <returns>データ</returns>
        public T Load<T>(Type type)
        {
            object data;
            if (store.TryGetValue(type, out data))
            {
                return (T)data;
            }

            return default(T);
        }
    }
}