namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class SipControl : ControlEx
    {
        public event EventHandler<SipEventArgs> SipButtonClick;

        private readonly SipButtonCollection buttons;

        private SipButton selectedButton;

        private bool isInside;

        /// <summary>
        /// 
        /// </summary>
        public SipButtonCollection Buttons
        {
            get { return buttons; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RedrawAfterEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SipControl()
        {
            buttons = new SipButtonCollection(this);
            DoubleBuffered = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected SipButton HitTest(int x, int y)
        {
            return buttons.Cast<SipButton>().FirstOrDefault(button => button.HitTest(x, y));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            selectedButton = HitTest(e.X, e.Y);
            if (selectedButton == null)
            {
                return;
            }

            isInside = true;

            Capture = true;

            UpdateButton(selectedButton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (selectedButton == null)
            {
                return;
            }

            Capture = false;

            isInside = false;

            if (!RedrawAfterEvent)
            {
                UpdateButton(selectedButton);
            }

            if (selectedButton == HitTest(e.X, e.Y))
            {
                if (SipButtonClick != null)
                {
                    SipButtonClick(this, new SipEventArgs(selectedButton.Key));
                }
            }

            if (RedrawAfterEvent)
            {
                UpdateButton(selectedButton);
            }

            selectedButton = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((selectedButton == null) || (!Capture))
            {
                return;
            }

            if (isInside)
            {
                if (!selectedButton.HitTest(e.X, e.Y))
                {
                    isInside = false;

                    UpdateButton(selectedButton);
                }
            }
            else
            {
                if (selectedButton.HitTest(e.X, e.Y))
                {
                    isInside = true;

                    UpdateButton(selectedButton);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Justification = "Compatibility")]
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do Nothing
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = GetPresentationMedium(e.Graphics);

            if (Image == null)
            {
                g.Clear(BackColor);
            }
            else
            {
                var rect = new Rectangle(0, 0, Width, Height);
                g.DrawImage(Image, rect, rect, GraphicsUnit.Pixel);
            }

            foreach (SipButton button in buttons)
            {
                button.Draw(g, (selectedButton == button) && isInside);
            }

            NotifyPaintingComplete(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        public void UpdateButton(SipButton button)
        {
            if (button == null)
            {
                throw new ArgumentNullException("button");
            }

            using (var g = CreateGraphics())
            {
                button.Draw(g, (selectedButton == button) && isInside);
            }
        }
    }
}
