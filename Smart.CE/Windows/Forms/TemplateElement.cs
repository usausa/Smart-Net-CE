namespace Smart.Windows.Forms
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using Smart.Drawing;

    /// <summary>
    /// 
    /// </summary>
    public class TemplateElement : Component
    {
        internal Control Parent { get; set; }

        private void ParentInvalidate()
        {
            if (Parent != null)
            {
                Parent.Invalidate();
            }
        }

        // Rectangle

        private Point point;
        public Point Point
        {
            get { return point; }
            set
            {
                point = value;
                ParentInvalidate();
            }
        }

        private Size size;
        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                ParentInvalidate();
            }
        }

        private Size padding = new Size(0, 0);
        public Size Padding
        {
            get { return padding; }
            set
            {
                padding = value;
                ParentInvalidate();
            }
        }

        private ContentAlignmentEx textAlign = ContentAlignmentEx.MiddleLeft;
        public ContentAlignmentEx TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                ParentInvalidate();
            }
        }

        private bool multiline;
        public bool Multiline
        {
            get { return multiline; }
            set
            {
                multiline = value;
                ParentInvalidate();
            }
        }

        private bool borderTop;
        public bool BorderTop
        {
            get { return borderTop; }
            set
            {
                borderTop = value;
                ParentInvalidate();
            }
        }

        private bool borderBottom;
        public bool BorderBottom
        {
            get { return borderBottom; }
            set
            {
                borderBottom = value;
                ParentInvalidate();
            }
        }

        private bool borderLeft;
        public bool BorderLeft
        {
            get { return borderLeft; }
            set
            {
                borderLeft = value;
                ParentInvalidate();
            }
        }

        private bool borderRight;
        public bool BorderRight
        {
            get { return borderRight; }
            set
            {
                borderRight = value;
                ParentInvalidate();
            }
        }

        // Text

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                ParentInvalidate();
            }
        }

        private string textProperty;
        public string TextProperty
        {
            get { return textProperty; }
            set
            {
                textProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<string> textConverter;
        public ITemplateConverter<string> TextConverter
        {
            get { return textConverter; }
            set
            {
                textConverter = value;
                ParentInvalidate();
            }
        }

        // Font

        private Font font;
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                ParentInvalidate();
            }
        }

        private string fontProperty;
        public string FontProperty
        {
            get { return fontProperty; }
            set
            {
                fontProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<Font> fontConverter;
        public ITemplateConverter<Font> FontConverter
        {
            get { return fontConverter; }
            set
            {
                fontConverter = value;
                ParentInvalidate();
            }
        }

        // Image

        private Image image;
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                ParentInvalidate();
            }
        }

        private string imageProperty;
        public string ImageProperty
        {
            get { return imageProperty; }
            set
            {
                imageProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<Image> imageConverter;
        public ITemplateConverter<Image> ImageConverter
        {
            get { return imageConverter; }
            set
            {
                imageConverter = value;
                ParentInvalidate();
            }
        }

        // Fore Color

        private Color foreColor = Color.Transparent;
        public Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                ParentInvalidate();
            }
        }

        private string foreColorProperty;
        public string ForeColorProperty
        {
            get { return foreColorProperty; }
            set
            {
                foreColorProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<Color> foreColorConverter;
        public ITemplateConverter<Color> ForeColorConverter
        {
            get { return foreColorConverter; }
            set
            {
                foreColorConverter = value;
                ParentInvalidate();
            }
        }

        // Back Color

        private Color backColor = Color.Transparent;
        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                ParentInvalidate();
            }
        }

        private string backColorProperty;
        public string BackColorProperty
        {
            get { return backColorProperty; }
            set
            {
                backColorProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<Color> backColorConverter;
        public ITemplateConverter<Color> BackColorConverter
        {
            get { return backColorConverter; }
            set
            {
                backColorConverter = value;
                ParentInvalidate();
            }
        }

        // Border Color

        private Color borderColor = Color.Transparent;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                ParentInvalidate();
            }
        }

        private string borderColorProperty;
        public string BorderColorProperty
        {
            get { return borderColorProperty; }
            set
            {
                borderColorProperty = value;
                ParentInvalidate();
            }
        }

        private ITemplateConverter<Color> borderColorConverter;
        public ITemplateConverter<Color> BorderColorConverter
        {
            get { return borderColorConverter; }
            set
            {
                borderColorConverter = value;
                ParentInvalidate();
            }
        }
    }
}
