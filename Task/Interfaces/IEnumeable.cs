using System;
using System.Text;

namespace Task.Interfaces
{
    public interface IEnumeable<T>
    {
        IEnumerator<T> GetEnumerator();
    }
}
