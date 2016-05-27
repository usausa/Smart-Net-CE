namespace Smart.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    ///
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Rectangle GetBounds(this Image image)
        {
            return new Rectangle(0, 0, image.Width, image.Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static byte[] ToBytes(this Image image, ImageFormat format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap ScaleToSize(this Image image, Size size)
        {
            return ScaleToSize(image, size.Width, size.Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Factory")]
        public static Bitmap ScaleToSize(this Image image, int height, int width)
        {
            var bmp = new Bitmap(height, width);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawImage(image, new Rectangle(0, 0, height, width), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return bmp;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap ScaleProportional(this Image image, Size size)
        {
            return ScaleProportional(image, size.Width, size.Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Bitmap ScaleProportional(this Image image, int width, int height)
        {
            float proportionalWidth;
            float proportionalHeight;
            if (width == 0)
            {
                proportionalWidth = ((float)height) / image.Size.Height * image.Width;
                proportionalHeight = height;
            }
            else if (height == 0)
            {
                proportionalWidth = width;
                proportionalHeight = ((float)width) / image.Size.Width * image.Height;
            }
            else if (((float)width) / image.Size.Width * image.Size.Height <= height)
            {
                proportionalWidth = width;
                proportionalHeight = ((float)width) / image.Size.Width * image.Height;
            }
            else
            {
                proportionalWidth = ((float)height) / image.Size.Height * image.Width;
                proportionalHeight = height;
            }
            return ScaleToSize(image, (int)proportionalWidth, (int)proportionalHeight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap ScaleToSizeProportional(this Image image, Size size)
        {
            return ScaleToSizeProportional(image, Color.White, size);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="backColor"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap ScaleToSizeProportional(this Image image, Color backColor, Size size)
        {
            return ScaleToSizeProportional(image, backColor, size.Width, size.Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap ScaleToSizeProportional(this Image image, int width, int height)
        {
            return ScaleToSizeProportional(image, Color.White, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="backColor"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Factory")]
        public static Bitmap ScaleToSizeProportional(this Image image, Color backColor, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backColor);
                var proportionalBitmap = image.ScaleProportional(width, height);
                g.DrawImage(proportionalBitmap, (width - proportionalBitmap.Width) / 2, (height - proportionalBitmap.Height) / 2);
            }
            return bmp;
        }
    }
}
