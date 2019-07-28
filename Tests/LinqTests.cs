using System;
using Xunit;

using Task;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Xunit.Abstractions;
using System.Linq;

namespace Tests
{
    public class LinqTests
    {
        /// <summary>
        /// List used for testing and comaprison
        /// </summary>
        public System.Collections.Generic.List<int> _intListSystem = new System.Collections.Generic.List<int>
        {
            1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4, 56, 2342, 242, 23423, 5
        };
        SingleList<int> _intListTest = new SingleList<int>();


        public LinqTests()
        {
            foreach (var value in _intListSystem)
            {
                _intListTest.Add(value);
            }
        }


        [Fact]
        public void Select()
        {
            var stringsResult = _intListTest.Select(n => $"{n}"); 
            var stringsExpected = _intListSystem.Select(n => $"{n}");
            comparisonHelper(stringsResult, stringsExpected);
        }

        [Fact]
        public void SelectWithIndex()
        {
            var stringsResult = _intListTest.Select((n, i) => $"{i}: {n}");
            var stringsExpected = _intListSystem.Select((n, i) => $"{i}: {n}");
            comparisonHelper(stringsResult, stringsExpected);
        }

        [Fact]
        public void Where()
        {
            var evenNumbersExpected = _intListSystem.Where(n => (n % 2) == 0); 
        }

        /// <typeparam name="T">Type, won't work with reference types, except strings</typeparam>
        /// <param name="result">A list of reqults that were produced by the Linq query implemented by me</param>
        /// <param name="expected">A list of results that were produced by a system linq query, 
        /// this is used as the benchmark</param>
        private void comparisonHelper<T>(System.Collections.Generic.IEnumerable<T> result, 
            System.Collections.Generic.IEnumerable<T> expected)
        {
            var resultArray = result.ToArray();
            var expectedArray = expected.ToArray();

            Assert.Equal(expectedArray.Length, resultArray.Length);

            for(int i =0; i < expectedArray.Length; i++)
            {
                Assert.Equal(expectedArray[i], resultArray[i]);
            }
        }
    }
}
