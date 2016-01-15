namespace Smart.Navigation.Plugins
{
    /// <summary>
    /// プラグイン
    /// </summary>
    public interface INavigatorPlugin
    {
        /// <summary>
        /// ビュー作成イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        void OnCreate(object view);

        /// <summary>
        /// ビュー破棄イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        void OnDispose(object view);

        /// <summary>
        /// 遷移元ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移元ビュー</param>
        void OnNavigateFrom(ForwadingPluginContext context, object view);

        /// <summary>
        /// 遷移先ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移先ビュー</param>
        void OnNavigateTo(ForwadingPluginContext context, object view);
    }
}