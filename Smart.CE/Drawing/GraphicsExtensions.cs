namespace Smart.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// 
    /// </summary>
    public static class GraphicsExtensions
    {
        //--------------------------------------------------------------------------------
        // Multiline
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static string[] GetMultilineText(this Graphics g, string text, Font font, float width)
        {
            var lines = new List<string>();

            foreach (var line in text.Split('\n'))
            {
                if (line.Length == 0)
                {
                    lines.Add(line);
                }
                else
                {
                    var str = line;
                    while (str.Length > 0)
                    {
                        for (var i = str.Length; i > 0; i--)
                        {
                            var substr = str.Substring(0, i);
                            var size = g.MeasureString(substr, font);
                            if (size.Width <= width)
                            {
                                lines.Add(substr);
                                str = str.Substring(i);
                                break;
                            }
                            
                            if (i == 1)
                            {
                                lines.Add(str.Substring(0, 1));
                                str = str.Substring(1);
                            }
                        }
                    }
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="texts"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static SizeF CalcMultilineTextSize(this Graphics g, string[] texts, Font font)
        {
            var totalWidth = 0;
            var totalHeight = 0;
            foreach (var line in texts)
            {
                var size = g.MeasureString(String.IsNullOrEmpty(line) ? " " : line, font);

                var width = (int)Math.Ceiling(size.Width);
                var height = (int)Math.Ceiling(size.Height);

                if (width > totalWidth)
                {
                    totalWidth = width;
                }
                totalHeight += height;
            }

            return new SizeF(totalWidth, totalHeight);
        }

        //--------------------------------------------------------------------------------
        // Text
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="textAlign"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void DrawText(this Graphics g, string text, Font font, Color color, float x, float y, float width, float height, ContentAlignmentEx textAlign)
        {
            var rect = textAlign.CalcTextRect(g.MeasureString(text, font), x, y, width, height);
            using (var brush = new SolidBrush(color))
            {
                g.DrawString(text, font, brush, rect.X, rect.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="rect"></param>
        /// <param name="textAlign"></param>
        public static void DrawText(this Graphics g, string text, Font font, Color color, RectangleF rect, ContentAlignmentEx textAlign)
        {
            DrawText(g, text, font, color, rect.Left, rect.Top, rect.Width, rect.Height, textAlign);
        }

        //--------------------------------------------------------------------------------
        // Shadow
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mask"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void DrawShadow(this Graphics g, string text, Font font, Color color, float x, float y, ShadowMask mask)
        {
            using (var brush = new SolidBrush(color))
            {
                if ((mask & ShadowMask.TopLeft) != 0)
                {
                    g.DrawString(text, font, brush, x - 1, y - 1);
                }
                if ((mask & ShadowMask.TopCenter) != 0)
                {
                    g.DrawString(text, font, brush, x, y - 1);
                }
                if ((mask & ShadowMask.TopRight) != 0)
                {
                    g.DrawString(text, font, brush, x + 1, y - 1);
                }
                if ((mask & ShadowMask.MiddleLeft) != 0)
                {
                    g.DrawString(text, font, brush, x - 1, y);
                }
                if ((mask & ShadowMask.MiddleRight) != 0)
                {
                    g.DrawString(text, font, brush, x + 1, y);
                }
                if ((mask & ShadowMask.BottomLeft) != 0)
                {
                    g.DrawString(text, font, brush, x - 1, y + 1);
                }
                if ((mask & ShadowMask.BottomCenter) != 0)
                {
                    g.DrawString(text, font, brush, x, y + 1);
                }
                if ((mask & ShadowMask.BottomRight) != 0)
                {
                    g.DrawString(text, font, brush, x + 1, y + 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="rect"></param>
        /// <param name="textAlign"></param>
        /// <param name="mask"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void DrawShadow(this Graphics g, string text, Font font, Color color, RectangleF rect, ContentAlignmentEx textAlign, ShadowMask mask)
        {
            var rc = textAlign.CalcTextRect(g.MeasureString(text, font), rect);
            DrawShadow(g, text, font, color, (int)rc.X, (int)rc.Y, mask);
        }

        //--------------------------------------------------------------------------------
        // Border
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static SolidBrush GetNormalBrush(Color color)
        {
            return new SolidBrush(color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static SolidBrush GetLightBrush(Color color)
        {
            return color.GetBrightness() > 0.6 ? new SolidBrush(color.Blend(Color.White, 1)) : new SolidBrush(color.Blend(Color.White, 0.5f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static SolidBrush GetDarkBrush(Color color)
        {
            return color.GetBrightness() < 0.5 ? new SolidBrush(color.Blend(Color.Black, 0.7f)) : new SolidBrush(color.Blend(Color.Black, 0.4f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static SolidBrush GetDarkDarkBrush(Color color)
        {
            return color.GetBrightness() < 0.5 ? new SolidBrush(color.Blend(Color.Black, 1)) : new SolidBrush(color.Blend(Color.Black, 0.6f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawBorderFlat(this Graphics g, Color color, int x, int y, int width, int height)
        {
            using (var dark = GetDarkBrush(color))
            {
                g.FillRectangle(dark, x, y, width - 1, 1);
                g.FillRectangle(dark, x, y, 1, height - 1);

                g.FillRectangle(dark, x + width - 1, y, 1, height);
                g.FillRectangle(dark, x, y + height - 1, width, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawBorderRaised(this Graphics g, Color color, int x, int y, int width, int height)
        {
            using (var normal = GetNormalBrush(color))
            using (var light = GetLightBrush(color))
            using (var dark = GetDarkBrush(color))
            using (var darkdark = GetDarkDarkBrush(color))
            {
                g.FillRectangle(normal, x, y, width - 1, 1);
                g.FillRectangle(normal, x, y, 1, height - 1);
                g.FillRectangle(light, x + 1, y + 1, width - 2, 1);
                g.FillRectangle(light, x + 1, y + 1, 1, height - 2);

                g.FillRectangle(darkdark, x + width - 1, y, 1, height);
                g.FillRectangle(darkdark, x, y + height - 1, width, 1);
                g.FillRectangle(dark, x + width - 2, y + 1, 1, height - 2);
                g.FillRectangle(dark, x + 1, y + height - 2, width - 2, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawBorderRaisedInner(this Graphics g, Color color, int x, int y, int width, int height)
        {
            using (var light = GetLightBrush(color))
            using (var dark = GetDarkBrush(color))
            {
                g.FillRectangle(light, x, y, width - 1, 1);
                g.FillRectangle(light, x, y, 1, height - 1);

                g.FillRectangle(dark, x + width - 1, y, 1, height);
                g.FillRectangle(dark, x, y + height - 1, width, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawBorderSunken(this Graphics g, Color color, int x, int y, int width, int height)
        {
            using (var normal = GetNormalBrush(color))
            using (var light = GetLightBrush(color))
            using (var dark = GetDarkBrush(color))
            using (var darkdark = GetDarkDarkBrush(color))
            {
                g.FillRectangle(dark, x, y, width, 1);
                g.FillRectangle(dark, x, y, 1, height);
                g.FillRectangle(darkdark, x + 1, y + 1, width - 2, 1);
                g.FillRectangle(darkdark, x + 1, y + 1, 1, height - 2);

                g.FillRectangle(light, x + width - 1, y + 1, 1, height - 1);
                g.FillRectangle(light, x + 1, y + height - 1, width - 1, 1);
                g.FillRectangle(normal, x + width - 2, y + 2, 1, height - 3);
                g.FillRectangle(normal, x + 2, y + height - 2, width - 3, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawBorderSunkenOuter(this Graphics g, Color color, int x, int y, int width, int height)
        {
            using (var light = GetLightBrush(color))
            using (var dark = GetDarkBrush(color))
            {
                g.FillRectangle(dark, x, y, width, 1);
                g.FillRectangle(dark, x, y, 1, height);

                g.FillRectangle(light, x + width - 1, y + 1, 1, height - 1);
                g.FillRectangle(light, x + 1, y + height - 1, width - 1, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void DrawBorder(this Graphics g, Border3DStyle style, Color color, int x, int y, int width, int height)
        {
            switch (style)
            {
                case Border3DStyle.Flat:
                    DrawBorderFlat(g, color, x, y, width, height);
                    break;
                case Border3DStyle.Raised:
                    DrawBorderRaised(g, color, x, y, width, height);
                    break;
                case Border3DStyle.RaisedInner:
                    DrawBorderRaisedInner(g, color, x, y, width, height);
                    break;
                case Border3DStyle.Sunken:
                    DrawBorderSunken(g, color, x, y, width, height);
                    break;
                case Border3DStyle.SunkenOuter:
                    DrawBorderSunkenOuter(g, color, x, y, width, height);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        /// <param name="rect"></param>
        public static void DrawBorder(this Graphics g, Border3DStyle style, Color color, Rectangle rect)
        {
            DrawBorder(g, style, color, rect.X, rect.Y, rect.Width, rect.Height);
        }

        //--------------------------------------------------------------------------------
        // CopyGraphics
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="gxDest"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void CopyGraphics(this Graphics g, Graphics gxDest, int width, int height)
        {
            CopyGraphics(g, gxDest, width, height, RasterOp.SRCCOPY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="gxDest"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="op"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void CopyGraphics(this Graphics g, Graphics gxDest, int width, int height, RasterOp op)
        {
            var srcDc = g.GetHdc();
            var destDc = gxDest.GetHdc();
            NativeMethods.BitBlt(destDc, 0, 0, width, height, srcDc, 0, 0, op);
            gxDest.ReleaseHdc(destDc);
            g.ReleaseHdc(srcDc);
        }

        //--------------------------------------------------------------------------------
        // Fill
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="op"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void FillColor(this Graphics g, Rectangle rect, Color color, RasterOp op)
        {
            var hdc = g.GetHdc();
            var hBrush = NativeMethods.CreateSolidBrush(color.ToWin32());
            var hOldBrush = NativeMethods.SelectObject(hdc, hBrush);

            NativeMethods.PatBlt(hdc, rect.Left, rect.Top, rect.Width, rect.Height, op);

            NativeMethods.SelectObject(hdc, hOldBrush);
            NativeMethods.DeleteObject(hBrush);
            g.ReleaseHdc(hdc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <param name="startColor"></param>
        /// <param name="endColor"></param>
        /// <param name="fillDir"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool GradientFill(this Graphics gr, Rectangle rc, Color startColor, Color endColor, FillDirection fillDir)
        {
            if (DesignMode.IsTrue)
            {
                using (var pen = new Pen(startColor))
                {
                    if (fillDir == FillDirection.LeftToRight)
                    {
                        for (var x = rc.Left; x < rc.Right; x++)
                        {
                            pen.Color = ColorEx.InterpolateLinear(startColor, endColor, x, 0, rc.Width);
                            gr.DrawLine(pen, x, rc.Top, x, rc.Bottom - 1);
                        }
                    }
                    if (fillDir == FillDirection.TopToBottom)
                    {
                        for (var y = rc.Top; y < rc.Bottom; y++)
                        {
                            pen.Color = ColorEx.InterpolateLinear(startColor, endColor, y, 0, rc.Height);
                            gr.DrawLine(pen, rc.Left, y, rc.Right - 1, y);
                        }
                    }
                }

                return false;
            }

            var tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rc.X, rc.Y, startColor);
            tva[1] = new TRIVERTEX(rc.Right, rc.Bottom, endColor);
            var gra = new[] { new GRADIENT_RECT(0, 1) };

            var hdc = gr.GetHdc();
            var ret = NativeMethods.GradientFill(hdc, tva, (uint)tva.Length, gra, (uint)gra.Length, (uint)fillDir);
            gr.ReleaseHdc(hdc);

            return ret;
        }

        //--------------------------------------------------------------------------------
        // Transparent
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Color GetTransparentColor(Image image)
        {
            return ((Bitmap)image).GetPixel(image.Width - 1, image.Height - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="transparency"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void DrawAlpha(this Graphics g, Image image, byte transparency, int x, int y)
        {
            using (var gSrc = Graphics.FromImage(image))
            {
                var hdcDst = g.GetHdc();
                var hdcSrc = gSrc.GetHdc();
                
                var blendFunction = new BlendFunction
                {
                    BlendOp = 0x00, // AC_SRC_OVER
                    BlendFlags = 0x00,
                    SourceConstantAlpha = transparency,
                    AlphaFormat = 0
                };
                NativeMethods.AlphaBlend(hdcDst, x, y, image.Width, image.Height, hdcSrc, 0, 0, image.Width, image.Height, blendFunction);
                
                g.ReleaseHdc(hdcDst);
                gSrc.ReleaseHdc(hdcSrc);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="destRect"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void DrawImageTransparent(this Graphics g, Image image, Rectangle destRect)
        {
            using (var imageAttr = new ImageAttributes())
            {
                var transpColor = GetTransparentColor(image);
                imageAttr.SetColorKey(transpColor, transpColor);
                g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }
    }
}
