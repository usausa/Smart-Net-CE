namespace Smart.Windows.Forms
{
    using System;
    using System.Windows.Forms;
    using Microsoft.WindowsCE.Forms;
    using Smart.Win32;

    /// <summary>
    /// 
    /// </summary>
    public class InactivityFilter : IMessageFilter, IDisposable
    {
        public event EventHandler InactivityElapsed;

        private readonly Timer timer = new Timer();

        /// <summary>
        /// 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Elapsed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutMilliseconds"></param>
        public InactivityFilter(int timeoutMilliseconds)
        {
            timer.Interval = timeoutMilliseconds;
            timer.Tick += OnTimerTick;
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        ~InactivityFilter()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            timer.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            Elapsed = true;

            var handler = InactivityElapsed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            Elapsed = false;
            timer.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            switch ((WM)m.Msg)
            {
                case WM.KEYUP:
                case WM.LBUTTONUP:
                case WM.MOUSEMOVE:
                    // リセット
                    timer.Enabled = false;
                    timer.Enabled = true;
                    break;
            }

            return false;
        }
    }
}
