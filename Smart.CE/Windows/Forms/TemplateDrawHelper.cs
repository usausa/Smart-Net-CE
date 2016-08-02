namespace Smart.Windows.Forms
{
    using System;
    using System.Collections;
    using System.Drawing;

    using Smart.Drawing;

    /// <summary>
    ///
    /// </summary>
    public static class TemplateDrawHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="item"></param>
        /// <param name="elements"></param>
        /// <param name="context"></param>
        public static void DrawElements(Graphics g, Rectangle rect, object item, IList elements, TemplateDrawContext context)
        {
            foreach (TemplateElement element in elements)
            {
                var elementRect = new Rectangle(rect.X + element.Point.X, rect.Y + element.Point.Y, element.Size.Width, element.Size.Height);
                var hasBorder = element.BorderTop || element.BorderBottom || element.BorderLeft || element.BorderRight;

                // Background
                if (!context.PreferBaseBackColor)
                {
                    var color = ResolveValue(item, element.BackColor, element.BackColorProperty, element.BackColorConverter);
                    if (!color.Equals(Color.Transparent))
                    {
                        using (var br = new SolidBrush(color))
                        {
                            g.FillRectangle(br, elementRect);
                        }
                    }
                }

                // Image
                var image = ResolveValue(item, element.Image, element.ImageProperty, element.ImageConverter);
                if (image != null)
                {
                    var imageRect = elementRect;
                    if (hasBorder)
                    {
                        imageRect.Inflate(-1, -1);
                    }

                    var rc = element.TextAlign.CalcTextRect(image.Width, image.Height, imageRect);

                    g.DrawImage(image, (int)rc.X, (int)rc.Y);
                }

                // Text
                var text = ResolveValue(item, element.Text, element.TextProperty, element.TextConverter);
                if (text != null)
                {
                    var color = context.PreferBaseForeColor ? context.BaseForeColor : ResolveValue(item, element.ForeColor, element.ForeColorProperty, element.ForeColorConverter);
                    if (color.Equals(Color.Transparent))
                    {
                        color = context.BaseForeColor;
                    }

                    var font = ResolveValue(item, element.Font, element.FontProperty, element.FontConverter) ?? context.Font;

                    var textRect = elementRect;
                    textRect.X += element.Padding.Width;
                    textRect.Y += element.Padding.Height;
                    textRect.Width -= element.Padding.Width * 2;
                    textRect.Height -= element.Padding.Height * 2;
                    if (hasBorder)
                    {
                        textRect.Inflate(-1, -1);
                    }

                    if (element.Multiline)
                    {
                        var texts = g.GetMultilineText(text, font, textRect.Width);
                        var textSize = g.CalcMultilineTextSize(texts, font);

                        var rc = element.TextAlign.CalcTextRect(textSize, textRect);

                        using (var br = new SolidBrush(color))
                        {
                            var top = rc.Top;
                            foreach (var line in texts)
                            {
                                var size = g.MeasureString(String.IsNullOrEmpty(line) ? " " : line, font);
                                var rcLine = element.TextAlign.CalcTextRect(size, rc.Left, top, rc.Width, size.Height);

                                g.DrawString(line, font, br, rcLine.X, rcLine.Y);

                                top += rcLine.Height;
                            }
                        }
                    }
                    else
                    {
                        var rc = element.TextAlign.CalcTextRect(g.MeasureString(text, font), textRect);

                        using (var br = new SolidBrush(color))
                        {
                            g.DrawString(text, font, br, rc);
                        }
                    }
                }

                // Border
                if (hasBorder)
                {
                    var color = context.PreferBaseBorderColor ? context.BaseBorderColor : ResolveValue(item, element.BorderColor, element.BorderColorProperty, element.BorderColorConverter);
                    if (color.Equals(Color.Transparent))
                    {
                        color = context.BaseBorderColor;
                    }

                    g.DrawBorder(color, elementRect, element.BorderTop, element.BorderBottom, element.BorderLeft, element.BorderRight);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="elementValue"></param>
        /// <param name="property"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        private static T ResolveValue<T>(object item, T elementValue, string property, ITemplateConverter<T> converter)
        {
            if (String.IsNullOrEmpty(property) && (converter == null))
            {
                return elementValue;
            }

            var value = item;
            if ((value != null) && !String.IsNullOrEmpty(property) && (property != "."))
            {
                var accessor = TemplateAccessorCache.GetAccessor(item.GetType(), property);
                if ((accessor != null) && accessor.CanRead)
                {
                    value = accessor.GetValue(value);
                }
            }

            if (converter != null)
            {
                value = converter.Convert(value);
            }

            try
            {
                return value is T ? (T)value : (T)Convert.ChangeType(value, typeof(T), null);
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}
