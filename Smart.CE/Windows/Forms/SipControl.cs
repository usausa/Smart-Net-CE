namespace Smart.Windows.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
            return buttons.FirstOrDefault(button => button.HitTest(x, y));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
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

        //--------------------------------------------------------------------------------
        // Item
        //--------------------------------------------------------------------------------

        public sealed class SipButtonCollection : IList<SipButton>, IList
        {
            private readonly SipControl parent;

            private readonly List<SipButton> list = new List<SipButton>();

            public SipButtonCollection(SipControl parent)
            {
                this.parent = parent;
            }

            public IEnumerator<SipButton> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Clear()
            {
                foreach (var item in list)
                {
                    item.Parent = null;
                }
                list.Clear();
                parent.Invalidate();
            }

            public void Add(SipButton item)
            {
                item.Parent = parent;
                list.Add(item);
                parent.Invalidate();
            }

            public int Add(object value)
            {
                Add((SipButton)value);
                return list.Count - 1;
            }

            public void Insert(int index, SipButton item)
            {
                item.Parent = parent;
                list.Insert(index, item);
                parent.Invalidate();
            }

            public void Insert(int index, object value)
            {
                Insert(index, (SipButton)value);
            }

            public bool Remove(SipButton item)
            {
                var index = list.IndexOf(item);
                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);
                return true;
            }

            public void Remove(object value)
            {
                Remove((SipButton)value);
            }

            public void RemoveAt(int index)
            {
                var item = this[index];
                list.RemoveAt(index);
                item.Parent = null;
                parent.Invalidate();
            }

            public bool Contains(SipButton item)
            {
                return list.Contains(item);
            }

            public bool Contains(object value)
            {
                return Contains((SipButton)value);
            }

            public int IndexOf(SipButton item)
            {
                return list.IndexOf(item);
            }

            public int IndexOf(object value)
            {
                return IndexOf((SipButton)value);
            }

            public void CopyTo(SipButton[] array, int arrayIndex)
            {
                list.CopyTo(array, arrayIndex);
            }

            public void CopyTo(Array array, int index)
            {
                CopyTo((SipButton[])array, index);
            }

            public int Count
            {
                get { return list.Count; }
            }

            public object SyncRoot
            {
                get { return list; }
            }

            public bool IsSynchronized
            {
                get { return false; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool IsFixedSize
            {
                get { return false; }
            }

            object IList.this[int index]
            {
                get { return this[index]; }
                set { this[index] = (SipButton)value; }
            }

            public SipButton this[int index]
            {
                get { return list[index]; }
                set
                {
                    var current = list[index];
                    current.Parent = null;
                    value.Parent = parent;
                    list[index] = value;
                    parent.Invalidate();
                }
            }
        }
    }
}
