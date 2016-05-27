namespace Smart.Windows.Forms
{
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    ///
    /// </summary>
    public interface IMessageFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
        bool PreFilterMessage(ref Message m);
    }
}
