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
            var stringsProduced = _intListTest.Select((n, i) => $"{i}: {n}");
            var stringsExpected = _intListSystem.Select((n, i) => $"{i}: {n}");
            comparisonHelper(stringsProduced, stringsExpected);
        }

        [Fact]
        public void Where()
        {
            var evenNumbersExpected = _intListSystem.Where(n => (n % 2) == 0);
            var evenNumbersProduced = _intListTest.Where(n => (n % 2) == 0);
            comparisonHelper(evenNumbersProduced, evenNumbersExpected);
        }

        [Fact]
        public void WhereWithIndex() {
            var evenNumbersExpected = _intListSystem.Where((number, i) => (number < i));
            var evenNumbersProduced = _intListTest.Where((number, i) => (number < i));
            comparisonHelper(evenNumbersProduced, evenNumbersExpected);
        }



        /// <typeparam name="T">Type, won't work with reference types, except strings</typeparam>
        /// <param name="produced">A list of reqults that were produced by the Linq query implemented by me</param>
        /// <param name="expected">A list of results that were produced by a system linq query, 
        /// this is used as the benchmark</param>
        private void comparisonHelper<T>(System.Collections.Generic.IEnumerable<T> produced, 
            System.Collections.Generic.IEnumerable<T> expected)
        {
            var producedArray = produced.ToArray();
            var expectedArray = expected.ToArray();

            Assert.Equal(expectedArray.Length, producedArray.Length);

            for(int i =0; i < expectedArray.Length; i++)
            {
                Assert.Equal(expectedArray[i], producedArray[i]);
            }
        }
    }
}
