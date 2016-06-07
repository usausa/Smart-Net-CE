namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;

    using Smart.Converter;

    /// <summary>
    ///
    /// </summary>
    public static class SqlMapper
    {
        //--------------------------------------------------------------------------------
        // Config
        //--------------------------------------------------------------------------------

        private static IList<IParameterBuilder> parameterBuilders;

        private static IList<IQueryHandler> queryHandlers;

        public static ObjectConverter Converter { get; set; }

        /// <summary>
        ///
        /// </summary>
        static SqlMapper()
        {
            ResetParameterBuilders();
            ResetQueryHandlers();
            Converter = ObjectConverter.Default;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="list"></param>
        public static void SetParameterBuilders(IList<IParameterBuilder> list)
        {
            parameterBuilders = list;
        }

        /// <summary>
        ///
        /// </summary>
        public static void ResetParameterBuilders()
        {
            parameterBuilders = DefaultParameterBuilders.Create();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="list"></param>
        public static void SetQueryHandlers(IList<IQueryHandler> list)
        {
            queryHandlers = list;
        }

        /// <summary>
        ///
        /// </summary>
        public static void ResetQueryHandlers()
        {
            queryHandlers = DefaultQueryHandlers.Create();
        }

        //--------------------------------------------------------------------------------
        // Setup
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="con"></param>
        /// <param name="transaction"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:ReviewSqlQueriesForSecurityVulnerabilities", Justification = "Ignore")]
        private static IDbCommand SetupCommand(IDbConnection con, IDbTransaction transaction, string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            var cmd = con.CreateCommand();

            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }

            cmd.CommandText = sql;

            if (commandTimeout.HasValue)
            {
                cmd.CommandTimeout = commandTimeout.Value;
            }

            if (commandType.HasValue)
            {
                cmd.CommandType = commandType.Value;
            }

            if (param != null)
            {
                var builder = parameterBuilders.FirstOrDefault(_ => _.IsMatch(param));
                if (builder == null)
                {
                    throw new SqlMapperException("Parameter can't build.");
                }

                builder.BuildParameters(cmd, param);
            }

            return cmd;
        }

        //--------------------------------------------------------------------------------
        // Core
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="wasClosed"></param>
        /// <param name="con"></param>
        /// <param name="cmd"></param>
        private static void Cleanup(bool wasClosed, IDbConnection con, IDbCommand cmd)
        {
            foreach (var parameter in cmd.Parameters)
            {
                var disposable = parameter as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            if (wasClosed)
            {
                con.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="con"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private static int ExecuteImpl(this IDbConnection con, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            var wasClosed = con.State == ConnectionState.Closed;
            using (var cmd = SetupCommand(con, transaction, sql, param, commandTimeout, commandType))
            {
                try
                {
                    if (wasClosed)
                    {
                        con.Open();
                    }

                    var result = cmd.ExecuteNonQuery();

                    return result;
                }
                finally
                {
                    Cleanup(wasClosed, con, cmd);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private static T ExecuteScalarImpl<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            var wasClosed = con.State == ConnectionState.Closed;
            using (var cmd = SetupCommand(con, transaction, sql, param, commandTimeout, commandType))
            {
                try
                {
                    if (wasClosed)
                    {
                        con.Open();
                    }

                    var result = cmd.ExecuteScalar();

                    return (T)Converter.Convert(result, typeof(T));
                }
                finally
                {
                    Cleanup(wasClosed, con, cmd);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="con"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="commandBehavior"></param>
        /// <returns></returns>
        private static IDataReader ExecuteReaderImpl(this IDbConnection con, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType, CommandBehavior commandBehavior)
        {
            var wasClosed = con.State == ConnectionState.Closed;
            using (var cmd = SetupCommand(con, transaction, sql, param, commandTimeout, commandType))
            {
                try
                {
                    if (wasClosed)
                    {
                        con.Open();
                    }

                    var reader = cmd.ExecuteReader(commandBehavior);
                    wasClosed = false;

                    return reader;
                }
                finally
                {
                    Cleanup(wasClosed, con, cmd);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con"></param>
        /// <param name="factory"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private static IEnumerable<T> QueryImpl<T>(this IDbConnection con, Func<T> factory, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            var wasClosed = con.State == ConnectionState.Closed;
            using (var cmd = SetupCommand(con, transaction, sql, param, commandTimeout, commandType))
            {
                try
                {
                    var type = typeof(T);
                    var queryHandler = queryHandlers.FirstOrDefault(_ => _.IsMatch(type));
                    if (queryHandler == null)
                    {
                        throw new SqlMapperException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't handle", type.FullName));
                    }

                    if (wasClosed)
                    {
                        con.Open();
                    }

                    var reader = cmd.ExecuteReader(wasClosed ? CommandBehavior.CloseConnection | CommandBehavior.SequentialAccess : CommandBehavior.SequentialAccess);
                    wasClosed = false;

                    return queryHandler.Handle(factory, reader, Converter);
                }
                finally
                {
                    Cleanup(wasClosed, con, cmd);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Execute
        //--------------------------------------------------------------------------------

        public static int Execute(this IDbConnection con, string sql)
        {
            return ExecuteImpl(con, sql, null, null, null, null);
        }

        public static int Execute(this IDbConnection con, string sql, object param)
        {
            return ExecuteImpl(con, sql, param, null, null, null);
        }

        public static int Execute(this IDbConnection con, string sql, object param, CommandType commandType)
        {
            return ExecuteImpl(con, sql, param, null, null, commandType);
        }

        public static int Execute(this IDbConnection con, string sql, object param, int commandTimeout)
        {
            return ExecuteImpl(con, sql, param, null, commandTimeout, null);
        }

        public static int Execute(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType)
        {
            return ExecuteImpl(con, sql, param, null, commandTimeout, commandType);
        }

        public static int Execute(this IDbConnection con, string sql, object param, IDbTransaction transaction)
        {
            return ExecuteImpl(con, sql, param, transaction, null, null);
        }

        public static int Execute(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType)
        {
            return ExecuteImpl(con, sql, param, transaction, null, commandType);
        }

        public static int Execute(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout)
        {
            return ExecuteImpl(con, sql, param, transaction, commandTimeout, null);
        }

        public static int Execute(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType)
        {
            return ExecuteImpl(con, sql, param, transaction, commandTimeout, commandType);
        }

        //--------------------------------------------------------------------------------
        // ExecuteScalar
        //--------------------------------------------------------------------------------

        public static T ExecuteScalar<T>(this IDbConnection con, string sql)
        {
            return ExecuteScalarImpl<T>(con, sql, null, null, null, null);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param)
        {
            return ExecuteScalarImpl<T>(con, sql, param, null, null, null);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, CommandType commandType)
        {
            return ExecuteScalarImpl<T>(con, sql, param, null, null, commandType);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, int commandTimeout)
        {
            return ExecuteScalarImpl<T>(con, sql, param, null, commandTimeout, null);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType)
        {
            return ExecuteScalarImpl<T>(con, sql, param, null, commandTimeout, commandType);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction)
        {
            return ExecuteScalarImpl<T>(con, sql, param, transaction, null, null);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType)
        {
            return ExecuteScalarImpl<T>(con, sql, param, transaction, null, commandType);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout)
        {
            return ExecuteScalarImpl<T>(con, sql, param, transaction, commandTimeout, null);
        }

        public static T ExecuteScalar<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType)
        {
            return ExecuteScalarImpl<T>(con, sql, param, transaction, commandTimeout, commandType);
        }

        //--------------------------------------------------------------------------------
        // ExecuteReader
        //--------------------------------------------------------------------------------

        public static IDataReader ExecuteReader(this IDbConnection con, string sql)
        {
            return ExecuteReaderImpl(con, sql, null, null, null, null, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param)
        {
            return ExecuteReaderImpl(con, sql, param, null, null, null, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, CommandType commandType)
        {
            return ExecuteReaderImpl(con, sql, param, null, null, commandType, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, int commandTimeout)
        {
            return ExecuteReaderImpl(con, sql, param, null, commandTimeout, null, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType)
        {
            return ExecuteReaderImpl(con, sql, param, null, commandTimeout, commandType, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, null, null, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, null, commandType, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, commandTimeout, null, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, commandTimeout, commandType, CommandBehavior.Default);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, null, null, null, null, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, null, null, null, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, CommandType commandType, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, null, null, commandType, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, int commandTimeout, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, null, commandTimeout, null, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, null, commandTimeout, commandType, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, null, null, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, null, commandType, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, commandTimeout, null, commandBehavior);
        }

        public static IDataReader ExecuteReader(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType, CommandBehavior commandBehavior)
        {
            return ExecuteReaderImpl(con, sql, param, transaction, commandTimeout, commandType, commandBehavior);
        }

        //--------------------------------------------------------------------------------
        // Query
        //--------------------------------------------------------------------------------

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql)
        {
            return QueryImpl(con, factory, sql, null, null, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param)
        {
            return QueryImpl(con, factory, sql, param, null, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, CommandType commandType)
        {
            return QueryImpl(con, factory, sql, param, null, null, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, int commandTimeout)
        {
            return QueryImpl(con, factory, sql, param, null, commandTimeout, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, int commandTimeout, CommandType commandType)
        {
            return QueryImpl(con, factory, sql, param, null, commandTimeout, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, IDbTransaction transaction)
        {
            return QueryImpl(con, factory, sql, param, transaction, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, IDbTransaction transaction, CommandType commandType)
        {
            return QueryImpl(con, factory, sql, param, transaction, null, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, IDbTransaction transaction, int commandTimeout)
        {
            return QueryImpl(con, factory, sql, param, transaction, commandTimeout, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, Func<T> factory, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType)
        {
            return QueryImpl(con, factory, sql, param, transaction, commandTimeout, commandType);
        }

        // where T : new()

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, null, null, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, null, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, CommandType commandType) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, null, null, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, int commandTimeout) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, null, commandTimeout, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, null, commandTimeout, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, transaction, null, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, transaction, null, commandType);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, transaction, commandTimeout, null);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType) where T : new()
        {
            return QueryImpl(con, () => new T(), sql, param, transaction, commandTimeout, commandType);
        }

        // Dictionary<string, object>

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, null, null, null, null);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, null, null, null);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, CommandType commandType)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, null, null, commandType);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, int commandTimeout)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, null, commandTimeout, null);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, int commandTimeout, CommandType commandType)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, null, commandTimeout, commandType);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, IDbTransaction transaction)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, transaction, null, null);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, IDbTransaction transaction, CommandType commandType)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, transaction, null, commandType);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, transaction, commandTimeout, null);
        }

        public static IEnumerable<Dictionary<string, object>> Query(this IDbConnection con, string sql, object param, IDbTransaction transaction, int commandTimeout, CommandType commandType)
        {
            return QueryImpl(con, () => new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase), sql, param, transaction, commandTimeout, commandType);
        }
    }
}
