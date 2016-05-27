namespace Smart.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class ApplicationEx
    {
        public static event EventHandler<EventArgs> ThreadExit;
        public static event EventHandler<EventArgs> ApplicationExit;

        private static readonly List<IMessageFilter> MessageFilters = new List<IMessageFilter>();
        private static Form mainForm;
        private static MSG msg;

        /// <summary>
        ///
        /// </summary>
        public static bool MessageLoop { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static string ExecutablePath
        {
            get
            {
                var fileName = new StringBuilder(256);
                var ret = NativeMethods.GetModuleFileName(IntPtr.Zero, fileName, fileName.Capacity);
                if (ret <= 0)
                {
                    return null;
                }

                if (ret > fileName.Capacity)
                {
                    throw new PathTooLongException("Assembly name is longer than MAX_PATH.");
                }

                return fileName.ToString();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static string StartupPath
        {
            get
            {
                return Path.GetDirectoryName(ExecutablePath);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void AddMessageFilter(IMessageFilter value)
        {
            MessageFilters.Add(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        public static void RemoveMessageFilter(IMessageFilter filter)
        {
            MessageFilters.Remove(filter);
        }

        /// <summary>
        ///
        /// </summary>
        public static void Run()
        {
            RunMessageLoop(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="form"></param>
        public static void Run(Form form)
        {
            Run(form, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="form"></param>
        /// <param name="displayMainForm"></param>
        public static void Run(Form form, bool displayMainForm)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            form.Closed += MainFormExit;
            mainForm = form;
            RunMessageLoop(displayMainForm);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="showForm"></param>
        private static void RunMessageLoop(bool showForm)
        {
            if (mainForm != null)
            {
                mainForm.Visible = showForm;
            }

            MessageLoop = true;
            while (Pump())
            {
            }
            MessageLoop = false;

            ExitThread();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private static bool Pump()
        {
            if (NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                var process = true;

                foreach (var filter in MessageFilters)
                {
                    var m = Message.Create(msg.Hwnd, msg.Message, msg.WParam, msg.LParam);
                    process = process && !filter.PreFilterMessage(ref m);
                }

                if (process)
                {
                    NativeMethods.TranslateMessage(out msg);
                    NativeMethods.DispatchMessage(ref msg);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        public static void Exit()
        {
            NativeMethods.PostQuitMessage(0);
        }

        /// <summary>
        ///
        /// </summary>
        public static void DoEvents()
        {
            while (NativeMethods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0))
            {
                Pump();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MainFormExit(object sender, EventArgs e)
        {
            NativeMethods.PostQuitMessage(0);
        }

        /// <summary>
        ///
        /// </summary>
        private static void ExitThread()
        {
            var threadExit = ThreadExit;
            if (threadExit != null)
            {
                threadExit(mainForm, EventArgs.Empty);
            }

            if (mainForm != null)
            {
                mainForm.Dispose();
            }

            var applicationExit = ApplicationExit;
            if (applicationExit != null)
            {
                applicationExit(null, EventArgs.Empty);
            }

            GC.GetTotalMemory(true);
        }
    }
}
