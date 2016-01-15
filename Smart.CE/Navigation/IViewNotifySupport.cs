namespace Smart.Navigation
{
    /// <summary>
    /// ビュー通知サポート
    /// </summary>
    public interface IViewNotifySupport
    {
        /// <summary>
        /// ビュー通知
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="args">ビュー通知イベント</param>
        void OnViewNotify(Navigator sender, ViewNotifyEventArgs args);
    }
}