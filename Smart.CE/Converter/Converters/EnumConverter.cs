namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class EnumConverter : IObjectConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        public bool IsMatch(object value, Type targetType)
        {
            return targetType.IsEnum;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, ObjectConverter converter)
        {
            if (value == null)
            {
                return DefaultValue.Of(targetType);
            }

            var str = value as string;
            if (str != null)
            {
                try
                {
                    return Enum.Parse(targetType, str, false);
                }
                catch (ArgumentException)
                {
                    return DefaultValue.Of(targetType);
                }
            }

            if (value is Enum)
            {
#if WindowsCE
                return Enum.Parse(targetType, EnumEx.GetName(value.GetType(), value), false);
#else
                return Enum.Parse(targetType, Enum.GetName(value.GetType(), value), false);
#endif
            }

            return Enum.Parse(targetType, value.ToString(), false);
        }
    }
}
