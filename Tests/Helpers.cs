using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests {
    public static class Helpers {

        /// <typeparam name="T">Type, won't work with "normal" reference types, except strings</typeparam>
        /// <param name="produced">A list of reqults that were produced by the Linq query implemented by me</param>
        /// <param name="expected">A list of results that were produced by a system linq query, 
        /// this is used as the benchmark</param>
        public static void CompareEnumerables<T>(IEnumerable<T> produced, IEnumerable<T> expected) {

            var producedArray = produced.ToArray();
            var expectedArray = expected.ToArray();

            Assert.Equal(expectedArray.Length, producedArray.Length);

            for (int i = 0; i < expectedArray.Length; i++) {
                Assert.Equal(expectedArray[i], producedArray[i]);
            }
        }
    }
}
