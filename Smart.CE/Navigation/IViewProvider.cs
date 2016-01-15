namespace Smart.Navigation
{
    using System;

    /// <summary>
    /// ビュープロバイダー
    /// </summary>
    public interface IViewProvider
    {
        /// <summary>
        /// 非同期
        /// </summary>
        bool IsAsync { get; }

        /// <summary>
        /// ターゲット解決
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <returns>ターゲット</returns>
        object ResolveEventTarget(object view);

        /// <summary>
        /// 非同期処理実行
        /// </summary>
        /// <param name="method">メソッド</param>
        /// <param name="args">引数</param>
        void BeginInvoke(Delegate method, params object[] args);

        /// <summary>
        /// ビュー作成
        /// </summary>
        /// <param name="type">型</param>
        /// <returns>ビューインスタンス</returns>
        object ViewCreate(Type type);

        /// <summary>
        /// ビュー活性化
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <param name="parameter">復帰情報</param>
        void ViewActive(object view, object parameter);

        /// <summary>
        /// ビュー非活性化
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <returns>復帰情報</returns>
        object ViewDeactive(object view);

        /// <summary>
        /// ビュー破棄
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        void ViewDispose(object view);
    }
}
