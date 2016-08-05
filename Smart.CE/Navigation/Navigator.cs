namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Navigation.Plugins;
    using Smart.Navigation.Plugins.Context;
    using Smart.Navigation.Plugins.Parameter;

    /// <summary>
    /// 画面制御ナビゲーター
    /// </summary>
    public class Navigator
    {
        //--------------------------------------------------------------------------------
        // クラス
        //--------------------------------------------------------------------------------

        #region <クラス>

        /// <summary>
        /// ビュー情報
        /// </summary>
        private class ViewInfo
        {
            public object Id { get; private set; }

            public object Domain { get; private set; }

            public Type Type { get; private set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="id">画面ID</param>
            /// <param name="domain">ドメイン</param>
            /// <param name="type">型</param>
            public ViewInfo(object id, object domain, Type type)
            {
                Id = id;
                Domain = domain;
                Type = type;
            }
        }

        /// <summary>
        /// スタック情報
        /// </summary>
        private class StackInfo
        {
            public ViewInfo ViewInfo { get; private set; }

            public object View { get; private set; }

            public object RestoreParameter { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="viewInfo">ビュー情報</param>
            /// <param name="view">ビューインスタンス</param>
            public StackInfo(ViewInfo viewInfo, object view)
            {
                ViewInfo = viewInfo;
                View = view;
            }
        }

        #endregion

        //--------------------------------------------------------------------------------
        // イベント
        //--------------------------------------------------------------------------------

        #region <イベント>

        public event EventHandler<ViewConfirmEventArgs> Confirm;

        public event EventHandler<ViewForwardEventArgs> Forwarding;

        public event EventHandler<ViewExitEventArgs> Exited;

        #endregion

        //--------------------------------------------------------------------------------
        // メンバ
        //--------------------------------------------------------------------------------

        #region <メンバ>

        private static readonly ViewParameters EmptyParameters = new ViewParameters();

        private readonly Dictionary<object, ViewInfo> idToViewType = new Dictionary<object, ViewInfo>();

        private readonly List<StackInfo> stacked = new List<StackInfo>();

        private readonly IViewProvider provider;

        #endregion

        //--------------------------------------------------------------------------------
        // プロパティ
        //--------------------------------------------------------------------------------

        #region <プロパティ>

        /// <summary>
        /// プラグインリスト
        /// </summary>
        public ICollection<INavigatorPlugin> Plugins { get; private set; }

        /// <summary>
        /// 現在のビュー
        /// </summary>
        public object CurrentView
        {
            get { return stacked.Count > 0 ? stacked[stacked.Count - 1].View : null; }
        }

        /// <summary>
        /// 現在のビューID
        /// </summary>
        public object CurrentViewId
        {
            get { return stacked.Count > 0 ? stacked[stacked.Count - 1].ViewInfo.Id : null; }
        }

        /// <summary>
        /// 現在のドメイン
        /// </summary>
        public object CurrentViewDomain
        {
            get { return stacked.Count > 0 ? stacked[stacked.Count - 1].ViewInfo.Domain : null; }
        }

        /// <summary>
        /// 現在のパラメーター
        /// </summary>
        private object CurrentRestoreParameter
        {
            get { return stacked.Count > 0 ? stacked[stacked.Count - 1].RestoreParameter : null; }
            set
            {
                if (stacked.Count > 0)
                {
                    stacked[stacked.Count - 1].RestoreParameter = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int StackedCount
        {
            get { return stacked.Count; }
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド
        //--------------------------------------------------------------------------------

        #region <メソッド>

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="provider">ビュープロバイダ</param>
        public Navigator(IViewProvider provider)
        {
            this.provider = provider;

            Plugins = new List<INavigatorPlugin>
            {
                // デフォルト
                new ViewParameterPlugin(),
                new ViewContextPlugin()
            };
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.登録
        //--------------------------------------------------------------------------------

        #region <メソッド.登録>

        /// <summary>
        /// ビュー登録
        /// </summary>
        /// <param name="id">ビューID</param>
        /// <param name="type">ビュー型</param>
        public void AddView(object id, Type type)
        {
            AddView(id, null, type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domain"></param>
        /// <param name="type"></param>
        public void AddView(object id, object domain, Type type)
        {
            idToViewType.Add(id, new ViewInfo(id, domain, type));
        }

        /// <summary>
        /// ビュー自動登録
        /// </summary>
        /// <param name="assembly">探索対象アセンブリ</param>
        public void AutoRegister(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            foreach (var type in assembly.GetTypes())
            {
                foreach (var attr in (ViewAttribute[])Attribute.GetCustomAttributes(type, typeof(ViewAttribute)))
                {
                    AddView(attr.Id, attr.Domain, type);
                }
            }
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.通知
        //--------------------------------------------------------------------------------

        #region <メソッド.通知>

        /// <summary>
        /// ビューへの通知
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <returns>通知正否</returns>
        public bool Notify(object msg)
        {
            return Notify(msg, null);
        }

        /// <summary>
        /// ビューへの通知
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="parameters">パラメータ</param>
        /// <returns>通知正否</returns>
        public bool Notify(object msg, IViewParameters parameters)
        {
            var support = provider.ResolveEventTarget(CurrentView) as IViewNotifySupport;
            if (support == null)
            {
                return false;
            }

            support.OnViewNotify(this, new ViewNotifyEventArgs(msg, parameters ?? EmptyParameters));

            return true;
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.遷移
        //--------------------------------------------------------------------------------

        #region <メソッド.遷移>

        /// <summary>
        /// 画面遷移
        /// </summary>
        /// <param name="id">画面ID</param>
        public void Forward(object id)
        {
            Forward(id, null);
        }

        /// <summary>
        /// 画面遷移
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <param name="parameters">パラメータ</param>
        public void Forward(object id, IViewParameters parameters)
        {
            InternalForward(id, parameters);
        }

        /// <summary>
        /// スタック型画面遷移(プッシュ)
        /// </summary>
        /// <param name="id">画面ID</param>
        public void Push(object id)
        {
            Push(id, null);
        }

        /// <summary>
        /// スタック型画面遷移(プッシュ)
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <param name="parameters">パラメータ</param>
        public void Push(object id, IViewParameters parameters)
        {
            InternalPush(id, parameters);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ)
        /// </summary>
        public void Pop()
        {
            Pop(1, null);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ)
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        public void Pop(IViewParameters parameters)
        {
            Pop(1, parameters);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ)
        /// </summary>
        /// <param name="level">ポップ数</param>
        public void Pop(int level)
        {
            Pop(level, null);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ)
        /// </summary>
        /// <param name="level">ポップ数</param>
        /// <param name="parameters">パラメータ</param>
        public void Pop(int level, IViewParameters parameters)
        {
            if (level < 1)
            {
                throw new ArgumentOutOfRangeException("level");
            }

            if (stacked.Count - 1 < level)
            {
                return;
            }

            InternalPop(level, parameters);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ or フォワード)
        /// </summary>
        /// <param name="id">画面ID</param>
        public void PopOrForward(object id)
        {
            PopOrForward(id, stacked.Count, null);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ or フォワード)
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <param name="level">ポップ数</param>
        public void PopOrForward(object id, int level)
        {
            PopOrForward(id, level, null);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ or フォワード)
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <param name="parameters">パラメータ</param>
        public void PopOrForward(object id, IViewParameters parameters)
        {
            PopOrForward(id, stacked.Count, parameters);
        }

        /// <summary>
        /// スタック型画面遷移(ポップ or フォワード)
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <param name="level">ポップ数</param>
        /// <param name="parameters">パラメータ</param>
        public void PopOrForward(object id, int level, IViewParameters parameters)
        {
            if (level < 0)
            {
                throw new ArgumentOutOfRangeException("level");
            }

            if (stacked.Count < level)
            {
                InternalPop(level, parameters);
            }
            else
            {
                InternalForward(id, parameters);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        private void InternalForward(object id, IViewParameters parameters)
        {
            if (!ConfirmNavigate())
            {
                return;
            }

            ViewInfo info;
            if (!idToViewType.TryGetValue(id, out info))
            {
                return;
            }

            InternalNavigate(parameters, () =>
            {
                // 前画面破棄
                ViewClose(CurrentView);

                if (CurrentView != null)
                {
                    stacked.RemoveAt(stacked.Count - 1);
                }

                // 新規スタック
                stacked.Add(new StackInfo(info, CreateView(info.Type)));

                return false;
            }, DisposeView);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        private void InternalPush(object id, IViewParameters parameters)
        {
            if (!ConfirmNavigate())
            {
                return;
            }

            ViewInfo info;
            if (!idToViewType.TryGetValue(id, out info))
            {
                return;
            }

            // グループ判定
            var exist = false;
            var first = -1;
            var last = -1;
            if ((info.Domain != null) && !Equals(CurrentViewDomain, info.Domain))
            {
                first = stacked.FindIndex(_ => Equals(_.ViewInfo.Domain, info.Domain));
                if (first >= 0)
                {
                    last = stacked.FindLastIndex(_ => Equals(_.ViewInfo.Domain, info.Domain));
                    exist = stacked.Skip(first).Take(last - first + 1).Any(_ => Equals(_.ViewInfo.Id, info.Id));
                }
            }

            InternalNavigate(parameters, () =>
            {
                // 前画面デアクティブ
                CurrentRestoreParameter = ViewDeactive(CurrentView);

                if (first >= 0)
                {
                    // スタック入れ替え
                    var temp = new StackInfo[last - first + 1];
                    stacked.CopyTo(first, temp, 0, temp.Length);
                    for (var i = 0; i < stacked.Count - last - 1; i++)
                    {
                        stacked[i + first] = stacked[i + last + 1];
                    }
                    for (var i = 0; i < temp.Length; i++)
                    {
                        stacked[stacked.Count - temp.Length + i] = temp[i];
                    }
                }

                if (!exist)
                {
                    // 新規スタック
                    stacked.Add(new StackInfo(info, CreateView(info.Type)));
                }

                return false;
            }, previousView => { });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="parameters"></param>
        private void InternalPop(int level, IViewParameters parameters)
        {
            if (!ConfirmNavigate())
            {
                return;
            }

            InternalNavigate(parameters, () =>
            {
                // 前画面破棄
                ViewClose(CurrentView);

                // 中間スタック破棄
                for (var i = stacked.Count - 2; i >= stacked.Count - level; i--)
                {
                    ViewClose(stacked[i].View);
                    DisposeView(stacked[i].View);
                }

                stacked.RemoveRange(stacked.Count - level, level);

                return true;
            }, DisposeView);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="updateStack"></param>
        /// <param name="postProcess"></param>
        private void InternalNavigate(IViewParameters parameters, Func<bool> updateStack, Action<object> postProcess)
        {
            if (provider.IsAsync)
            {
                provider.BeginInvoke((Action)(() => DoInternalNavigate(parameters, updateStack, postProcess)));
            }
            else
            {
                DoInternalNavigate(parameters, updateStack, postProcess);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ConfirmNavigate()
        {
            var args = new ViewConfirmEventArgs();

            var view = CurrentView;
            if (view != null)
            {
                var target = provider.ResolveEventTarget(view);
                var support = target as IViewConfirmSupport;
                if (support != null)
                {
                    support.OnViewConfirm(args);
                    if (args.Cancel)
                    {
                        return false;
                    }
                }
            }

            if (Confirm != null)
            {
                Confirm(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="updateStack"></param>
        /// <param name="postProcess"></param>
        private void DoInternalNavigate(IViewParameters parameters, Func<bool> updateStack, Action<object> postProcess)
        {
            // プラグインコンテキスト
            var pluginContext = new ForwadingPluginContext();

            // 遷移元画面遷移イベント
            ViewForwardFrom(CurrentView, pluginContext);

            // 前状態
            var previousViewId = CurrentViewId;
            var previousView = CurrentView;

            // スタック更新
            var restore = updateStack();

            // イベント
            var args = new ViewForwardEventArgs(CurrentViewId, previousViewId, previousView, parameters ?? EmptyParameters, restore);

            // ナビゲーターイベント
            if (Forwarding != null)
            {
                Forwarding(this, args);
            }

            // 遷移先画面遷移イベント
            ViewForwardTo(CurrentView, args, CurrentRestoreParameter, pluginContext);

            // 復帰情報クリア
            CurrentRestoreParameter = null;

            // 後処理
            postProcess(previousView);
        }

        /// <summary>
        /// ナビゲーター終了処理
        /// </summary>
        public void Exit()
        {
            // 中間スタック破棄
            for (var i = stacked.Count - 1; i >= 0; i--)
            {
                ViewClose(stacked[i].View);
                DisposeView(stacked[i].View);
            }

            stacked.Clear();

            // ナビゲーター後処理
            if (Exited != null)
            {
                Exited(this, new ViewExitEventArgs());
            }
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.ライフサイクル
        //--------------------------------------------------------------------------------

        #region <メソッド.ライフサイクル

        /// <summary>
        /// ビュー作成
        /// </summary>
        /// <param name="type">型</param>
        /// <returns>ビューインスタンス</returns>
        private object CreateView(Type type)
        {
            // ビュー作成
            var view = provider.ViewCreate(type);

            var target = provider.ResolveEventTarget(view);

            // ナビゲーター設定
            var aware = target as INavigatorAware;
            if (aware != null)
            {
                aware.SetNavigator(this);
            }

            // プラグインイベント
            foreach (var plugin in Plugins)
            {
                plugin.OnCreate(target);
            }

            return view;
        }

        /// <summary>
        /// ビュー破棄
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        private void DisposeView(object view)
        {
            if (view == null)
            {
                return;
            }

            var target = provider.ResolveEventTarget(view);

            // プラグインイベント
            foreach (var plugin in Plugins)
            {
                plugin.OnDispose(target);
            }

            // ビュー破棄
            provider.ViewDispose(view);
        }

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.ビューイベント
        //--------------------------------------------------------------------------------

        #region <メソッド.ビューイベント>

        /// <summary>
        /// 遷移元画面イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        /// <param name="context">プラグインコンテキスト</param>
        private void ViewForwardFrom(object view, ForwadingPluginContext context)
        {
            if (view == null)
            {
                return;
            }

            var target = provider.ResolveEventTarget(view);

            // 遷移イベント
            var support = target as IViewEventSupport;
            if (support != null)
            {
                support.OnViewNavigating();
            }

            // プラグインイベント
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigateFrom(context, target);
            }
        }

        /// <summary>
        /// 遷移先画面イベント
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <param name="args">引数</param>
        /// <param name="parameter">復帰情報</param>
        /// <param name="context">プラグインコンテキスト</param>
        private void ViewForwardTo(object view, ViewForwardEventArgs args, object parameter, ForwadingPluginContext context)
        {
            var target = provider.ResolveEventTarget(view);

            // プラグインイベント
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigateTo(context, target);
            }

            // 遷移イベント
            var support = target as IViewEventSupport;
            if (support != null)
            {
                support.OnViewNavigated(args);
            }

            // 活性化
            provider.ViewActive(view, parameter);

            // 活性化イベント
            if (support != null)
            {
                support.OnViewActived(args);
            }
        }

        /// <summary>
        /// ビュー非活性化
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        /// <returns>パラメーター</returns>
        private object ViewDeactive(object view)
        {
            if (view == null)
            {
                return null;
            }

            // 非活性化
            return provider.ViewDeactive(view);
        }

        /// <summary>
        /// ビュークローズ
        /// </summary>
        /// <param name="view">ビューインスタンス</param>
        private void ViewClose(object view)
        {
            if (view == null)
            {
                return;
            }

            // クローズイベント
            var support = provider.ResolveEventTarget(view) as IViewEventSupport;
            if (support != null)
            {
                support.OnViewClose();
            }
        }

        #endregion
    }
}