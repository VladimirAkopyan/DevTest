using System;
using Xunit;

using Task;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Xunit.Abstractions;
using static Tests.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    public class SingleListTests {


        /// <summary>
        /// List used for testing and comaprison
        /// </summary>
        public List<int> _intListSystem = new List<int>{
            1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4
        };
        SingleList<int> _intListTested
            = new SingleList<int>();


        public SingleListTests() {
            foreach (var value in _intListSystem) {
                _intListTested.Add(value);
            }
        }



        /// <summary>
        /// Basic Test to prove we can add items to the list and get what we'd expect
        /// </summary>
        /// <param name="values"></param>
        [Fact]
        public void Enumerate() {
            Assert.Equal(_intListTested.Count, _intListSystem.Count);

            int i = 0;
            foreach (var number in _intListTested) {
                Assert.Equal(number, _intListSystem[i]);
                i++;
            }
        }

        [Fact]
        public void IndexOperatorRead() {
            for (int i = 0; i < _intListSystem.Count; i++) {
                Assert.Equal(_intListSystem[i], _intListTested[i]);
            }
        }

        [Fact]
        public void IndexOperatorWrite() {
            _intListTested[6] = -500;
            _intListSystem[6] = -500;
            _intListTested[8] = -200;
            _intListSystem[8] = -200;
            _intListTested[1] = -10;
            _intListSystem[1] = -10;
            _intListTested[9] = 666;
            _intListSystem[9] = 666;

            for (int i = 0; i < _intListSystem.Count; i++) {
                Assert.Equal(_intListSystem[i], _intListTested[i]);
            }
        }

        [Fact]
        public void RemoveAt() {
            //Remove At Index Tests
            _intListTested.RemoveAt(2);
            Assert.Equal(_intListTested.Count, _intListSystem.Count - 1);
            Assert.Equal(_intListTested[2], _intListSystem[3]);

            _intListTested.RemoveAt(0);
            Assert.Equal(_intListTested.Count, _intListSystem.Count - 2);
            Assert.Equal(_intListTested[0], _intListSystem[1]);

            Assert.Equal(_intListTested.Count, _intListSystem.Count - 2);
        }

        [Fact]
        public void Insert() {
            //At the start
            _intListTested.Insert(0, 666);
            Assert.Equal(666, _intListTested[0]);
            _intListSystem.Insert(0, 666);
            Assert.Equal(_intListSystem[1], _intListTested[1]);
            //in the middle
            _intListTested.Insert(4, 999);
            Assert.Equal(999, _intListTested[4]);
            _intListSystem.Insert(4, 999);
            Assert.Equal(_intListSystem[6], _intListTested[6]);

            //Append at the end
            _intListSystem.Insert(_intListSystem.Count, 1234);
            _intListTested.Insert(_intListTested.Count, 1234);

            Assert.Equal(_intListSystem.Count, _intListTested.Count);

            for (int i = 0; i < _intListTested.Count; i++) {
                Assert.Equal(_intListSystem[i], _intListTested[i]);
            }
        }

        private static void IndexComparisonHelper<T>(params T[] data) {
            //Comparing to a list here just because it implements all the operations. 
            //No particular reason this colleciton was chosen.
            var systemList = new List<T>(data);

            var testedList = new SingleList<T>();

            foreach (var value in systemList) {
                testedList.Add(value);
            }
            //Index Test
            foreach (var value in systemList) {
                var correctIndex = systemList.IndexOf(value);
                var testedIndex = testedList.IndexOf(value);
                Assert.Equal(correctIndex, testedIndex);
                Assert.True(testedList.Contains(value));
            }
            //Removal Tests
            for (int i = 0; i < data.Length; i++) {
                //Let's check it behaves in a manner identical to deference list 
                Assert.Equal(testedList.Remove(data[2]), systemList.Remove(data[2]));
                Assert.Equal(testedList.Count, systemList.Count);
            }
        }

        /// <summary>
        /// To see if the list correctly compares strings, because strings are immutable objects
        /// and should be compared by value despite being a reference type
        /// </summary>
        [Theory]
        [InlineData("rob", "martha", "sue", "kate", "bob", "bob")]
        [InlineData("rob", "rob", "rob", "rob", "rob", "sue")]

        public void IndexOfString(params string[] data) {
            IndexComparisonHelper(data);
        }

        /// <summary>
        /// To see if the list correctly compares vallue types
        /// </summary>
        [Theory]
        [InlineData(1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4)]
        public void IndexOfValueType(params int[] data) {
            IndexComparisonHelper(data);
        }

        /// <summary>
        /// To see if the list correctly compares reference types
        /// </summary>
        [Fact]
        public void IndexOfRefType() {
            TestDataClass[] data =
            {
                new TestDataClass{number1 = 1, number2 =2 },
                new TestDataClass{number1 = 2, number2 =2 },
                new TestDataClass{number1 = 3, number2 =2 },
                new TestDataClass{number1 = 5, number2 =2 },
                new TestDataClass{number1 = 6, number2 =2 },
                new TestDataClass{number1 = 1, number2 =2 },
                new TestDataClass{number1 = 1, number2 =3 }
            };
            IndexComparisonHelper(data);
        }


        [Fact]
        public void AsEnumerableTest() {
            var testedEnumerable = _intListTested.AsEnumerable();
            var systemEnumerable = _intListSystem.AsEnumerable();
            CompareEnumerables(testedEnumerable, systemEnumerable);
        }

    }
}
