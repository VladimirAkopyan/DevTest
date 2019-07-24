using System;
using Task.Interfaces;

namespace Task
{
    /// <summary>
    /// A singly-linked list
    /// Stores a list of items in the order they're added
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class SingleList<T>// : IEnumeable<T>
    {
        internal ListItem<T> head;
        internal ListItem<T> tail;
        internal int _count = 0; 

        public SingleList(){
            head = null;
        }

        public void Add(T value){
            var item = new ListItem<T>(null, value);

            if (head != null)
            {     
                tail.Next = item;
                
            }
            else
            {
                head = item;
            }
            tail = item;
            _count++; 
        }

        public int Count => _count; 

        //This is going to be very slow to traverse when not accessed sequentially; 
        //As it is a linked list, nothing but exhaustive search will do. 
        public T this[int index]{
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        

        public int IndexOf(T item)
        {
            var c = System.Collections.Generic.EqualityComparer<T>.Default;
            int i = 0; 
            ListItem<T> node = head; 
            while(node != null)
            {
                if(c.Equals(node.Value, item))
                {
                    return i; 
                }
                i++; 
            }
            return -1; 
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _count = 0;
            head = new ListItem<T>(null, default(T));
            tail = head;
        }

        public bool Contains(T item)
        {
            ListItem<T> node = head;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }
            }
            return false; 
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
/*
        public IEnumeable<T> AsEnumerable()
        {
            throw new NotImplementedException();
        }
        */

        internal struct Enumerator : IEnumerator<T> {
            SingleList<T> _list; 
            //Index here starts at 1 because .Net starts with MoveNext before it calls Current
            int _index;
            ListItem<T> _current;

            internal Enumerator(SingleList<T> singleList){
                _list = singleList;
                _index = 0;
                _current = null; 
            }

            /// <summary>
            /// Returns null when collection iterator is not in a valid state
            /// </summary>
            public T Current => _current.Value;

            /// <summary>
            /// There are three scnarios, advancing from 0 to 1
            /// From 1 to 2, 3, 5, i.e. regular
            /// Reaching the end of the collection, after which reset needs to be called; 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext(){
                if (_index == 0) // Initialisation
                {
                    _current = _list.head;
                    _index++;
                    return true; 
                }
                else if(_current == null) return false;  //End of collection reached long ago
                _current = _current.Next;
                _index++;

                if (_current == null)
                    return false;
                else
                    return true; 
            }

            public void Reset()
            {
                _index = 0;
                _current = null;
            }
        }

        public sealed class ListItem<T>
        {
            public ListItem<T> Next;
            public T Value;

            public ListItem(ListItem<T> next, T value)
            {
                Next = next;
                Value = value;
            }
        }
    }
}
