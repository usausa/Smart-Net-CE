namespace Smart.Navigation.Plugins.Parameter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Smart.Navigation.Plugins;

    /// <summary>
    /// ビューパラメータプラグイン
    /// </summary>
    public class ViewParameterPlugin : NavigatorPluginBase
    {
        //--------------------------------------------------------------------------------
        // メンバ
        //--------------------------------------------------------------------------------

        #region <メンバ>

        private readonly Dictionary<Type, AttributeMember<ViewParameterAttribute>[]> cache = new Dictionary<Type, AttributeMember<ViewParameterAttribute>[]>();

        #endregion

        //--------------------------------------------------------------------------------
        // メソッド.プラグイン
        //--------------------------------------------------------------------------------

        #region <メソッド.プラグイン>

        /// <summary>
        /// 遷移元ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移元ビュー</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        public override void OnNavigateFrom(ForwadingPluginContext context, object view)
        {
            context.Save(GetType(), GatherExportParameters(view));
        }

        /// <summary>
        /// 遷移先ビューイベント
        /// </summary>
        /// <param name="context">プラグインコンテキスト</param>
        /// <param name="view">遷移先ビュー</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        public override void OnNavigateTo(ForwadingPluginContext context, object view)
        {
            var parameters = context.Load<Dictionary<string, object>>(GetType());
            if (parameters != null)
            {
                ApplyImportParameters(view, parameters);
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
        private IEnumerable<AttributeMember<ViewParameterAttribute>> GetAttributeMembers(Type type)
        {
            AttributeMember<ViewParameterAttribute>[] members;
            if (cache.TryGetValue(type, out members))
            {
                return members;
            }

            members = AttributeMemberFactory.GetAttributeMembers<ViewParameterAttribute>(type);
            cache[type] = members;
            return members;
        }

        /// <summary>
        /// エクスポートパラメータ抽出
        /// </summary>
        /// <param name="view">遷移元ビュー</param>
        /// <returns>抽出パラメータ一覧</returns>
        private Dictionary<string, object> GatherExportParameters(object view)
        {
            var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            foreach (var member in GetAttributeMembers(view.GetType()))
            {
                if ((member.Attribute.Direction & Direction.Export) != 0)
                {
                    var name = member.Attribute.Name ?? member.Name;
                    parameters.Add(name, member.GetValue(view));
                }
            }

            return parameters;
        }

        /// <summary>
        /// 抽出パラメータ適用
        /// </summary>
        /// <param name="view">遷移先ビュー</param>
        /// <param name="parameters">抽出パラメータ一覧</param>
        private void ApplyImportParameters(object view, IDictionary<string, object> parameters)
        {
            foreach (var member in GetAttributeMembers(view.GetType()))
            {
                if ((member.Attribute.Direction & Direction.Import) != 0)
                {
                    var name = member.Attribute.Name ?? member.Name;
                    object value;
                    if (parameters.TryGetValue(name, out value))
                    {
                        member.SetValue(view, Convert.ChangeType(value, member.MemberType, CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        #endregion
    }
}