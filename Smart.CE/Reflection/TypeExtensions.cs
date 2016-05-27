namespace Smart.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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
        public static IEnumerable<MemberInfo> GetAccessableMember(this Type type)
        {
            return GetAccessableMember(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetPublicAccessableMember(this Type type)
        {
            return GetAccessableMember(type, BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetPrivateAccessableMember(this Type type)
        {
            return GetAccessableMember(type, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static IEnumerable<MemberInfo> GetAccessableMember(this Type type, BindingFlags flags)
        {
            IEnumerable<Type> typesToScan = new[] { type, type.BaseType };
            if (type.IsInterface)
            {
                typesToScan = typesToScan.Concat(type.FindInterfaces((m, f) => true, null));
            }

            return typesToScan
                .Where(x => x != null)
                .SelectMany(x => x.FindMembers(MemberTypes.Property | MemberTypes.Field, flags,
                    (m, f) =>
                    {
                        if (m is FieldInfo)
                        {
                            return true;
                        }
                        var pi = m as PropertyInfo;
                        return pi != null && !pi.GetIndexParameters().Any();
                    }, null))
                .GroupBy(x => x.Name)
                .Select(x => x.First());
        }
    }
}
