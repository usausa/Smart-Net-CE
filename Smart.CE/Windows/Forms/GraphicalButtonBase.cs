namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Smart.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class GraphicalButtonBase : GraphicalControl
    {
        private Size pressedOffset = new Size(0, 0);
        private bool drawFocusRectangle;
        private Color focusRectangleColor = Color.Gray;

        private ButtonCustomize customize = ButtonCustomize.All;

        private BorderStyle borderStyle = BorderStyle.Fixed3D;
        private int borderWidth = 2;

        private Color pressedForeColor = SystemColors.ControlText;
        private Color pressedBackColor = SystemColors.Control;
        private Color pressedBackColor2 = SystemColors.Control;
        private Image pressedImage;

        private Color focusedForeColor = SystemColors.ControlText;
        private Color focusedBackColor = SystemColors.Control;
        private Color focusedBackColor2 = SystemColors.Control;
        private Image focusedImage;

        private Color disabledForeColor = SystemColors.InactiveCaption;
        private Color disabledBackColor = SystemColors.Control;
        private Color disabledBackColor2 = SystemColors.Control;
        private Image disabledImage;

        /// <summary>
        ///
        /// </summary>
        public Size PressedOffset
        {
            get { return pressedOffset; }
            set
            {
                pressedOffset = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool DrawFocusRectangle
        {
            get { return drawFocusRectangle; }
            set
            {
                drawFocusRectangle = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color FocusRectangleColor
        {
            get { return focusRectangleColor; }
            set
            {
                focusRectangleColor = value;
                if (customize.HasFocused())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ButtonCustomize Customize
        {
            get { return customize; }
            set
            {
                customize = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public BorderStyle BorderStyle
        {
            get { return borderStyle; }
            set
            {
                borderStyle = value;
                if (borderStyle == BorderStyle.None)
                {
                    borderWidth = 0;
                }
                if ((borderStyle == BorderStyle.Fixed3D) &&
                    (borderWidth != 1) && (borderWidth != 2))
                {
                    borderWidth = 2;
                }
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
                if (borderWidth == 0)
                {
                    borderStyle = BorderStyle.None;
                }
                if ((borderStyle == BorderStyle.Fixed3D) &&
                    (borderWidth != 1) && (borderWidth != 2))
                {
                    borderWidth = 2;
                }
                if ((borderStyle == BorderStyle.None) &&
                    (borderWidth > 0))
                {
                    borderStyle = BorderStyle.FixedSingle;
                }
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color PressedForeColor
        {
            get { return pressedForeColor; }
            set
            {
                pressedForeColor = value;
                if (customize.HasPressed())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color PressedBackColor
        {
            get { return pressedBackColor; }
            set
            {
                pressedBackColor = value;
                if (customize.HasPressed())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color PressedBackColor2
        {
            get { return pressedBackColor2; }
            set
            {
                pressedBackColor2 = value;
                if (customize.HasPressed())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image PressedImage
        {
            get { return pressedImage; }
            set
            {
                if (DisposeResource && (pressedImage != null))
                {
                    pressedImage.Dispose();
                }
                pressedImage = value;
                if (customize.HasPressed())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color FocusedForeColor
        {
            get { return focusedForeColor; }
            set
            {
                focusedForeColor = value;
                if (customize.HasFocused())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color FocusedBackColor
        {
            get { return focusedBackColor; }
            set
            {
                focusedBackColor = value;
                if (customize.HasFocused())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color FocusedBackColor2
        {
            get { return focusedBackColor2; }
            set
            {
                focusedBackColor2 = value;
                if (customize.HasFocused())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image FocusedImage
        {
            get { return focusedImage; }
            set
            {
                if (DisposeResource && (focusedImage != null))
                {
                    focusedImage.Dispose();
                }
                focusedImage = value;
                if (customize.HasFocused())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color DisabledForeColor
        {
            get { return disabledForeColor; }
            set
            {
                disabledForeColor = value;
                if (customize.HasDisabled())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color DisabledBackColor
        {
            get { return disabledBackColor; }
            set
            {
                disabledBackColor = value;
                if (customize.HasDisabled())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color DisabledBackColor2
        {
            get { return disabledBackColor2; }
            set
            {
                disabledBackColor2 = value;
                if (customize.HasDisabled())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image DisabledImage
        {
            get { return disabledImage; }
            set
            {
                if (DisposeResource && (disabledImage != null))
                {
                    disabledImage.Dispose();
                }
                disabledImage = value;
                if (customize.HasDisabled())
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual bool IsPressed
        {
            get { return false; }
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual bool IsFocused
        {
            get { return Focused; }
        }

        /// <summary>
        ///
        /// </summary>
        public GraphicalButtonBase()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        ///
        /// </summary>
        protected override void DisposeResources()
        {
            if (pressedImage != null)
            {
                pressedImage.Dispose();
                pressedImage = null;
            }

            if (focusedImage != null)
            {
                focusedImage.Dispose();
                focusedImage = null;
            }

            if (disabledImage != null)
            {
                disabledImage.Dispose();
                disabledImage = null;
            }

            base.DisposeResources();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Up:
                        if (Parent != null)
                        {
                            Parent.SelectNextControl(this, false, true, true, true);
                        }
                        return;

                    case Keys.Right:
                    case Keys.Down:
                        if (Parent != null)
                        {
                            Parent.SelectNextControl(this, true, true, true, true);
                        }
                        return;

                    default:
                        return;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual ButtonState ResolveButtonState()
        {
            if (!Enabled && Customize.HasDisabled())
            {
                return ButtonState.Disabled;
            }
            if (IsPressed && Customize.HasPressed())
            {
                return ButtonState.Pressed;
            }
            if (Focused && Customize.HasFocused())
            {
                return ButtonState.Focused;
            }
            return ButtonState.Normal;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        protected override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            // フォーカス
            DrawFocus(g, CalcFocusRect());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Rectangle CalcTextRect()
        {
            var rect = base.CalcTextRect();

            if (DrawFocusRectangle)
            {
                rect.X += 3;
                rect.Y += 3;
                rect.Width -= 6;
                rect.Height -= 6;
            }

            if (IsPressed)
            {
                rect.X += PressedOffset.Width;
                rect.Y += PressedOffset.Height;
            }

            return rect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual Rectangle CalcFocusRect()
        {
            return CalcBackgroundRect();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected override void DrawText(Graphics g, Rectangle rect)
        {
            var color = ForeColor;

            switch (ResolveButtonState())
            {
                case ButtonState.Focused:
                    color = FocusedForeColor;
                    break;
                case ButtonState.Pressed:
                    color = PressedForeColor;
                    break;
                case ButtonState.Disabled:
                    color = DisabledForeColor;
                    break;
            }

            DrawText(g, rect, Text, TextAlign, MultiLine, Font, color, ShadowColor, ShadowMask);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void DrawBackground(Graphics g, Rectangle rect)
        {
            Image image;
            Color backColor;
            Color backColor2;
            switch (ResolveButtonState())
            {
                case ButtonState.Focused:
                    image = focusedImage;
                    backColor = focusedBackColor;
                    backColor2 = focusedBackColor2;
                    break;
                case ButtonState.Pressed:
                    image = pressedImage;
                    backColor = pressedBackColor;
                    backColor2 = pressedBackColor2;
                    break;
                case ButtonState.Disabled:
                    image = disabledImage;
                    backColor = disabledBackColor;
                    backColor2 = disabledBackColor2;
                    break;
                default:
                    image = Image;
                    backColor = BackColor;
                    backColor2 = BackColor2;
                    break;
            }

            if (image != null)
            {
                DrawImage(g, rect, image, UseTransparent, backColor, TransparentColor);
            }
            else
            {
                if (GradationStyle == GradationStyle.None)
                {
                    using (var brush = new SolidBrush(backColor))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else if (GradationStyle == GradationStyle.Horz)
                {
                    g.GradientFill(rect, backColor, backColor2, FillDirection.LeftToRight);
                }
                else if (GradationStyle == GradationStyle.Vert)
                {
                    g.GradientFill(rect, backColor, backColor2, FillDirection.TopToBottom);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected override void DrawBorder(Graphics g, Rectangle rect)
        {
            if (BorderStyle == BorderStyle.None)
            {
                return;
            }

            Color borderColor;
            switch (ResolveButtonState())
            {
                case ButtonState.Focused:
                    borderColor = focusedBackColor;
                    break;
                case ButtonState.Pressed:
                    borderColor = pressedBackColor;
                    break;
                case ButtonState.Disabled:
                    borderColor = disabledBackColor;
                    break;
                default:
                    borderColor = BackColor;
                    break;
            }

            if (BorderStyle == BorderStyle.FixedSingle)
            {
                g.DrawBorder(Border3DStyle.Flat, borderColor, rect);
            }
            else if (BorderStyle == BorderStyle.Fixed3D)
            {
                if (BorderWidth == 1)
                {
                    g.DrawBorder(IsPressed ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedInner, borderColor, rect);
                }
                else
                {
                    g.DrawBorder(IsPressed ? Border3DStyle.Sunken : Border3DStyle.Raised, borderColor, rect);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected virtual void DrawFocus(Graphics g, Rectangle rect)
        {
            if (DrawFocusRectangle && IsFocused)
            {
                using (var pen = new Pen(FocusRectangleColor))
                {
                    g.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
                }
            }
        }
    }
}
