namespace Smart.Windows.Forms
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    public interface IBehaivor<TControl> where TControl : Control
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        void Attach(TControl control);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        void Detach(TControl control);
    }

    /// <summary>
    /// 
    /// </summary>
    public static class BehaivorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="behaivors"></param>
        public static void AddBehaivor<TControl>(this TControl control, params IBehaivor<TControl>[] behaivors) where TControl : Control
        {
            if (behaivors == null)
            {
                throw new ArgumentNullException("behaivors");
            }

            foreach (var behaivor in behaivors)
            {
                behaivor.Attach(control);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClearKeyBehaivor : IBehaivor<Control>
    {
        private static readonly ClearKeyBehaivor Singleton = new ClearKeyBehaivor(new[] { Keys.Escape, Keys.Delete });

        /// <summary>
        /// 
        /// </summary>
        public static ClearKeyBehaivor Default
        {
            get
            {
                return Singleton;
            }
        }

        private readonly Keys[] clearKeys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearKeys"></param>
        public ClearKeyBehaivor(params Keys[] clearKeys)
        {
            this.clearKeys = clearKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            
            control.KeyDown += ControlOnKeyDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyDown -= ControlOnKeyDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnKeyDown(object sender, KeyEventArgs args)
        {
            if (clearKeys.Any(key => args.KeyCode == key))
            {
                var control = (Control)sender;
                control.Text = string.Empty;
                args.Handled = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KeyDownHandledBehaivor : IBehaivor<Control>
    {
        private readonly Keys[] handledKeys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handledKeys"></param>
        public KeyDownHandledBehaivor(params Keys[] handledKeys)
        {
            this.handledKeys = handledKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyDown += ControlOnKeyDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyDown -= ControlOnKeyDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnKeyDown(object sender, KeyEventArgs args)
        {
            if (handledKeys.Any(key => args.KeyCode == key))
            {
                args.Handled = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KeyPressHandledBehaivor : IBehaivor<Control>
    {
        private static readonly KeyPressHandledBehaivor DisableBeepSingleton = new KeyPressHandledBehaivor((char)Keys.Enter, (char)Keys.Escape);

        /// <summary>
        /// 
        /// </summary>
        public static KeyPressHandledBehaivor DisableBeep
        {
            get
            {
                return DisableBeepSingleton;
            }
        }

        private readonly char[] handledKeys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handledKeys"></param>
        public KeyPressHandledBehaivor(params char[] handledKeys)
        {
            this.handledKeys = handledKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyPress += ControlOnKeyPress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyPress -= ControlOnKeyPress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnKeyPress(object sender, KeyPressEventArgs args)
        {
            if (handledKeys.Any(key => args.KeyChar == key))
            {
                args.Handled = true;
            }
        }
    }
}
