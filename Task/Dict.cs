using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    /// A key-value store
    /// Uses a binary tree to efficiently store items in-order
    /// </summary>
    /// <typeparam name="K">Key type</typeparam>
    /// <typeparam name="V">Value type</typeparam>
    public class Dict<K, V>
    {
        DictItem root;

        public V this[K key]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<K> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<V> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Tuple<K, V> item)
        {
            throw new NotImplementedException();
        }

        public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool ContainsValue(V item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<K, V>> AsEnumerable()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Tuple<K, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public Option<V> TryGetValue(K key)
        {
            throw new NotImplementedException();
        }

        class DictItem
        {
            public K Key;
            public V Value;
            public DictItem Left;
            public DictItem Right;

            public DictItem(V value, DictItem left, DictItem right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }
    }
}
