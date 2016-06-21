namespace Smart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly Type NullableType = typeof(Nullable<>);

        private static readonly Type CompilerGeneratedAttributeType = typeof(CompilerGeneratedAttribute);

        private static readonly Type GenericEnumerableType = typeof(IEnumerable<>);

        private static readonly Type EnumerableType = typeof(IEnumerable);

        private static readonly Type ObjectType = typeof(object);

        private static readonly Dictionary<Type, object> DefaultValues = new Dictionary<Type, object>
        {
            { typeof(bool), false },
            { typeof(byte), 0 },
            { typeof(sbyte), 0 },
            { typeof(short), 0 },
            { typeof(ushort), 0 },
            { typeof(int), 0 },
            { typeof(uint), 0U },
            { typeof(long), 0L },
            { typeof(ulong), 0UL },
            { typeof(IntPtr), IntPtr.Zero },
            { typeof(UIntPtr), UIntPtr.Zero },
            { typeof(char), '\0' },
            { typeof(double), 0.0 },
            { typeof(float), 0.0f }
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType && !(type.IsGenericType && type.GetGenericTypeDefinition() == NullableType))
            {
                object value;
                if (DefaultValues.TryGetValue(type, out value))
                {
                    return value;
                }

                return Activator.CreateInstance(type);
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == NullableType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool IsStruct(this Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool IsAnonymous(this Type type)
        {
            return Attribute.IsDefined(type, CompilerGeneratedAttributeType, false) &&
                type.IsGenericType &&
                type.Name.Contains("AnonymousType") &&
                (type.Name.StartsWith("<>", StringComparison.Ordinal) || type.Name.StartsWith("VB$", StringComparison.Ordinal)) &&
                ((type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic) &&
                ((type.Attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static Type GetCollectionElementType(this Type type)
        {
            if (type.HasElementType)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == GenericEnumerableType)
            {
                return type.GetGenericArguments()[0];
            }

            var enumerableType = type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == GenericEnumerableType);
            if (enumerableType != null)
            {
                return enumerableType.GetGenericArguments()[0];
            }

            if (EnumerableType.IsAssignableFrom(type))
            {
                return ObjectType;
            }

            return null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static Type GetNonNullableType(this Type type)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == NullableType))
            {
                return type.GetGenericArguments()[0];
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static Type GetEnumType(this Type type)
        {
            if (type.IsEnum)
            {
                return type;
            }

            if (type.IsGenericType && (type.GetGenericTypeDefinition() == NullableType))
            {
                type = type.GetGenericArguments()[0];
                return type.IsEnum ? type : null;
            }

            return null;
        }
    }
}
