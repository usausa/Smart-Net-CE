namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public interface IDynamicParameter
    {
        void BuildParameters(IDbCommand cmd);
    }

    /// <summary>
    ///
    /// </summary>
    public sealed class DynamicParameter : IDynamicParameter
    {
        private readonly DbTypeMap dbTypeMap;

        private readonly Dictionary<string, ParamInfo> parameters = new Dictionary<string, ParamInfo>();

        private class ParamInfo
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public DbType? DbType { get; set; }
            public int? Size { get; set; }
            public ParameterDirection Direction { get; set; }
            public IDbDataParameter AttachedParam { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        public DynamicParameter()
        {
            dbTypeMap = DbTypeMap.Default;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbTypeMap"></param>
        public DynamicParameter(DbTypeMap dbTypeMap)
        {
            this.dbTypeMap = dbTypeMap;
        }

        /// <summary>
        ///
        /// </summary>
        public IEnumerable<string> ParameterNames
        {
            get { return parameters.Select(_ => _.Key); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, object value)
        {
            parameters[name] = new ParamInfo { Name = name, Value = value, Direction = ParameterDirection.Input };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        public void Add(string name, object value, DbType dbType)
        {
            parameters[name] = new ParamInfo { Name = name, Value = value, DbType = dbType, Direction = ParameterDirection.Input };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        public void Add(string name, object value, DbType dbType, int size)
        {
            parameters[name] = new ParamInfo { Name = name, Value = value, DbType = dbType, Size = size, Direction = ParameterDirection.Input };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        public void Add(string name, object value, DbType dbType, ParameterDirection direction)
        {
            parameters[name] = new ParamInfo { Name = name, Value = value, DbType = dbType, Direction = direction };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void Add(string name, object value, DbType dbType, int size, ParameterDirection direction)
        {
            parameters[name] = new ParamInfo { Name = name, Value = value, DbType = dbType, Size = size, Direction = direction };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            var value = parameters[name].AttachedParam.Value;
            if (value == DBNull.Value)
            {
                return default(T);
            }
            return (T)value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        void IDynamicParameter.BuildParameters(IDbCommand cmd)
        {
            foreach (var param in parameters.Values)
            {
                var parameter = cmd.CreateParameter();
                cmd.Parameters.Add(parameter);

                param.AttachedParam = parameter;

                parameter.ParameterName = param.Name;
                parameter.Value = param.Value ?? DBNull.Value;
                parameter.DbType = param.DbType ?? dbTypeMap.LookupDbType(param.Value);
                if (param.Size.HasValue)
                {
                    parameter.Size = param.Size.Value;
                }
                parameter.Direction = param.Direction;
            }
        }
    }
}
