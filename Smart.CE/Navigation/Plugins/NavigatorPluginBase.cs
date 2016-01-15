namespace Smart.Navigation.Plugins
{
    /// <summary>
    /// プラグインベース
    /// </summary>
    public abstract class NavigatorPluginBase : INavigatorPlugin
    {
        /// <summary>
        /// ビュー作成イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        public virtual void OnCreate(object view)
        {
        }

        /// <summary>
        /// ビュー破棄イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        public virtual void OnDispose(object view)
        {
        }

        /// <summary>
        /// 遷移元ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移元ビュー</param>
        public virtual void OnNavigateFrom(ForwadingPluginContext context, object view)
        {
        }

        /// <summary>
        /// 遷移先ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移先ビュー</param>
        public virtual void OnNavigateTo(ForwadingPluginContext context, object view)
        {
        }
    }
}
