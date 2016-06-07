namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    ///
    /// </summary>
    public interface IParameterBuilder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool IsMatch(object param);

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        void BuildParameters(IDbCommand cmd, object param);
    }

    /// <summary>
    ///
    /// </summary>
    public class DynamicParameterParameterBuilder : IParameterBuilder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool IsMatch(object param)
        {
            return param is IDynamicParameter;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        public void BuildParameters(IDbCommand cmd, object param)
        {
            ((IDynamicParameter)param).BuildParameters(cmd);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class DictionaryParameterBuilder : IParameterBuilder
    {
        private readonly DbTypeMap dbTypeMap;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbTypeMap"></param>
        public DictionaryParameterBuilder(DbTypeMap dbTypeMap)
        {
            this.dbTypeMap = dbTypeMap;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool IsMatch(object param)
        {
            return param is IDictionary<string, object>;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public void BuildParameters(IDbCommand cmd, object param)
        {
            foreach (var keyValue in (IDictionary<string, Object>)param)
            {
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = keyValue.Key;
                var value = keyValue.Value ?? DBNull.Value;
                if (value != DBNull.Value)
                {
                    parameter.DbType = dbTypeMap.LookupDbType(value);
                }
                parameter.Value = value;

                cmd.Parameters.Add(parameter);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ObjectParameterBuilder : IParameterBuilder
    {
        private readonly DbTypeMap dbTypeMap;

        private readonly ITypeMetadataFactory metadataFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbTypeMap"></param>
        /// <param name="metadataFactory"></param>
        public ObjectParameterBuilder(DbTypeMap dbTypeMap, ITypeMetadataFactory metadataFactory)
        {
            this.dbTypeMap = dbTypeMap;
            this.metadataFactory = metadataFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool IsMatch(object param)
        {
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public void BuildParameters(IDbCommand cmd, object param)
        {
            foreach (var accessor in metadataFactory.Create(param.GetType()).GetParameterAccessors())
            {
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = accessor.MemberInfo.Name;
                var value = accessor.GetValue(param) ?? DBNull.Value;
                if (value != DBNull.Value)
                {
                    parameter.DbType = dbTypeMap.LookupDbType(value);
                }
                parameter.Value = value;

                cmd.Parameters.Add(parameter);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class DefaultParameterBuilders
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IList<IParameterBuilder> Create()
        {
            return new List<IParameterBuilder>
            {
                new DynamicParameterParameterBuilder(),
                new DictionaryParameterBuilder(DbTypeMap.Default),
                new ObjectParameterBuilder(DbTypeMap.Default, DefaultTypeMetaDataFactory.Default)
            };
        }
    }
}
