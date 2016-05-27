namespace Smart.Windows.Forms
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class CreateParams
    {
        public string Caption { get; set; }

        public string ClassName { get; set; }

        public int ClassStyle { get; set; }

        public int ExStyle { get; set; }

        public int Height { get; set; }

        public IntPtr Param { get; set; }

        public IntPtr Parent { get; set; }

        public int Style { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        /// <summary>
        ///
        /// </summary>
        public CreateParams()
        {
            Caption = string.Empty;
            ClassName = string.Empty;
            Param = IntPtr.Zero;
            Parent = IntPtr.Zero;
        }
    }
}
