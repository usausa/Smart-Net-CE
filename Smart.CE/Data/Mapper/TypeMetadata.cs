namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;

    using Smart.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public interface ITypeMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Ignore")]
        IEnumerable<IAccessor> GetParameterAccessors();

        IAccessor GetMapAccessor(string column);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DefaultTypeMetadata : ITypeMetadata
    {
        private readonly IList<IAccessor> parameterAccessors = new List<IAccessor>();

        private readonly Dictionary<string, IAccessor> mapAccessors = new Dictionary<string, IAccessor>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public DefaultTypeMetadata(Type type)
        {
            foreach (var mi in type.GetPublicAccessableMember())
            {
                var accessor = mi.ToAccessor();
                if (accessor.CanRead)
                {
                    parameterAccessors.Add(accessor);
                }
                if (accessor.CanWrite)
                {
                    mapAccessors[mi.Name] = accessor;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetParameterAccessors()
        {
            return parameterAccessors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IAccessor GetMapAccessor(string column)
        {
            IAccessor accessor;
            mapAccessors.TryGetValue(column, out accessor);
            return accessor;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITypeMetadataFactory
    {
        ITypeMetadata Create(Type type);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DefaultTypeMetaDataFactory : ITypeMetadataFactory
    {
        private static readonly DefaultTypeMetaDataFactory Instance = new DefaultTypeMetaDataFactory();

        private readonly object sync = new object();

        private readonly Dictionary<Type, DefaultTypeMetadata> cache = new Dictionary<Type, DefaultTypeMetadata>();

        /// <summary>
        /// 
        /// </summary>
        public static DefaultTypeMetaDataFactory Default
        {
            get { return Instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ITypeMetadata Create(Type type)
        {
            lock (sync)
            {
                DefaultTypeMetadata metadata;
                if (cache.TryGetValue(type, out metadata))
                {
                    return metadata;
                }

                metadata = new DefaultTypeMetadata(type);
                cache[type] = metadata;

                return metadata;
            }
        }
    }
}
