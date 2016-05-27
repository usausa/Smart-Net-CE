namespace Smart.Drawing
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [Flags]
    public enum ShadowMask
    {
        TopLeft      = 0x80,
        TopCenter    = 0x40,
        TopRight     = 0x20,
        MiddleLeft   = 0x10,
        MiddleRight  = 0x08,
        BottomLeft   = 0x04,
        BottomCenter = 0x02,
        BottomRight  = 0x01,

        None         = 0x00,
        Full         = 0xFF,
    }

    /// <summary>
    ///
    /// </summary>
    public static class ShadowMaskExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsTop(this ShadowMask mask)
        {
            return (mask == ShadowMask.TopLeft) || (mask == ShadowMask.TopCenter) || (mask == ShadowMask.TopRight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsBottom(this ShadowMask mask)
        {
            return (mask == ShadowMask.BottomLeft) || (mask == ShadowMask.BottomCenter) || (mask == ShadowMask.BottomRight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsLeft(this ShadowMask mask)
        {
            return (mask == ShadowMask.TopLeft) || (mask == ShadowMask.MiddleLeft) || (mask == ShadowMask.BottomLeft);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsRight(this ShadowMask mask)
        {
            return (mask == ShadowMask.TopRight) || (mask == ShadowMask.MiddleRight) || (mask == ShadowMask.BottomRight);
        }
    }
}
