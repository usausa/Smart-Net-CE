namespace Smart.Navigation
{
    using System;

    /// <summary>
    /// ビュー通知イベント引数
    /// </summary>
    public class ViewNotifyEventArgs : EventArgs
    {
        /// <summary>
        /// メッセージ
        /// </summary>
        public object Msg { get; private set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public IViewParameters Paramters { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="paramters">パラメータ</param>
        public ViewNotifyEventArgs(object msg, IViewParameters paramters)
        {
            Msg = msg;
            Paramters = paramters;
        }
    }
}