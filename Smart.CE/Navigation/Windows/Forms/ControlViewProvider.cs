namespace Smart.Navigation.Windows.Forms
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Controlビュープロバイダー
    /// </summary>
    public class ControlViewProvider : IViewProvider
    {
        private readonly Control parent;

        /// <summary>
        /// フィット
        /// </summary>
        public bool FitToParent { get; set; }

        /// <summary>
        /// 非同期
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RestoreFocus { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親コントロール</param>
        public ControlViewProvider(Control parent)
        {
            this.parent = parent;
            FitToParent = true;
        }

        /// <summary>
        /// イベントターゲット解決
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <returns>ターゲット</returns>
        public virtual object ResolveEventTarget(object view)
        {
            return view;
        }

        /// <summary>
        /// 非同期処理実行
        /// </summary>
        /// <param name="method">メソッド</param>
        /// <param name="args">引数</param>
        public virtual void BeginInvoke(Delegate method, params object[] args)
        {
            parent.BeginInvoke(method, args);
        }

        /// <summary>
        /// ビュー作成
        /// </summary>
        /// <param name="type">型</param>
        /// <returns>ビューインスタンス</returns>
        public virtual object ViewCreate(Type type)
        {
            var view = (Control)Activator.CreateInstance(type);

            if (FitToParent)
            {
                view.Size = parent.Size;
            }

            parent.Controls.Add(view);
            if (FitToParent)
            {
                view.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            return view;
        }

        /// <summary>
        /// ビュー活性化
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <param name="parameter">復帰情報</param>
        public virtual void ViewActive(object view, object parameter)
        {
            var control = (Control)view;

            control.Visible = true;

            if (RestoreFocus)
            {
                var focused = parameter as Control;
                if (focused != null)
                {
                    focused.Focus();
                }
            }
            else
            {
                control.Focus();
            }
        }

        /// <summary>
        /// ビュー非活性化
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <returns>復帰情報</returns>
        public virtual object ViewDeactive(object view)
        {
            var control = (Control)view;

            control.Visible = false;

            if (RestoreFocus)
            {
                while (control.Parent != null)
                {
                    control = control.Parent;
                }

                return GetFocused(control);
            }
            return null;
        }

        /// <summary>
        /// ビュー破棄
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        public virtual void ViewDispose(object view)
        {
            var control = (Control)view;

            parent.Controls.Remove(control);
            control.Parent = null;

            control.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static Control GetFocused(Control parent)
        {
            return parent.Focused ? parent : parent.Controls.Cast<Control>().Select(c => GetFocused(c)).FirstOrDefault(_ => _ != null);
        }
    }
}
