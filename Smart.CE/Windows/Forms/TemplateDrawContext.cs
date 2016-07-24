namespace Smart.Windows.Forms
{
    using System.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class TemplateDrawContext
    {
        /// <summary>
        /// 
        /// </summary>
        public bool PreferBaseForeColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool PreferBaseBackColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool PreferBaseBorderColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color BaseForeColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color BaseBackColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color BaseBorderColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Font Font { get; set; }
    }
}
