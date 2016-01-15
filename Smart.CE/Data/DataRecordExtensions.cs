namespace Smart.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool? GetNullableBoolean(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (bool?)record.GetBoolean(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static byte? GetNullableByte(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (byte?)record.GetByte(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static char? GetNullableChar(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (char?)record.GetChar(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static DateTime? GetNullableDateTime(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (DateTime?)record.GetDateTime(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Decimal? GetNullableDecimal(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (Decimal?)record.GetDecimal(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static double? GetNullableDouble(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (double?)record.GetDouble(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static float? GetNullableFloat(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (float?)record.GetFloat(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Guid? GetNullableGuid(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (Guid?)record.GetGuid(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static short? GetNullableInt16(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (short?)record.GetInt32(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int? GetNullableInt32(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (int?)record.GetInt32(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static long? GetNullableInt64(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (long?)record.GetInt64(i);
        }
    }
}
