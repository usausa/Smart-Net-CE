namespace Smart.Drawing
{
    using System;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="render"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Factory")]
        public static Bitmap CreateBitmap(int width, int height, Action<Graphics> render)
        {
            if (render == null)
            {
                throw new ArgumentNullException("render");
            }

            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                render(g);
            }
            return bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Bitmap LoadFromFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                return new Bitmap(fs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Bitmap LoadFromBuffer(this byte[] buffer)
        {
            using (var fs = new MemoryStream(buffer))
            {
                return new Bitmap(fs);
            }
        }
    }
}
