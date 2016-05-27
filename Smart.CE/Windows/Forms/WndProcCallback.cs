namespace Smart.Windows.Forms
{
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
    public delegate bool WndProcCallback(ref Message message);
}
