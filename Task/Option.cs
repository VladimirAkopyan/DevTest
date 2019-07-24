using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Option<T>
    {
        public readonly T Value;
        public readonly bool HasValue;

        Option(T value)
        {
            Value = value;
            HasValue = true;
        }

        Option()
        {
            HasValue = false;
        }

        public static Option<T> Some(T value) =>
            new Option<T>(value);

        public static Option<T> None() =>
            new Option<T>();
    }
}
