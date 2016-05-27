namespace Smart.Drawing
{
    using System.Drawing;

    public static class IconExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Factory")]
        public static Bitmap ToBitmap(this Icon icon)
        {
            var bmp = new Bitmap(icon.Width, icon.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawIcon(icon, 0, 0);
            }
            return bmp;
        }
    }
}
