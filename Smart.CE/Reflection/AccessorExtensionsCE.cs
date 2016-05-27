namespace Smart.Reflection
{
    using System;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class AccessorExtensionsCE
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this FieldInfo fi)
        {
            return new ReflectionFieldAccessor(fi);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this PropertyInfo pi)
        {
            var getter = DelegateMethodGenerator.CreateTypedGetDelegate(pi);
            var setter = DelegateMethodGenerator.CreateTypedSetDelegate(pi);

            if (pi.PropertyType.IsValueType)
            {
                var accessorType = typeof(ValueTypeDelegateAccsessor<,>).MakeGenericType(pi.DeclaringType, pi.PropertyType);
                return (IAccessor)ActivatorEx.CreateInstance(accessorType, pi, pi.PropertyType, getter, setter, DefaultValue.Of(pi.PropertyType));
            }
            else
            {
                var accessorType = typeof(DelegateAccsessor<,>).MakeGenericType(pi.DeclaringType, pi.PropertyType);
                return (IAccessor)ActivatorEx.CreateInstance(accessorType, pi, pi.PropertyType, getter, setter);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static IAccessor ToAccessor(this MemberInfo mi)
        {
            var fi = mi as FieldInfo;
            if (fi != null)
            {
                return fi.ToAccessor();
            }

            var pi = mi as PropertyInfo;
            if (pi != null)
            {
                return pi.ToAccessor();
            }

            throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Type {0} must be PropertyInfo or FieldInfo.", mi.GetType()));
        }
    }
}
