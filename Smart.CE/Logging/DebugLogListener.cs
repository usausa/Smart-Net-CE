namespace Smart.Logging
{
    using System.Diagnostics;

    /// <summary>
    ///
    /// </summary>
    public class DebugLogListener : LogListener
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            if (NeedIndent)
            {
                WriteIndent();
            }
            Debug.Write(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            if (NeedIndent)
            {
                WriteIndent();
            }
            Debug.WriteLine(message);

            NeedIndent = true;
        }
    }
}
