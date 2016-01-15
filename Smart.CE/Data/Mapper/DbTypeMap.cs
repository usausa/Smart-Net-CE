namespace Smart.Data.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public class DbTypeMap
    {
        private static readonly DbTypeMap Instance = new DbTypeMap();

        private Dictionary<Type, DbType> typeMap;

        /// <summary>
        /// 
        /// </summary>
        public static DbTypeMap Default
        {
            get { return Instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbTypeMap()
        {
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dbType"></param>
        public void Add(Type type, DbType dbType)
        {
            var snapshot = typeMap;

            DbType oldValue;
            if (snapshot.TryGetValue(type, out oldValue) && oldValue == dbType)
            {
                return;
            }

            var newCopy = new Dictionary<Type, DbType>(snapshot);
            newCopy[type] = dbType;
            typeMap = newCopy; 
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            var snapshot = new Dictionary<Type, DbType>();

            snapshot[typeof(byte)] = DbType.Byte;
            snapshot[typeof(sbyte)] = DbType.SByte;
            snapshot[typeof(short)] = DbType.Int16;
            snapshot[typeof(ushort)] = DbType.UInt16;
            snapshot[typeof(int)] = DbType.Int32;
            snapshot[typeof(uint)] = DbType.UInt32;
            snapshot[typeof(long)] = DbType.Int64;
            snapshot[typeof(ulong)] = DbType.UInt64;
            snapshot[typeof(float)] = DbType.Single;
            snapshot[typeof(double)] = DbType.Double;
            snapshot[typeof(decimal)] = DbType.Decimal;
            snapshot[typeof(bool)] = DbType.Boolean;
            snapshot[typeof(string)] = DbType.String;
            snapshot[typeof(char)] = DbType.StringFixedLength;
            snapshot[typeof(Guid)] = DbType.Guid;
            snapshot[typeof(DateTime)] = DbType.DateTime;
            snapshot[typeof(TimeSpan)] = DbType.Time;
            snapshot[typeof(byte[])] = DbType.Binary;
            snapshot[typeof(byte?)] = DbType.Byte;
            snapshot[typeof(sbyte?)] = DbType.SByte;
            snapshot[typeof(short?)] = DbType.Int16;
            snapshot[typeof(ushort?)] = DbType.UInt16;
            snapshot[typeof(int?)] = DbType.Int32;
            snapshot[typeof(uint?)] = DbType.UInt32;
            snapshot[typeof(long?)] = DbType.Int64;
            snapshot[typeof(ulong?)] = DbType.UInt64;
            snapshot[typeof(float?)] = DbType.Single;
            snapshot[typeof(double?)] = DbType.Double;
            snapshot[typeof(decimal?)] = DbType.Decimal;
            snapshot[typeof(bool?)] = DbType.Boolean;
            snapshot[typeof(char?)] = DbType.StringFixedLength;
            snapshot[typeof(Guid?)] = DbType.Guid;
            snapshot[typeof(DateTime?)] = DbType.DateTime;
            snapshot[typeof(TimeSpan?)] = DbType.Time;
            snapshot[typeof(object)] = DbType.Object;

            typeMap = snapshot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbType LookupDbType(object value)
        {
            if ((value == null) || (value is DBNull))
            {
                return DbType.Object;
            }

            var type = value.GetType();

            var nullUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullUnderlyingType != null)
            {
                type = nullUnderlyingType;
            }

            var snapShot = typeMap;

            if (type.IsEnum && !snapShot.ContainsKey(type))
            {
                type = Enum.GetUnderlyingType(type);
            }

            DbType dbType;
            if (snapShot.TryGetValue(type, out dbType))
            {
                return dbType;
            }

            throw new SqlMapperException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't be used", type.FullName));
        }
    }
}
