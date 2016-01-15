namespace Smart.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Smart.Drawing;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BorderEffect
    {
        /// <summary>
        /// 
        /// </summary>
        public Control Target { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected BorderEffect()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        protected BorderEffect(Control target)
        {
            Attach(target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void Attach(Control target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Target = target;
            if (target.Parent != null)
            {
                target.Parent.Paint += ParentPaint;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Detach()
        {
            if ((Target != null) && (Target.Parent != null))
            {
                Target.Parent.Paint -= ParentPaint;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentPaint(object sender, PaintEventArgs e)
        {
            if (Target.Visible)
            {
                OnParentPaint(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnParentPaint(object sender, PaintEventArgs e);
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShadowEffect : BorderEffect
    {
        private ShadowMask shadowMask = ShadowMask.BottomRight;
        private int borderWidth = 1;
        private Color borderColor = Color.Gray;

        /// <summary>
        /// 
        /// </summary>
        public ShadowMask ShadowMask
        {
            get { return shadowMask; }
            set
            {
                shadowMask = value;
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
        public ShadowEffect()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public ShadowEffect(Control target)
            : base(target)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void Invalidate()
        {
            if (Target.Parent == null)
            {
                return;
            }

            var top = shadowMask.IsTop() ? borderWidth : 0;
            var bottom = shadowMask.IsBottom() ? borderWidth : 0;
            var left = shadowMask.IsLeft() ? borderWidth : 0;
            var right = shadowMask.IsRight() ? borderWidth : 0;
            var rect = new Rectangle(Target.Left - left, Target.Top - top, Target.Width + left + right, Target.Height + top + bottom);

            Target.Parent.Invalidate(rect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        protected override void OnParentPaint(object sender, PaintEventArgs e)
        {
            var top = shadowMask.IsTop() ? borderWidth : 0;
            var bottom = shadowMask.IsBottom() ? borderWidth : 0;
            var left = shadowMask.IsLeft() ? borderWidth : 0;
            var right = shadowMask.IsRight() ? borderWidth : 0;
            var rect = new Rectangle(Target.Left - left, Target.Top - top, Target.Width + left + right, Target.Height + top + bottom);

            if (e.ClipRectangle.IntersectsWith(rect))
            {
                using (Brush brush = new SolidBrush(borderColor))
                {
                    if ((shadowMask & ShadowMask.TopLeft) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left - borderWidth, Target.Top - borderWidth, Target.Width, Target.Height);
                    }
                    if ((shadowMask & ShadowMask.TopCenter) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left, Target.Top - borderWidth, Target.Width, Target.Height);
                    }
                    if ((shadowMask & ShadowMask.TopRight) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left + borderWidth, Target.Top - borderWidth, Target.Width, Target.Height);
                    }

                    if ((shadowMask & ShadowMask.MiddleLeft) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left - borderWidth, Target.Top, Target.Width, Target.Height);
                    }
                    if ((shadowMask & ShadowMask.MiddleRight) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left + borderWidth, Target.Top, Target.Width, Target.Height);
                    }

                    if ((shadowMask & ShadowMask.BottomLeft) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left - borderWidth, Target.Top + borderWidth, Target.Width, Target.Height);
                    }
                    if ((shadowMask & ShadowMask.BottomCenter) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left, Target.Top + borderWidth, Target.Width, Target.Height);
                    }
                    if ((shadowMask & ShadowMask.BottomRight) != 0)
                    {
                        e.Graphics.FillRectangle(brush, Target.Left + borderWidth, Target.Top + borderWidth, Target.Width, Target.Height);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Border3DEffect : BorderEffect
    {
        private Border3DStyle border3DStyle = Border3DStyle.Flat;

        private Color color = SystemColors.Control;

        /// <summary>
        /// 
        /// </summary>
        public Border3DStyle Border3DStyle
        {
            get { return border3DStyle; }
            set
            {
                border3DStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Border3DEffect()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public Border3DEffect(Control target)
            : base(target)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void Invalidate()
        {
            if (Target.Parent == null)
            {
                return;
            }

            var width = border3DStyle.CalcBorderWidth();
            var rect = new Rectangle(Target.Left - width, Target.Top - width, Target.Width + (width * 2), Target.Height + (width * 2));

            Target.Parent.Invalidate(rect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        protected override void OnParentPaint(object sender, PaintEventArgs e)
        {
            var width = border3DStyle.CalcBorderWidth();
            e.Graphics.DrawBorder(border3DStyle, color, Target.Left - width, Target.Top - width, Target.Width + (width * 2), Target.Height + (width * 2));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BorderEffectManager
    {
        private readonly List<BorderEffect> borderEffects = new List<BorderEffect>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effects"></param>
        public void Add(params BorderEffect[] effects)
        {
            borderEffects.AddRange(effects);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targets"></param>
        public void AddShadow(params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderWidth"></param>
        /// <param name="targets"></param>
        public void AddShadow(int borderWidth, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderWidth = borderWidth });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderColor"></param>
        /// <param name="targets"></param>
        public void AddShadow(Color borderColor, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderColor = borderColor });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shadowMask"></param>
        /// <param name="targets"></param>
        public void AddShadow(ShadowMask shadowMask, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { ShadowMask = shadowMask });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderWidth"></param>
        /// <param name="borderColor"></param>
        /// <param name="targets"></param>
        public void AddShadow(int borderWidth, Color borderColor, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderWidth = borderWidth, BorderColor = borderColor });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderWidth"></param>
        /// <param name="shadowMask"></param>
        /// <param name="targets"></param>
        public void AddShadow(int borderWidth, ShadowMask shadowMask, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderWidth = borderWidth, ShadowMask = shadowMask });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderColor"></param>
        /// <param name="shadowMask"></param>
        /// <param name="targets"></param>
        public void AddShadow(Color borderColor, ShadowMask shadowMask, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderColor = borderColor, ShadowMask = shadowMask });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderWidth"></param>
        /// <param name="borderColor"></param>
        /// <param name="shadowMask"></param>
        /// <param name="targets"></param>
        public void AddShadow(int borderWidth, Color borderColor, ShadowMask shadowMask, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new ShadowEffect(target) { BorderWidth = borderWidth, BorderColor = borderColor, ShadowMask = shadowMask });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targets"></param>
        public void AddBorder3D(params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new Border3DEffect(target));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="border3DStyle"></param>
        /// <param name="targets"></param>
        public void AddBorder3D(Border3DStyle border3DStyle, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new Border3DEffect(target) { Border3DStyle = border3DStyle });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="targets"></param>
        public void AddBorder3D(Color color, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new Border3DEffect(target) { Color = color });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="border3DStyle"></param>
        /// <param name="color"></param>
        /// <param name="targets"></param>
        public void AddBorder3D(Border3DStyle border3DStyle, Color color, params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                borderEffects.Add(new Border3DEffect(target) { Border3DStyle = border3DStyle, Color = color });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targets"></param>
        public void Detach(params Control[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            foreach (var target in targets)
            {
                for (var i = borderEffects.Count - 1; i >= 0; i--)
                {
                    if (borderEffects[i].Target == target)
                    {
                        borderEffects[i].Detach();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public T GetEffect<T>(Control target) where T : BorderEffect
        {
            return borderEffects.Where(effect => effect.Target == target).Cast<T>().FirstOrDefault();
        }
    }
}
