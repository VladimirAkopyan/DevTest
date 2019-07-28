using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Task
{
    public static class Extensions
    {
       
        public static IEnumerable<TResult> Select<TSource, TResult> (this SingleList<TSource> list, 
            Func<TSource, TResult> selector){

            if (selector == null) throw new ArgumentNullException(nameof(selector));

            foreach (TSource element in list)
            {
                yield return selector(element); 
            }
            
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this SingleList<TSource> list,
            Func<TSource, int, TResult> selector)
        {

            if (list == null) throw new ArgumentNullException(nameof(list));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            int i = 0; 
            foreach (TSource element in list)
            {
                yield return selector(element, i);
                i++; 
            }

        }
    }
}
