namespace Smart.Windows.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "Compatibility")]
    public sealed class TemplateElementCollection : IList
    {
        private readonly Control owner;

        private readonly List<TemplateElement> list = new List<TemplateElement>();

        public TemplateElementCollection(Control owner)
        {
            this.owner = owner;
        }

        public IEnumerator<TemplateElement> GetEnumerator()
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
            owner.Invalidate();
        }

        public void Add(TemplateElement item)
        {
            item.Parent = owner;
            list.Add(item);
            owner.Invalidate();
        }

        public int Add(object value)
        {
            Add((TemplateElement)value);
            return list.Count - 1;
        }

        public void Insert(int index, TemplateElement item)
        {
            item.Parent = owner;
            list.Insert(index, item);
            owner.Invalidate();
        }

        public void Insert(int index, object value)
        {
            Insert(index, (TemplateElement)value);
        }

        public void RemoveAt(int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            item.Parent = null;
            owner.Invalidate();
        }

        public bool Remove(TemplateElement item)
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
            Remove((TemplateElement)value);
        }

        public bool Contains(TemplateElement item)
        {
            return list.Contains(item);
        }

        public bool Contains(object value)
        {
            return Contains((TemplateElement)value);
        }

        public int IndexOf(TemplateElement item)
        {
            return list.IndexOf(item);
        }

        public int IndexOf(object value)
        {
            return IndexOf((TemplateElement)value);
        }

        public void CopyTo(TemplateElement[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((TemplateElement[])array, index);
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
            set { this[index] = (TemplateElement)value; }
        }

        public TemplateElement this[int index]
        {
            get { return list[index]; }
            set
            {
                var current = list[index];
                current.Parent = null;
                value.Parent = owner;
                list[index] = value;
                owner.Invalidate();
            }
        }
    }
}
