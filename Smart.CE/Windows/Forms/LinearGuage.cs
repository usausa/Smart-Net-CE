namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Smart.Drawing;

    /// <summary>
    /// 
    /// </summary>
    public class LinearGuage : ControlEx
    {
        private bool vertical = true;
        private int borderWidth = 1;
        private Color borderColor = Color.Gray;
        private Color guageColor = Color.Green;
        private Color guageBackColor = Color.Red;

        private bool showValue;
        private string unit = string.Empty;
        private string format = "0";

        private double maxValue = 100;
        private double minValue;
        private double currentValue;

        /// <summary>
        /// 
        /// </summary>
        public bool Vertical
        {
            get { return vertical; }
            set
            {
                vertical = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color GuageColor
        {
            get { return guageColor; }
            set
            {
                guageColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color GuageBackColor
        {
            get { return guageBackColor; }
            set
            {
                guageBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowValue
        {
            get { return showValue; }
            set
            {
                showValue = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Format
        {
            get { return format; }
            set
            {
                format = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = GetPresentationMedium(e.Graphics);

            g.Clear(guageBackColor);

            using (var pen = new Pen(borderColor, 1))
            {
                var rcBorder = new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                for (var i = 0; i < borderWidth; i++)
                {
                    g.DrawRectangle(pen, rcBorder);
                    rcBorder.X += 1;
                    rcBorder.Y += 1;
                    rcBorder.Width -= 2;
                    rcBorder.Height -= 2;
                }
            }

            Rectangle rect;
            var percent = (currentValue - minValue) / (maxValue - minValue);
            if (vertical)
            {
                var length = (int)(percent * (ClientRectangle.Height - (borderWidth * 2)));
                rect = new Rectangle(borderWidth, -borderWidth + ClientRectangle.Height - length, ClientRectangle.Width - (borderWidth * 2), length);
            }
            else
            {
                var length = (int)(percent * (ClientRectangle.Width - (borderWidth * 2)));
                rect = new Rectangle(borderWidth, borderWidth, length, ClientRectangle.Height - (borderWidth * 2));
            }

            using (var brush = new SolidBrush(guageColor))
            {
                g.FillRectangle(brush, rect);
            }

            if (showValue)
            {
                var str = String.IsNullOrEmpty(format) ? currentValue.ToString(CultureInfo.CurrentCulture) : currentValue.ToString(format, CultureInfo.CurrentCulture);
                if (!String.IsNullOrEmpty(unit))
                {
                    str = String.Format(CultureInfo.CurrentCulture, "{0} {1}", str, unit);
                }

                g.DrawText(str, Font, ForeColor, ClientRectangle, ContentAlignmentEx.MiddleCenter);
            }

            NotifyPaintingComplete(e);
        }
    }
}
