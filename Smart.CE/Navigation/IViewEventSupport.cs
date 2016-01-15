namespace Smart.Navigation
{
    /// <summary>
    /// ビューイベントサポート
    /// </summary>
    public interface IViewEventSupport
    {
        /// <summary>
        /// ビュークローズ
        /// </summary>
        void OnViewClose();

        /// <summary>
        /// 遷移元画面イベント
        /// </summary>
        void OnViewNavigating();

        /// <summary>
        /// 遷移先画イベント
        /// </summary>
        /// <param name="args">ビュー遷移イベント</param>
        void OnViewNavigated(ViewForwardEventArgs args);

        /// <summary>
        /// ビュー活性化
        /// </summary>
        /// <param name="args">ビュー遷移イベント</param>
        void OnViewActived(ViewForwardEventArgs args);
    }
}