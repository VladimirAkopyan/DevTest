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

            //Create a function that just discards the index and calls the selector
            Func<TSource, int, TResult> selectWithIterator = (i, n) => selector(i);
            return Select(list, selectWithIterator);
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this SingleList<TSource> list,
            Func<TSource, int, TResult> selector) {

            if (list == null) throw new ArgumentNullException(nameof(list));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            int i = 0; 
            foreach (TSource element in list){
                yield return selector(element, i);
                i++; 
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this SingleList<TSource> list,
            Predicate<TSource> predicate) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            //Create a function that just discards the index and calls the selector
            Func<TSource, int, bool> selectWithIterator = (item, n) => predicate(item);
            return Where(list, selectWithIterator);
        }

        public static IEnumerable<TSource> Where<TSource>(this SingleList<TSource> list,
            Func<TSource, int, bool> predicate) {

            if (list == null) throw new ArgumentNullException(nameof(list));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int i = 0;
            foreach (TSource element in list) {
                if(predicate(element, i)) yield return element;
                i++;
            }
        }


    }
}
