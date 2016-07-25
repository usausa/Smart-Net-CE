namespace Smart.Windows.Forms
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public class TemplateList : ControlEx
    {
        public event EventHandler SelectedIndexChanged;

        private bool updating;

        private bool showScrollBar = true;

        private readonly VScrollBar scroll = new VScrollBar();

        private bool selectable;

        private int selectedIndex = -1;

        private int previousIndex;

        private int itemHeight = 32;

        private readonly TemplateElementCollection elements;

        private readonly TemplateListItemCollection items;

        private Color alternateBackColor = SystemColors.Window;

        private Color selectedForeColor = SystemColors.HighlightText;

        private Color selectedBackColor = SystemColors.Highlight;

        private Color inactiveSelectedForeColor = SystemColors.WindowText;

        private Color inactiveSelectedBackColor = SystemColors.Highlight;

        private Color borderColor = SystemColors.ControlDark;

        private bool border = true;

        private Color splitterColor = SystemColors.ControlLight;

        private bool splitter = true;

        private bool preferForeColorWhenSelected = true;

        private bool preferBackColorWhenSelected = true;

        private bool preferBorderColorWhenSelected = true;

        private Component designData;

        //--------------------------------------------------------------------------------
        // Property
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        public bool ShowScrollBar
        {
            get { return showScrollBar; }
            set
            {
                showScrollBar = value;
                UpdateLayout();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ScrollBarWidth
        {
            get { return scroll.Width; }
            set
            {
                scroll.Width = value;
                OnResize(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ScrollBarVisible
        {
            get { return scroll.Visible; }
        }

        /// <summary>
        ///
        /// </summary>
        public int TopIndex
        {
            get { return scroll.Value; }
            set { scroll.Value = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Selectable
        {
            get { return selectable; }
            set
            {
                selectable = value;

                if (!selectable)
                {
                    selectedIndex = -1;

                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex == value)
                {
                    return;
                }

                selectedIndex = Math.Max(value, -1);

                Refresh();

                FireSelectedIndexChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight <= 0)
                {
                    throw new ArgumentException("Invalid value.", "value");
                }

                itemHeight = value;
                UpdateLayout();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TemplateElementCollection Elements
        {
            get { return elements; }
        }

        /// <summary>
        ///
        /// </summary>
        public TemplateListItemCollection Items
        {
            get { return items; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color AlternateBackColor
        {
            get { return alternateBackColor; }
            set
            {
                alternateBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedForeColor
        {
            get { return selectedForeColor; }
            set
            {
                selectedForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedBackColor
        {
            get { return selectedBackColor; }
            set
            {
                selectedBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color InactiveSelectedForeColor
        {
            get { return inactiveSelectedForeColor; }
            set
            {
                inactiveSelectedForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color InactiveSelectedBackColor
        {
            get { return inactiveSelectedBackColor; }
            set
            {
                inactiveSelectedBackColor = value;
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
        public bool Border
        {
            get { return border; }
            set
            {
                border = value;
                OnResize(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SplitterColor
        {
            get { return splitterColor; }
            set
            {
                splitterColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Splitter
        {
            get { return splitter; }
            set
            {
                splitter = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PreferForeColorWhenSelected
        {
            get { return preferForeColorWhenSelected; }
            set
            {
                preferForeColorWhenSelected = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PreferBackColorWhenSelected
        {
            get { return preferBackColorWhenSelected; }
            set
            {
                preferBackColorWhenSelected = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PreferBorderColorWhenSelected
        {
            get { return preferBorderColorWhenSelected; }
            set
            {
                preferBorderColorWhenSelected = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Component DesignData
        {
            get { return designData; }
            set
            {
                designData = value;
                Invalidate();
            }
        }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public TemplateList()
        {
            DoubleBuffered = true;

            scroll.LargeChange = 1;
            scroll.Maximum = 0;
            scroll.ValueChanged += (sender, args) => Refresh();
            scroll.Visible = false;
            Controls.Add(scroll);

            elements = new TemplateElementCollection(this);
            items = new TemplateListItemCollection(this);
        }

        //--------------------------------------------------------------------------------
        // Event
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        private void FireSelectedIndexChanged()
        {
            var handler = SelectedIndexChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        //--------------------------------------------------------------------------------
        // Layout
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            scroll.Top = border ? 1 : 0;
            scroll.Left = Width - scroll.Width - (border ? 1 : 0);
            scroll.Height = Height - (border ? 2 : 0);
            UpdateLayout();
            base.OnResize(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        public void EnsureVisible(int index)
        {
            if (index < scroll.Value)
            {
                scroll.Value = Math.Max(index, 0);
            }
            else if (index > scroll.Value)
            {
                scroll.Value = CalcFirstInRectFrom(index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginUpdate()
        {
            updating = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndUpdate()
        {
            updating = false;
            UpdateLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateLayout()
        {
            if ((Width == 0) || (Height == 0))
            {
                return;
            }

            if (updating)
            {
                return;
            }

            scroll.Maximum = items.Count == 0 ? 0 : CalcFirstInRectFrom(items.Count - 1);
            scroll.Visible = ShowScrollBar || (scroll.Maximum > 0);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        private void ItemCleared()
        {
            selectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void ItemInserted(int index)
        {
            if ((selectedIndex == -1) || (selectedIndex < index))
            {
                return;
            }

            selectedIndex++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void ItemRemoved(int index)
        {
            if ((selectedIndex == -1) || (selectedIndex < index))
            {
                return;
            }

            if (selectedIndex == index)
            {
                selectedIndex = -1;

                FireSelectedIndexChanged();
            }
            else
            {
                selectedIndex--;
            }
        }

        //--------------------------------------------------------------------------------
        // Focus
        //--------------------------------------------------------------------------------

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

        //--------------------------------------------------------------------------------
        // Key
        //--------------------------------------------------------------------------------

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    MoveUp();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
                case Keys.PageUp:
                    MovePageUp();
                    break;
                case Keys.PageDown:
                    MovePageDown();
                    break;
                case Keys.Home:
                    MoveTop();
                    break;
                case Keys.End:
                    MoveLast();
                    break;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveUp()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                if (selectedIndex < 0)
                {
                    MoveTop();
                    return;
                }

                var index = selectedIndex - 1;
                if (index < 0)
                {
                    return;
                }

                SelectTo(index);
            }
            else
            {
                if (scroll.Value > 0)
                {
                    scroll.Value--;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void MoveDown()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                if (selectedIndex < 0)
                {
                    MoveTop();
                    return;
                }

                var index = selectedIndex + 1;
                if (index > items.Count - 1)
                {
                    return;
                }

                SelectTo(index);
            }
            else
            {
                if (scroll.Value < scroll.Maximum)
                {
                    scroll.Value++;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void MovePageUp()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                if (selectedIndex < 0)
                {
                    MoveTop();
                    return;
                }

                if (selectedIndex == 0)
                {
                    return;
                }

                // Calc page range
                var index = CalcFirstInRectFrom(selectedIndex);
                if (index == selectedIndex)
                {
                    index--;
                }

                SelectTo(index);
            }
            else
            {
                var index = CalcFirstInRectFrom(scroll.Value);
                if ((scroll.Value > 0) && (scroll.Value == index))
                {
                    index--;
                }

                scroll.Value = index;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MovePageDown()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                // Calc page range
                var current = selectedIndex < 0 ? 0 : selectedIndex;
                if (current == items.Count - 1)
                {
                    return;
                }

                var index = CalcLastInRectFrom(current);
                if (index == current)
                {
                    index++;
                }

                SelectTo(index);
            }
            else
            {
                var index = CalcLastInRectFrom(scroll.Value);
                if ((scroll.Value < scroll.Maximum) && (scroll.Value == index))
                {
                    index++;
                }

                scroll.Value = index;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveTop()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                if (selectedIndex == 0)
                {
                    return;
                }

                SelectTo(0);
            }
            else
            {
                scroll.Value = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveLast()
        {
            if (items.Count == 0)
            {
                return;
            }

            if (selectable)
            {
                if (selectedIndex == items.Count - 1)
                {
                    return;
                }

                SelectTo(items.Count - 1);
            }
            else
            {
                scroll.Value = scroll.Maximum;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        private void SelectTo(int index)
        {
            // Need scroll ?
            if ((index < scroll.Value) || (index > CalcLastInRectFrom(scroll.Value)))
            {
                selectedIndex = index;

                EnsureVisible(selectedIndex);

                FireSelectedIndexChanged();
            }
            else
            {
                SelectedIndex = index;
            }
        }

        //--------------------------------------------------------------------------------
        // Mouse
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!selectable)
            {
                return;
            }

            previousIndex = selectedIndex;

            var hit = HitTest(e.Y);
            if (hit >= 0)
            {
                selectedIndex = hit;

                Refresh();

                if (!Focused)
                {
                    Focus();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if ((selectedIndex >= 0) && (previousIndex != selectedIndex))
            {
                FireSelectedIndexChanged();
            }
        }

        //--------------------------------------------------------------------------------
        // Calculate
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Rectangle CalcListRect()
        {
            var rect = ClientRectangle;

            if (border)
            {
                rect.Inflate(-1, -1);
            }

            if (scroll.Visible)
            {
                rect.Width -= scroll.Width;
            }

            return rect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int CalcFirstInRectFrom(int index)
        {
            var rect = CalcListRect();

            var i = index;
            var height = 0;
            for (; i >= 0; i--)
            {
                // ignore last splitter
                height += itemHeight + (splitter && (i == items.Count - 1) ? 1 : 0);
                if (height > rect.Height)
                {
                    return Math.Min(i + 1, index);
                }
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int CalcLastInRectFrom(int index)
        {
            var rect = CalcListRect();

            var i = index;
            var height = 0;

            for (; i < items.Count; i++)
            {
                // ignore last splitter
                height += itemHeight + (splitter && (i == items.Count - 1) ? 1 : 0);
                if (height > rect.Height)
                {
                    return Math.Max(i - 1, index);
                }
            }

            return items.Count - 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int HitTest(int y)
        {
            var rect = CalcListRect();

            var baseY = rect.Top;
            var i = scroll.Value;
            while ((baseY < rect.Bottom) && (i < items.Count))
            {
                var height = itemHeight + (splitter ? 1 : 0);
                if ((y >= baseY) && (y < baseY + height))
                {
                    return i;
                }

                baseY += height;

                i++;
            }

            return -1;
        }

        //--------------------------------------------------------------------------------
        // Draw
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (updating)
            {
                return;
            }

            var g = GetPresentationMedium(e.Graphics);
            OnDraw(g);
            NotifyPaintingComplete(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            // Elements
            var rect = CalcListRect();

            g.Clip = new Region(rect);

            var context = new TemplateDrawContext
            {
                BaseBorderColor = SplitterColor,
                Font = Font
            };

            var designMode = (Site != null) && Site.DesignMode;
            var baseY = rect.Top;
            var source = designData as ITemplateListDesignData;
            var list = designMode && source != null ? (IList)source.Create() : items;
            var i = designMode ? 0 : scroll.Value;
            var max = designMode && list != null ? list.Count : items.Count;

            if (list != null)
            {
                while ((baseY < rect.Bottom) && (i < max))
                {
                    var elementRect = new Rectangle(rect.Left, baseY, rect.Width, itemHeight);
                    var selected = i == selectedIndex;
                    var foreColor = selected
                        ? (Focused ? selectedForeColor : inactiveSelectedForeColor)
                        : ForeColor;
                    var backColor = selected
                        ? (Focused ? selectedBackColor : inactiveSelectedBackColor)
                        : (i % 2 == 0) ? BackColor : alternateBackColor;

                    using (var brush = new SolidBrush(backColor))
                    {
                        g.FillRectangle(brush, elementRect);
                    }

                    context.PreferBaseForeColor = selected && PreferForeColorWhenSelected;
                    context.PreferBaseBackColor = selected && PreferBackColorWhenSelected;
                    context.PreferBaseBorderColor = selected && PreferBorderColorWhenSelected;
                    context.BaseForeColor = foreColor;
                    context.BaseBackColor = backColor;

                    TemplateDrawHelper.DrawElements(g, elementRect, list[i], elements, context);

                    baseY += itemHeight;

                    if (splitter)
                    {
                        using (var brush = new SolidBrush(SplitterColor))
                        {
                            g.FillRectangle(brush, new Rectangle(rect.Left, baseY, rect.Width, 1));
                        }

                        baseY += 1;
                    }

                    i++;
                }
            }

            using (var brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, rect.X, baseY, rect.Width, rect.Bottom - baseY);
            }

            // Border
            g.Clip = new Region(ClientRectangle);

            using (var pen = new Pen(borderColor))
            {
                g.DrawRectangle(pen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            }
        }

        //--------------------------------------------------------------------------------
        // Item
        //--------------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "Compatibility")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1039:ListsAreStronglyTyped", Justification = "Compatibility")]
        public sealed class TemplateListItemCollection : IList
        {
            private readonly TemplateList owner;

            private readonly ArrayList list = new ArrayList();

            public TemplateListItemCollection(TemplateList owner)
            {
                this.owner = owner;
            }

            public IEnumerator GetEnumerator()
            {
                return list.GetEnumerator();
            }

            public void Clear()
            {
                list.Clear();
                owner.ItemCleared();
                owner.UpdateLayout();
            }

            public int Add(object value)
            {
                list.Add(value);
                owner.ItemInserted(list.Count);
                owner.UpdateLayout();
                return list.Count - 1;
            }

            public void Insert(int index, object value)
            {
                list.Insert(index, value);
                owner.ItemInserted(index);
                owner.UpdateLayout();
            }

            public void Remove(object value)
            {
                var index = list.IndexOf(value);
                if (index != -1)
                {
                    RemoveAt(index);
                }
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
                owner.ItemRemoved(index);
                owner.UpdateLayout();
            }

            bool IList.Contains(object value)
            {
                return list.Contains(value);
            }

            int IList.IndexOf(object value)
            {
                return list.IndexOf(value);
            }

            public void CopyTo(Array array, int index)
            {
                list.CopyTo(array, index);
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
                get { return list[index]; }
                set
                {
                    list[index] = value;
                    owner.UpdateLayout();
                }
            }
        }
    }
}
