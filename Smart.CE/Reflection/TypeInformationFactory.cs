namespace Smart.Reflection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class TypeInformationFactory
    {
        private static readonly object Sync = new object();
        private static readonly Dictionary<Type, TypeInformation> Cache = new Dictionary<Type, TypeInformation>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeInformation Create(Type type)
        {
            lock (Sync)
            {
                TypeInformation information;
                if (Cache.TryGetValue(type, out information))
                {
                    return information;
                }

                information = new TypeInformation(type);
                Cache[type] = information;

                return information;
            }
        }
    }
}
