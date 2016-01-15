namespace Smart.Windows.Forms
{
    using System;
    using System.Collections;
    using System.Drawing;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "Compatibility")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "Compatibility")]
    public sealed class SipButtonCollection : IList
    {
        private readonly SipControl parent;
        private readonly ArrayList list = new ArrayList();

        public SipButtonCollection(SipControl parent)
        {
            this.parent = parent;
        }

        public int Add(SipButton value)
        {
            Insert(list.Count, value);
            return list.Count - 1;
        }

        public void Clear()
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                RemoveAt(i);
            }
        }

        public bool Contains(SipButton value)
        {
            return list.Contains(value);
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(SipButton value)
        {
            return list.IndexOf(value);
        }

        public void Insert(int index, SipButton value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var scale = parent.ScaleFactor;
            if ((scale.Width > 1) || (scale.Height > 1))
            {
                value.Rectangle = new Rectangle(value.Rectangle.X * (int)scale.Width, value.Rectangle.Y * (int)scale.Height,
                                                value.Rectangle.Width * (int)scale.Width, value.Rectangle.Height * (int)scale.Height);
            }
            value.Parent = parent;
            list.Insert(index, value);
            parent.Invalidate();
        }

        public void Remove(SipButton value)
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
            parent.Invalidate();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        int IList.Add(object value)
        {
            return Add((SipButton)value);
        }

        bool IList.Contains(object value)
        {
            return Contains((SipButton)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((SipButton)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (SipButton)value);
        }

        void IList.Remove(object value)
        {
            Remove((SipButton)value);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public SipButton this[int index]
        {
            get { return (SipButton)list[index]; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }
    }
}
