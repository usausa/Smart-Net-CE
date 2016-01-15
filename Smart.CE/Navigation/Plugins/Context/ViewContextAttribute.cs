namespace Smart.Navigation.Plugins.Context
{
    using System;

    /// <summary>
    /// ビューコンテキスト属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ViewContextAttribute : Attribute
    {
        /// <summary>
        /// コンテキストキー
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// コンテキスト型
        /// </summary>
        public Type Context { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewContextAttribute()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">コンテキストキー</param>
        public ViewContextAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">コンテキスト型</param>
        public ViewContextAttribute(Type context)
        {
            Context = context;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">コンテキストキー</param>
        /// <param name="context">コンテキスト型</param>
        public ViewContextAttribute(string key, Type context)
        {
            Key = key;
            Context = context;
        }
    }
}