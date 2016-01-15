namespace Smart.Navigation.Plugins.Context
{
    /// <summary>
    /// コンテキストライフサイクルサポート
    /// </summary>
    public interface IViewContextSupport
    {
        /// <summary>
        /// コンテキスト初期化
        /// </summary>
        void Initilize();

        /// <summary>
        /// コンテキスト破棄
        /// </summary>
        void Dispose();
    }
}