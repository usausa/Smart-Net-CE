namespace Smart.Reflection
{
    using System;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo != null)
            {
                return methodInfo.ReturnType;
            }

            var eventInfo = memberInfo as EventInfo;
            if (eventInfo != null)
            {
                return eventInfo.EventHandlerType;
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static object GetMemberValue(this MemberInfo memberInfo, object target)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(target, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null, null);
            }

            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(target);
            }

            throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "{0} is not supported.", memberInfo.GetType()));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetMemberValue(this MemberInfo memberInfo, object target, object value)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(target, value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null, null);
            }

            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null);
            }

            throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "{0} is not supported.", memberInfo.GetType()));
        }
    }
}
