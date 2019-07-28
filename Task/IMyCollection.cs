using System;
using System.Collections.Generic;
using System.Text;

namespace Task {
    public interface IMyEnumerable<T> {
        IMyEnumerator<T> GetEnumerator();
    }

    public interface IMyEnumerator<T> {
        T Current { get; }
        bool MoveNext();
        void Reset(); 
    }
}
