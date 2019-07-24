using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Interfaces
{
    public interface IEnumerator<T>
    {
        bool MoveNext();
        T Current
        {
            get;
        }
        void Reset();
    }
}
