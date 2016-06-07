namespace Smart.Windows.Forms
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="form"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void CenterScreen(this Form form)
        {
            var x = (Screen.PrimaryScreen.Bounds.Width - form.Width) / 2;
            var y = (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2;
            form.Location = new Point(x, y);
        }
    }
}
