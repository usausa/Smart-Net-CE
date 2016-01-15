namespace Smart.Drawing
{
    using System;
    using System.Drawing;

    /// <summary>
    /// 
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ToWin32(this Color color)
        {
            return (color.B << 16) | (color.G << 8) | color.R;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color Blend(this Color color1, Color color2, float alpha)
        {
            var r3 = Convert.ToByte(color1.R + ((color2.R - color1.R) * alpha));
            var g3 = Convert.ToByte(color1.G + ((color2.G - color1.G) * alpha));
            var b3 = Convert.ToByte(color1.B + ((color2.B - color1.B) * alpha));
            return Color.FromArgb(r3, g3, b3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color Invert(this Color color)
        {
            return Color.FromArgb(0xff - color.R, 0xff - color.G, 0xff - color.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GrayScale(this Color color)
        {
            var gray = ((color.R * 306) + (color.G * 601) + (color.B * 117)) >> 10;
            return Color.FromArgb(gray, gray, gray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetBrightness(this Color color)
        {
            var r = color.R / 255.0f;
            var g = color.G / 255.0f;
            var b = color.B / 255.0f;

            var max = r;
            var min = r;

            if (g > max)
            {
                max = g;
            }
            if (b > max)
            {
                max = b;
            }

            if (g < min)
            {
                min = g;
            }
            if (b < min)
            {
                min = b;
            }

            return (max + min) / 2;
        }
    }
}