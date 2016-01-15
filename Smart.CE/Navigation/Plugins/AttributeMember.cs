namespace Smart.Navigation.Plugins
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 属性付きメンバ情報
    /// </summary>
    /// <typeparam name="T">属性型</typeparam>
    public class AttributeMember<T> where T : Attribute
    {
        private readonly FieldInfo fieldInfo;

        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// メンバ名称
        /// </summary>
        public string Name
        {
            get
            {
                return fieldInfo != null ? fieldInfo.Name : propertyInfo.Name;
            }
        }

        /// <summary>
        /// メンバ型
        /// </summary>
        public Type MemberType
        {
            get
            {
                return fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
            }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public T Attribute { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fieldInfo">フィールド情報</param>
        /// <param name="attribute">属性</param>
        public AttributeMember(FieldInfo fieldInfo, T attribute)
        {
            this.fieldInfo = fieldInfo;
            Attribute = attribute;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="propertyInfo">プロパティ情報</param>
        /// <param name="attribute">属性</param>
        public AttributeMember(PropertyInfo propertyInfo, T attribute)
        {
            this.propertyInfo = propertyInfo;
            Attribute = attribute;
        }

        /// <summary>
        /// 値取得
        /// </summary>
        /// <param name="target">対象オブジェクト</param>
        /// <returns>値</returns>
        public object GetValue(object target)
        {
            return fieldInfo != null ? fieldInfo.GetValue(target) : propertyInfo.GetValue(target, null);
        }

        /// <summary>
        /// 値設定
        /// </summary>
        /// <param name="target">対象オブジェクト</param>
        /// <param name="value">対象</param>
        public void SetValue(object target, object value)
        {
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value);
            }
            else
            {
                propertyInfo.SetValue(target, value, null);
            }
        }
    }

    /// <summary>
    /// 属性付きメンバ情報ファクトリー
    /// </summary>
    public static class AttributeMemberFactory
    {
        /// <summary>
        /// 属性付きメンバ情報一覧取得
        /// </summary>
        /// <typeparam name="T">型属性</typeparam>
        /// <param name="type">型</param>
        /// <returns>属性付きメンバ情報一覧</returns>
        public static AttributeMember<T>[] GetAttributeMembers<T>(Type type) where T : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .SelectMany(
                        fi => (T[])Attribute.GetCustomAttributes(fi, typeof(T)),
                        (fi, attr) => new AttributeMember<T>(fi, attr))
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .SelectMany(
                                pi => (T[])Attribute.GetCustomAttributes(pi, typeof(T)),
                                (pi, attr) => new AttributeMember<T>(pi, attr)))
                    .ToArray();
        }
    }
}
