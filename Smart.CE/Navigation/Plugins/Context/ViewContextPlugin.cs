namespace Smart.Navigation.Plugins.Context
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Plugins;

    /// <summary>
    /// コンテキストプラグイン
    /// </summary>
    public class ViewContextPlugin : NavigatorPluginBase
    {
        //--------------------------------------------------------------------------------
        // クラス
        //--------------------------------------------------------------------------------

        #region <クラス>

        private class Reference
        {
            public object Context { get; set; }

            public int Counter { get; set; }
        }

        #endregion
        
        //--------------------------------------------------------------------------------
        // メンバ
        //--------------------------------------------------------------------------------

        #region <メンバ>

        private readonly Dictionary<Type, AttributeMember<ViewContextAttribute>[]> cache = new Dictionary<Type, AttributeMember<ViewContextAttribute>[]>();

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.プラグイン
        //--------------------------------------------------------------------------------

        #region <メソッド.プラグイン>

        /// <summary>
        /// ビュー作成イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        public override void OnCreate(object view)
        {
            foreach (var member in GetAttributeMembers(view.GetType()))
            {
                var key = member.Attribute.Key ?? member.MemberType.FullName;

                Reference reference;
                if (!store.TryGetValue(key, out reference))
                {
                    reference = new Reference
                    {
                        Context = Activator.CreateInstance(member.Attribute.Context ?? member.MemberType)
                    };

                    var support = reference.Context as IViewContextSupport;
                    if (support != null)
                    {
                        support.Initilize();
                    }

                    store[key] = reference;
                }

                reference.Counter++;

                member.SetValue(view, reference.Context);
            }
        }

        /// <summary>
        /// ビュー破棄イベント
        /// </summary>
        /// <param name="view">ビュー</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        public override void OnDispose(object view)
        {
            foreach (var member in GetAttributeMembers(view.GetType()))
            {
                var key = member.Attribute.Key ?? member.MemberType.FullName;

                Reference reference;
                if (!store.TryGetValue(key, out reference))
                {
                    continue;
                }

                reference.Counter--;
                if (reference.Counter != 0)
                {
                    continue;
                }

                var support = reference.Context as IViewContextSupport;
                if (support != null)
                {
                    support.Dispose();
                }

                store.Remove(key);
            }
        }

        #endregion

        //--------------------------------------------------------------------------------
        // サポート
        //--------------------------------------------------------------------------------

        #region <サポート>

        /// <summary>
        /// 属性付きメンバ情報一覧取得
        /// </summary>
        /// <param name="type">型</param>
        /// <returns>属性付きメンバ情報一覧</returns>
        private IEnumerable<AttributeMember<ViewContextAttribute>> GetAttributeMembers(Type type)
        {
            AttributeMember<ViewContextAttribute>[] members;
            if (cache.TryGetValue(type, out members))
            {
                return members;
            }

            members = AttributeMemberFactory.GetAttributeMembers<ViewContextAttribute>(type);
            cache[type] = members;
            return members;
        }

        #endregion
    }
}