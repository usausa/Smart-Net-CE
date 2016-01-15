namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class AnimateControl : Control
    {
        private Image image;

        private bool autoStart;
        private int loopCount;
        private int loopCounter;
        private int frameCount;
        private int currentFrame;

        private readonly Timer timer = new Timer();

        /// <summary>
        /// 
        /// </summary>
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                currentFrame = 0;

                Invalidate();

                if (autoStart)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interval
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoStart
        {
            get { return autoStart; }
            set { autoStart = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LoopCount
        {
            get { return loopCount; }
            set { loopCount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                ResizeControl();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                ResizeControl();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AnimateControl()
        {
            timer.Tick += OnTimerTick;
            timer.Interval = 1000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                BackColor = Parent.BackColor;
            }
            base.OnParentChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            currentFrame++;

            if (currentFrame == frameCount)
            {
                currentFrame = 0;
                loopCounter++;
            }

            if ((loopCount != 0) && (loopCounter == loopCount))
            {
                Stop();
                return;
            }

            Invalidate();
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (timer.Enabled)
            {
                return;
            }

            ResizeControl();
            loopCounter = 0;

            timer.Enabled = true;

            Invalidate();
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            timer.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResizeControl()
        {
            if (image != null)
            {
                frameCount = image.Width / Width;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Justification = "Compatibility")]
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!timer.Enabled)
            {
                base.OnPaintBackground(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null)
            {
                e.Graphics.DrawImage(image, 0, 0, new Rectangle(currentFrame * Width, 0, Width, Height), GraphicsUnit.Pixel);
            }
        }
    }
}