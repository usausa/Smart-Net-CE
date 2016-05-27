namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Smart.Converter;
    using Smart.Reflection;

    /// <summary>
    ///
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsMatch(Type type);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="reader"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        IEnumerable<T> Handle<T>(Func<T> factory, IDataReader reader, ObjectConverter converter);
    }

    /// <summary>
    ///
    /// </summary>
    public class DictionaryQueryHandler : IQueryHandler
    {
        private static readonly Type TargetType = typeof(IDictionary<string, object>);

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsMatch(Type type)
        {
            return TargetType.IsAssignableFrom(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="reader"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public IEnumerable<T> Handle<T>(Func<T> factory, IDataReader reader, ObjectConverter converter)
        {
            var columns = new string[reader.FieldCount];
            for (var i = 0; i < columns.Length; i++)
            {
                columns[i] = reader.GetName(i);
            }

            while (reader.Read())
            {
                var entity = (IDictionary<string, object>)factory();

                foreach (var column in columns)
                {
                    var value = reader[column];

                    if (value == DBNull.Value)
                    {
                        value = null;
                    }

                    entity[column] = value;
                }

                yield return (T)entity;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ObjectQueryHandler : IQueryHandler
    {
        private readonly ITypeMetadataFactory metadataFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="metadataFactory"></param>
        public ObjectQueryHandler(ITypeMetadataFactory metadataFactory)
        {
            this.metadataFactory = metadataFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsMatch(Type type)
        {
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="reader"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public IEnumerable<T> Handle<T>(Func<T> factory, IDataReader reader, ObjectConverter converter)
        {
            using (reader)
            {
                var metadata = metadataFactory.Create(typeof(T));

                var columns = new string[reader.FieldCount];
                var accessors = new IAccessor[reader.FieldCount];
                for (var i = 0; i < columns.Length; i++)
                {
                    var name = reader.GetName(i);
                    columns[i] = name;
                    accessors[i] = metadata.GetMapAccessor(name);
                }

                while (reader.Read())
                {
                    var entity = factory();

                    for (var i = 0; i < columns.Length; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null)
                        {
                            continue;
                        }

                        var value = reader[columns[i]];

                        if (accessor.Type != value.GetType())
                        {
                            value = converter.Convert(value, accessor.Type);
                        }

                        accessor.SetValue(entity, value);
                    }

                    yield return entity;
                }
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class DefaultQueryHandlers
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IList<IQueryHandler> Create()
        {
            return new List<IQueryHandler>
            {
                new DictionaryQueryHandler(),
                new ObjectQueryHandler(DefaultTypeMetaDataFactory.Default)
            };
        }
    }
}
