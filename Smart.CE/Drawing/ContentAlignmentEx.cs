namespace Smart.Drawing
{
    using System;
    using System.Drawing;

    /// <summary>
    ///
    /// </summary>
    [Flags]
    public enum ContentAlignmentEx
    {
        BottomCenter    = 0x200,
        BottomLeft      = 0x100,
        BottomRight     = 0x400,
        MiddleCenter    = 0x020,
        MiddleLeft      = 0x010,
        MiddleRight     = 0x040,
        TopCenter       = 0x002,
        TopLeft         = 0x001,
        TopRight        = 0x004
    }

    /// <summary>
    ///
    /// </summary>
    public static class ContentAlignmentExtensions
    {
        //--------------------------------------------------------------------------------
        // Alignment
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsTop(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.TopLeft) || (alignment == ContentAlignmentEx.TopCenter) || (alignment == ContentAlignmentEx.TopRight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsMiddle(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.MiddleLeft) || (alignment == ContentAlignmentEx.MiddleCenter) || (alignment == ContentAlignmentEx.MiddleRight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsBottom(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.BottomLeft) || (alignment == ContentAlignmentEx.BottomCenter) || (alignment == ContentAlignmentEx.BottomRight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsLeft(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.TopLeft) || (alignment == ContentAlignmentEx.MiddleLeft) || (alignment == ContentAlignmentEx.BottomLeft);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsCenter(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.TopCenter) || (alignment == ContentAlignmentEx.MiddleCenter) || (alignment == ContentAlignmentEx.BottomCenter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static bool IsRight(this ContentAlignmentEx alignment)
        {
            return (alignment == ContentAlignmentEx.TopRight) || (alignment == ContentAlignmentEx.MiddleRight) || (alignment == ContentAlignmentEx.BottomRight);
        }

        //--------------------------------------------------------------------------------
        // Calc
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="textAlign"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectangleF CalcTextRect(this ContentAlignmentEx textAlign, float sx, float sy, float x, float y, float width, float height)
        {
            if (textAlign.IsCenter())
            {
                x += (width - sx) / 2;
            }
            else if (textAlign.IsRight())
            {
                x += width - sx;
            }

            if (textAlign.IsMiddle())
            {
                y += (height - sy) / 2;
            }
            else if (textAlign.IsBottom())
            {
                y += height - sy;
            }

            return new RectangleF(x, y, sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textAlign"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectangleF CalcTextRect(this ContentAlignmentEx textAlign, float sx, float sy, RectangleF rect)
        {
            return textAlign.CalcTextRect(sx, sy, rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textAlign"></param>
        /// <param name="sizeText"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectangleF CalcTextRect(this ContentAlignmentEx textAlign, SizeF sizeText, float x, float y, float width, float height)
        {
            return textAlign.CalcTextRect(sizeText.Width, sizeText.Height, x, y, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="textAlign"></param>
        /// <param name="sizeText"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectangleF CalcTextRect(this ContentAlignmentEx textAlign, SizeF sizeText, RectangleF rect)
        {
            return textAlign.CalcTextRect(sizeText.Width, sizeText.Height, rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}