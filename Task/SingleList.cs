using System;


namespace Task
{
    /// <summary>
    /// A singly-linked list
    /// Stores a list of items in the order they're added
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class SingleList<T>
    {
        internal ListItem head;
        internal ListItem tail;
        internal int _count = 0;

        //For optimisation of index operations, will help sequential access 
        private IndexCache _indexCache = new IndexCache();  

        public SingleList(){
            head = null;
        }

        public void Add(T value){
            if (head != null) {
                var item = new ListItem(null, value);
                tail.Next = item;
                tail = item;
            }
            else {
                head = new ListItem(null, value);
                _indexCache.Set(head, 0);
                tail = head;
            }            
            _count++; 
        }

        public int Count => _count; 


        private ListItem Seek(int index)
        {
            if (index < _indexCache.Index)
            {
                _indexCache.Set(head, 0);

            }
            while (index > _indexCache.Index)
            {
                _indexCache.Item = _indexCache.Item.Next;
                _indexCache.Index++;
            }
            return _indexCache.Item;
        }


        //This is going to be very slow to traverse when not accessed sequentially; 
        //As it is a linked list, nothing but exhaustive search will do. 
        public T this[int index]{
            get
            {
                if ((uint)index >= (uint)_count) throw new ArgumentOutOfRangeException(); 
                return Seek(index).Value; 
            }
            set
            {
                if ((uint) index >= (uint)_count) throw new ArgumentOutOfRangeException();
                Seek(index).Value = value;
            }
        }
        
        /// <summary>
        /// Inserts the given item at a given position.
        /// Inserting at Index 0 has same effect as attaching something at the front of the array
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, T value){
            if ((uint)index > (uint)_count) throw new ArgumentOutOfRangeException();

            ListItem inserted = new ListItem(null, value);

            if (index == 0){
                inserted.Next = head;
                head = inserted;
            }
            else {
                var prev = Seek(index -1);
                inserted.Next = prev.Next;
                prev.Next = inserted;
            }
            if (index <= _indexCache.Index){ //Need to update our index to get correct results 
                _indexCache.Set(_indexCache.Item, _indexCache.Index + 1);
            }
            _count++;
        }


        public void RemoveAt(int index){
            if ((uint)index >= (uint)_count) throw new ArgumentOutOfRangeException();
            if (index == 0) head = head.Next;
            else
            {
                var prev = Seek(index -1);
                prev.Next = prev.Next.Next; 
            }
            _count--;
        }

        public void Clear(){
            _count = 0;
            head = new ListItem(null, default(T));
            _indexCache.Set(null, 0); 
            tail = head;
        }

        private (int index, ListItem previous) LocateValue(T value)
        {
            //Trying to make sure we use correct euqality comparer for value types, ref types and for strings/overriden 
            var c = System.Collections.Generic.EqualityComparer<T>.Default;

            if (c.Equals(head.Value, value)) return (0, null); //It's the head

            int i = 1;
            ListItem previous = head;
            while (previous.Next != null)
            {
                if (c.Equals(previous.Next.Value, value))
                {
                    return (i, previous);
                }
                i++;
                previous = previous.Next; 
            }
            return (-1, null); //Nothing found
        } 

        public bool Contains(T value){
            return LocateValue(value).index != -1; 
        }

        /// <summary>
        /// Returns -1 if not found 
        /// </summary>
        public int IndexOf(T value)
        {
            return LocateValue(value).index; 
        }

        public bool Remove(T value){
            var (index, previous) = LocateValue(value);

            if (previous != null){ //it's somewhere in the middle of the list
                previous.Next = previous.Next.Next;
                _count--; 
                return true;
            }
            else if (index == 0){ //It's the head of the list
                head = head.Next;
                _count--;
                return true;
            }
            else return false; //Does not exist
        }

        public Enumerator GetEnumerator(){
            return new Enumerator(this);
        }

/* TODO: 
        public IEnumeable<T> AsEnumerable()
        {
            throw new NotImplementedException();
        }
        */

        public struct Enumerator {
            SingleList<T> _list; 
            //Index here starts at 1 because .Net starts with MoveNext before it calls Current
            int _index;
            ListItem _currentNode, _nextNode;

            internal Enumerator(SingleList<T> singleList){
                _list = singleList;
                _index = 0;
                _currentNode = null;
                _nextNode = _list.head; 
            }

            /// <summary>
            /// Returns null when collection iterator is not in a valid state
            /// </summary>
            public T Current => _currentNode.Value;

            /// <summary>
            /// There are three scnarios, advancing from 0 to 1
            /// From 1 to 2, 3, 5, i.e. regular
            /// Reaching the end of the collection, after which reset needs to be called; 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext(){
                if (_nextNode == null)
                {
                    return false;  //End of collection reached
                }
                _currentNode = _nextNode; 
                _nextNode = _nextNode.Next;                
                _index++;
                return true; 
            }

            public void Reset(){
                _index = 0;                
                _currentNode = null;
                _nextNode = _list.head; 
            }
        }

        public sealed class ListItem{
            public ListItem Next;
            public T Value;

            public ListItem(ListItem next, T value)
            {
                Next = next;
                Value = value;
            }
        }

        /// <summary>
        /// Simular idea to the Iterator used by IEnumerable
        /// </summary>
        private class IndexCache
        {
            public ListItem Item;
            public int Index;

            public void Advance()
            {
                Index++;
                Item = Item.Next;
            }

            public void Set(ListItem item, int index)
            {
                Item = item;
                Index = index;
            }
        }
    }
}
