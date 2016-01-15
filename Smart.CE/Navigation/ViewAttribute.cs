namespace Smart.Navigation
{
    using System;

    /// <summary>
    /// ビュー属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ViewAttribute : Attribute
    {
        /// <summary>
        /// ビューID
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        /// ドメイン
        /// </summary>
        public object Domain { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ビューID</param>
        public ViewAttribute(object id)
        {
            Id = id;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ビューID</param>
        /// <param name="domain">ドメイン</param>
        public ViewAttribute(object id, object domain)
        {
            Id = id;
            Domain = domain;
        }
    }
}