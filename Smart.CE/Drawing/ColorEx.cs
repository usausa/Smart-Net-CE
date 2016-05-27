namespace Smart.Drawing
{
    using System;
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    public static class ColorEx
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color FromKnownColor(string color)
        {
            var pi = typeof(Color).GetProperty(color);
            return pi == null ? Color.Empty : (Color)pi.GetValue(null, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static Color FromHtml(string html)
        {
            var color = Color.Empty;

            if ((!string.IsNullOrEmpty(html) &&
                (html[0] == '#')) &&
                ((html.Length == 7) || (html.Length == 4)))
            {
                if (html.Length == 7)
                {
                    color = Color.FromArgb(
                        Convert.ToInt32(html.Substring(1, 2), 16),
                        Convert.ToInt32(html.Substring(3, 2), 16),
                        Convert.ToInt32(html.Substring(5, 2), 16));
                }
                else
                {
                    var str = char.ToString(html[1]);
                    var str2 = char.ToString(html[2]);
                    var str3 = char.ToString(html[3]);
                    color = Color.FromArgb(
                        Convert.ToInt32(str + str, 16),
                        Convert.ToInt32(str2 + str2, 16),
                        Convert.ToInt32(str3 + str3, 16));
                }
            }

            if (color.IsEmpty)
            {
                color = FromKnownColor(html);
            }

            return color;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtml(Color color)
        {
            return string.Format(CultureInfo.InvariantCulture, "#{0:X6}", ((color.R << 16) + (color.G << 8)) + color.B);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color FromWin32(int color)
        {
            return Color.FromArgb(color & 0xff, (color >> 8) & 0xff, (color >> 16) & 0xff);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ToWin32(Color color)
        {
            return color.R + (color.G << 8) + (color.B << 16);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="position"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Color InterpolateLinear(Color first, Color second, float position, float start, float end)
        {
            var r = ((second.R * (position - start)) + (first.R * (end - position))) / (end - start);
            var g = ((second.G * (position - start)) + (first.G * (end - position))) / (end - start);
            var b = ((second.B * (position - start)) + (first.B * (end - position))) / (end - start);
            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }
    }
}
