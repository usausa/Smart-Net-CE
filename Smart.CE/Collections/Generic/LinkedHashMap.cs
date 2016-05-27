namespace Smart.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class LinkedHashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> dictionary;
        private readonly LinkedList<KeyValuePair<TKey, TValue>> list;

        /// <summary>
        ///
        /// </summary>
        public LinkedHashMap()
        {
            dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="comparer"></param>
        public LinkedHashMap(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(comparer);
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="capacity"></param>
        public LinkedHashMap(int capacity)
        {
            dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(capacity);
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public LinkedHashMap(int capacity, IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(capacity, comparer);
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public LinkedHashMap(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var countable = source as ICollection;
            dictionary = countable != null ? new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(countable.Count) : new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
            foreach (var pair in source)
            {
                this[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        public LinkedHashMap(IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var countable = source as ICollection;
            dictionary = countable != null ? new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(countable.Count, comparer) : new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(comparer);
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
            foreach (var pair in source)
            {
                this[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            DoAdd(key, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void DoAdd(TKey key, TValue value)
        {
            var node = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
            dictionary.Add(key, node);
            list.AddLast(node);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> n;
            if (!dictionary.TryGetValue(key, out n))
            {
                return false;
            }
            DoRemove(n);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        private void DoRemove(LinkedListNode<KeyValuePair<TKey, TValue>> node)
        {
            dictionary.Remove(node.Value.Key);
            list.Remove(node);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> n;
            if (dictionary.TryGetValue(key, out n))
            {
                value = n.Value.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool TryGetNode(TKey key, TValue value, out LinkedListNode<KeyValuePair<TKey, TValue>> node)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> n;
            if (dictionary.TryGetValue(key, out n) && EqualityComparer<TValue>.Default.Equals(value, n.Value.Value))
            {
                node = n;
                return true;
            }
            node = null;
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return dictionary[key].Value.Value; }
            set
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> n;
                if (!dictionary.TryGetValue(key, out n))
                {
                    DoAdd(key, value);
                    return;
                }
                DoSet(n, key, value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void DoSet(LinkedListNode<KeyValuePair<TKey, TValue>> node, TKey key, TValue value)
        {
            var newNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
            dictionary[key] = newNode;
            list.AddAfter(node, newNode);
            list.Remove(node);
        }

        /// <summary>
        ///
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return list.Select(p => p.Key).ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return list.Select(p => p.Value).ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
            list.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        public int Count
        {
            get { return dictionary.Count; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> pair;
            return TryGetNode(item.Key, item.Value, out pair);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> node;
            if (!TryGetNode(item.Key, item.Value, out node))
            {
                return false;
            }
            DoRemove(node);
            return true;
        }
    }
}
