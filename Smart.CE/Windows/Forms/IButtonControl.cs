namespace Smart.Windows.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public interface IButtonControl
    {
        DialogResult DialogResult { get; set; }

        void NotifyDefault(bool value);

        void PerformClick();
    }
}
