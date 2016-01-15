namespace Smart.Navigation.Windows.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// Controlビューベースクラス
    /// </summary>
    public class ControlViewBase : UserControl, INavigatorAware, IViewEventSupport, IViewNotifySupport
    {
        /// <summary>
        /// ナビゲーター
        /// </summary>
        protected Navigator Navigator { get; private set; }

        /// <summary>
        /// ナビゲーター設定
        /// </summary>
        /// <param name="navigator">ナビゲーター</param>
        public void SetNavigator(Navigator navigator)
        {
            Navigator = navigator;
        }

        /// <summary>
        /// ビュークローズ
        /// </summary>
        public virtual void OnViewClose()
        {
            // please override
        }

        /// <summary>
        /// 画面遷移先
        /// </summary>
        /// <param name="args">引数</param>
        public virtual void OnViewNavigated(ViewForwardEventArgs args)
        {
            // please override
        }

        /// <summary>
        /// ビュー活性化
        /// </summary>
        /// <param name="args">引数</param>
        public virtual void OnViewActived(ViewForwardEventArgs args)
        {
            // please override
        }

        /// <summary>
        /// 画面遷移元
        /// </summary>
        public virtual void OnViewNavigating()
        {
            // please override
        }

        /// <summary>
        /// ビュー通知
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="args">引数</param>
        public virtual void OnViewNotify(Navigator sender, ViewNotifyEventArgs args)
        {
            // please override
        }
    }
}
