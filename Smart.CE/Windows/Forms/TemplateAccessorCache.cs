namespace Smart.Windows.Forms
{
    using System;
    using System.Collections.Generic;

    using Smart.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class TemplateAccessorCache
    {
        private static readonly Dictionary<Tuple<Type, string>, IAccessor> Cache = new Dictionary<Tuple<Type, string>, IAccessor>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IAccessor GetAccessor(Type type, string name)
        {
            lock (Cache)
            {
                var key = new Tuple<Type, string>(type, name);

                IAccessor accessor;
                if (Cache.TryGetValue(key, out accessor))
                {
                    return accessor;
                }

                var pi = type.GetProperty(name);
                accessor = pi == null ? null : pi.ToAccessor();
                Cache[key] = accessor;

                return accessor;
            }
        }
    }
}
