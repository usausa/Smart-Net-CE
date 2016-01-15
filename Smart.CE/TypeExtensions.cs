namespace Smart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
#if !WindowsCE
    using System.ComponentModel;
#endif
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsAnonymous(this Type type)
        {
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false) &&
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Type GetValueType(this Type type)
        {
            var pi = type.GetProperty("Value");
            return pi.PropertyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsEnumerableType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsListType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsDictionaryType(this Type type)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                return true;
            }

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType);
            var baseDefinitions = genericInterfaces.Select(t => t.GetGenericTypeDefinition());
            return baseDefinitions.Any(t => t == typeof(IDictionary<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Type GetDictionaryType(this Type type)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                return type;
            }

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            return genericInterfaces.FirstOrDefault();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetElementType(this Type type)
        {
            return GetElementType(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Type GetElementType(this Type type, IEnumerable enumerable)
        {
            if (type.HasElementType)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }

            var ienumerableType = GetIEnumerableType(type);
            if (ienumerableType != null)
            {
                return ienumerableType.GetGenericArguments()[0];
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (enumerable != null)
                {
                    var first = enumerable.Cast<object>().FirstOrDefault();
                    if (first != null)
                    {
                        return first.GetType();
                    }
                }
                return typeof(object);
            }

            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "Unable to find element type for {0}.", type), "type");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type GetIEnumerableType(Type type)
        {
            try
            {
#if WindowsCE
                return type.GetInterfaces().FirstOrDefault(c => c.Name == "IEnumerable`1");
#else
                return type.GetInterface("IEnumerable`1");
#endif
            }
            catch (AmbiguousMatchException)
            {
                if (type.BaseType != typeof(object))
                {
                    return GetIEnumerableType(type.BaseType);
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Type GetEnumType(this Type type)
        {
            if (type.IsNullableType())
            {
                type = type.GetGenericArguments()[0];
            }

            return type.IsEnum ? type : null;
        }

#if !WindowsCE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsComplexType(this Type type)
        {
            return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
        }
#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool HasDefaultConstructor(this Type type)
        {
#if WindowsCE
            return type.GetConstructor(new Type[0]) != null;
#else
            return type.GetConstructor(Type.EmptyTypes) != null;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsImplements(this Type type, Type interfaceType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException("TInterface");
            }

            return type.GetInterfaces().Any(t => t == interfaceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsInherits(this Type type, Type baseType)
        {
            do
            {
                if (type == baseType)
                {
                    return true;
                }
                if ((type == type.BaseType) || (type.BaseType == null))
                {
                    return false;
                }
                type = type.BaseType;
            } while (true);
        }
    }
}
