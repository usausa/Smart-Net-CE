namespace Smart.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class AccessorExtension
    {
        private static readonly Type DelegateNonNullableAccsessorType = typeof(DelegateNonNullableAccsessor<,>);

        private static readonly Type DelegateNullableAccsessorType = typeof(DelegateNullableAccsessor<,>);

        //--------------------------------------------------------------------------------
        // Generic
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this PropertyInfo pi)
        {
            return ToDelegateAccessor(pi);
        }

        //--------------------------------------------------------------------------------
        // Delegate
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static IAccessor ToDelegateAccessor(this PropertyInfo pi)
        {
            var getter = DelegateMethodGenerator.CreateGetter(pi);
            var setter = DelegateMethodGenerator.CreateSetter(pi);

            if (pi.PropertyType.IsValueType)
            {
                var accessorType = DelegateNonNullableAccsessorType.MakeGenericType(pi.DeclaringType, pi.PropertyType);
                return (IAccessor)ActivatorEx.CreateInstance(accessorType, pi, getter, setter, pi.PropertyType.GetDefaultValue());
            }
            else
            {
                var accessorType = DelegateNullableAccsessorType.MakeGenericType(pi.DeclaringType, pi.PropertyType);
                return (IAccessor)ActivatorEx.CreateInstance(accessorType, pi, getter, setter);
            }
        }

        //--------------------------------------------------------------------------------
        // Reflection
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static IAccessor ToReflectionAccessor(this PropertyInfo pi)
        {
            return new ReflectionAccessor(pi);
        }
    }
}
