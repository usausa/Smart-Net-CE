namespace Smart.Navigation
{
    using System;

    /// <summary>
    /// ビュー遷移イベント引数
    /// </summary>
    public class ViewForwardEventArgs : EventArgs
    {
        /// <summary>
        /// 遷移先ビューID
        /// </summary>
        public object ViewId { get; private set; }

        /// <summary>
        /// 遷移元ビューID
        /// </summary>
        public object PreviousViewId { get; private set; }

        /// <summary>
        /// 遷移元ビューインスタンス
        /// </summary>
        public object PreviousView { get; private set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public IViewParameters Parameters { get; private set; }

        /// <summary>
        /// 復帰遷移判定
        /// </summary>
        public bool IsRestore { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="viewId">遷移先ビューID</param>
        /// <param name="previousViewId">遷移元ビューID</param>
        /// <param name="previousView">遷移もとビューインスタンス</param>
        /// <param name="parameters">パラメータ</param>
        /// <param name="isRestore">復帰遷移判定</param>
        public ViewForwardEventArgs(object viewId, object previousViewId, object previousView, IViewParameters parameters, bool isRestore)
        {
            ViewId = viewId;
            PreviousViewId = previousViewId;
            PreviousView = previousView;
            Parameters = parameters;
            IsRestore = isRestore;
        }
    }
}