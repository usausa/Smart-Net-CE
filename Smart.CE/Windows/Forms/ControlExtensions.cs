namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class ControlExtensions
    {
        private const int WS_VSCROLL = 0x00200000;

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void InvokeIfNeed(this Control control, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="action"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void InvokeIfNeed<T>(this Control control, Action<T> action, T args)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action, args);
            }
            else
            {
                action(args);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static T AddControl<T>(this Control parent, Func<Control, T> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            var control = factory(parent);
            parent.Controls.Add(control as Control);
            return control;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Control GetControl(this Control parent, string name)
        {
            return parent.Controls.OfType<Control>().FirstOrDefault(c => c.Name == name);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static T GetControl<T>(this Control parent, string name) where T : Control
        {
            return parent.Controls.OfType<T>().FirstOrDefault(c => c.Name == name);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Control GetFocused(this Control parent)
        {
            return parent.Focused ? parent : parent.Controls.Cast<Control>().Select(c => GetFocused(c)).FirstOrDefault(_ => _ != null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsOwnFocus(this Control control)
        {
            return TestFocusHierarchy(control);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private static bool TestFocusHierarchy(this Control control)
        {
            return control.Focused || control.Controls.Cast<Control>().Any(TestFocusHierarchy);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int CalcResolution(this ContainerControl control)
        {
            return Convert.ToInt32(control.CurrentAutoScaleDimensions.Height / 96);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsHighResolution(this ContainerControl control)
        {
            return (int)control.CurrentAutoScaleDimensions.Height == 192;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static Control GetNextControl(this Control parent, Control control, bool forward)
        {
            var index = parent.Controls.IndexOf(control);

            if (forward)
            {
                if (index < parent.Controls.Count)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            else
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = parent.Controls.Count - 1;
                }
            }

            return parent.Controls[index];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        public static void ScrollToControlVisible(this ScrollableControl parent, Control control)
        {
            ScrollToControlVisible(parent, control, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        /// <param name="margin"></param>
        public static void ScrollToControlVisible(this ScrollableControl parent, Control control, int margin)
        {
            ScrollToControlVisible(parent, control, margin, margin);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        /// <param name="marginX"></param>
        /// <param name="marginY"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void ScrollToControlVisible(this ScrollableControl parent, Control control, int marginX, int marginY)
        {
            int x;
            if (control.Left - marginX > parent.AutoScrollPosition.X)
            {
                x = control.Left - parent.AutoScrollPosition.X - marginX;
            }
            else
            {
                x = parent.AutoScrollPosition.X;
            }

            int y;
            if (control.Top - marginY > parent.AutoScrollPosition.Y)
            {
                y = control.Top - parent.AutoScrollPosition.Y - marginY;
            }
            else
            {
                y = parent.AutoScrollPosition.Y;
            }

            parent.AutoScrollPosition = new Point(x, y);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsScollBarVisibile(this Control control)
        {
            var style = (int)NativeMethods.GetWindowLong(control.Handle, -16);
            return (style & WS_VSCROLL) != 0;
        }
    }
}
