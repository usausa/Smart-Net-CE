namespace Smart.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public class FormatConverter : Component, ITemplateConverter<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NullValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FormatConverter()
        {
            NullValue = string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Convert(object source)
        {
            if (source == null)
            {
                return NullValue;
            }

            var formattable = source as IFormattable;
            return formattable == null ? source.ToString() : formattable.ToString(Format, CultureInfo.CurrentUICulture);
        }
    }
}
