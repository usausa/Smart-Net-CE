namespace Smart.Threading
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    /// <param name="state"></param>
    public delegate void SendOrPostCallback(object state);

    /// <summary>
    ///
    /// </summary>
    public class SynchronizationContext
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public virtual void Post(SendOrPostCallback d, object state)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }

            ThreadPool.QueueUserWorkItem(d.Invoke, state);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public virtual void Send(SendOrPostCallback d, object state)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }

            d(state);
        }
    }

    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    public class WindowsFormsSynchronizationContext : SynchronizationContext
    {
        private readonly Control marshalingControl;

        /// <summary>
        ///
        /// </summary>
        public WindowsFormsSynchronizationContext()
        {
            marshalingControl = new Control();

            // Force create
            // ReSharper disable once UnusedVariable
            var handle = marshalingControl.Handle;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        public WindowsFormsSynchronizationContext(Control control)
        {
            marshalingControl = control;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public override void Post(SendOrPostCallback d, object state)
        {
            if ((marshalingControl != null) && !marshalingControl.IsDisposed)
            {
                marshalingControl.BeginInvoke(d, new[] { state });
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public override void Send(SendOrPostCallback d, object state)
        {
            if ((marshalingControl != null) && !marshalingControl.IsDisposed)
            {
                marshalingControl.Invoke(d, new[] { state });
            }
        }
    }
}
