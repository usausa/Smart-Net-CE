namespace Smart.Navigation.Plugins.Parameter
{
    using System;

    /// <summary>
    /// インポート/エクスポート方向
    /// </summary>
    [Flags]
    public enum Direction
    {
        /// <summary>
        /// インポート
        /// </summary>
        Import = 0x00000001,

        /// <summary>
        /// エクスポート
        /// </summary>
        Export = 0x00000002,

        /// <summary>
        /// 双方向
        /// </summary>
        Both = Import | Export,
    }

    /// <summary>
    /// ビューパラメータ属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ViewParameterAttribute : Attribute
    {
        /// <summary>
        /// インポート/エクスポート方向
        /// </summary>
        public Direction Direction { get; private set; }

        /// <summary>
        /// パラメータ名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewParameterAttribute()
            : this(Direction.Both)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">パラメータ名称</param>
        public ViewParameterAttribute(string name)
            : this(Direction.Both, name)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="direction">インポート/エクスポート方向</param>
        public ViewParameterAttribute(Direction direction)
        {
            Direction = direction;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="direction">インポート/エクスポート方向</param>
        /// <param name="name">パラメータ名称</param>
        public ViewParameterAttribute(Direction direction, string name)
        {
            Direction = direction;
            Name = name;
        }
    }
}