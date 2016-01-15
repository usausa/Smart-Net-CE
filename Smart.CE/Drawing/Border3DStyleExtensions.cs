namespace Smart.Drawing
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public static class Border3DStyleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int CalcBorderWidth(this Border3DStyle style)
        {
            switch (style)
            {
                case Border3DStyle.Flat:
                    return 1;
                case Border3DStyle.Raised:
                    return 2;
                case Border3DStyle.RaisedInner:
                    return 1;
                case Border3DStyle.Sunken:
                    return 2;
                case Border3DStyle.SunkenOuter:
                    return 1;
            }

            throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "Style {0} is not supported.", style));
        }
    }
}
