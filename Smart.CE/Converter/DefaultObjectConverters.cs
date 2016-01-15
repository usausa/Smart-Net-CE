namespace Smart.Converter
{
    using System.Collections.Generic;

    using Smart.Converter.Converters;

    /// <summary>
    /// 
    /// </summary>
    public static class DefaultObjectConverters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IList<IObjectConverter> Create()
        {
            return new List<IObjectConverter>
            {
                new EnumConverter(),                // Enumへの変換
                new ConvertibleConverter(),         // IConvertibleへの変換なら
                new NullableConverter(),            // NulllableはIConvertibleではないので
                new SqlTypesNullableConverter(),    // INullableへの変換なら
                new NullConverter(),                // 元の値がnullなら
                new DBNullConverter(),              // 元の値がDBNullの場合にnullと同様に
                new AssignableConverter(),          // 代入を試みる
                new StringConverter(),              // 最後に文字列化を試す
            };
        }
    }
}
