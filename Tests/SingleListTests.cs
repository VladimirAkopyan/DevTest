using System;
using Xunit;

using Task;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Tests
{
    public class SingleListTests
    {


        /// <summary>
        /// List used for testing and comaprison
        /// </summary>
        public System.Collections.Generic.List<int> integerListSystem = new System.Collections.Generic.List<int>
        {
            1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4
        };
        SingleList<int> testedList = new SingleList<int>();


        public SingleListTests()
        {
            foreach (var value in integerListSystem)
            {
                testedList.Add(value);
            }
        }



        /// <summary>
        /// Basic Test to prove we can add items to the list and get what we'd expect
        /// </summary>
        /// <param name="values"></param>
        [Fact]
        public void Enumerate()
        {
            Assert.Equal(testedList.Count, integerListSystem.Count);

            int i = 0;
            foreach(var number in testedList)
            {               
                Assert.Equal(number, integerListSystem[i]);
                i++;
            }
        }

        [Fact]
        public void IndexOperatorRead()
        {
            for (int i = 0; i < integerListSystem.Count; i++)
            {
                Assert.Equal(integerListSystem[i], testedList[i]);
            }
        }

        [Fact]
        public void IndexOperatorWrite()
        {
            testedList[6] = -500;
            integerListSystem[6] = -500;
            testedList[8] = -200;
            integerListSystem[8] = -200;
            testedList[1] = -10;
            integerListSystem[1] = -10;
            testedList[9] = 666;
            integerListSystem[9] = 666;

            for (int i = 0; i < integerListSystem.Count; i++)
            {
                Assert.Equal(integerListSystem[i], testedList[i]);
            }
        }

        [Fact]
        public void RemoveAt(){
            //Remove At Index Tests
            testedList.RemoveAt(2);
            Assert.Equal(testedList.Count, integerListSystem.Count - 1); 
            Assert.Equal(testedList[2], integerListSystem[3]);

            testedList.RemoveAt(0);
            Assert.Equal(testedList.Count, integerListSystem.Count - 2);
            Assert.Equal(testedList[0], integerListSystem[1]);

            Assert.Equal(testedList.Count, integerListSystem.Count - 2);
        }

        [Fact]
        public void Insert()
        {
            //At the start
            testedList.Insert(0, 666);
            Assert.Equal(666, testedList[0]);
            integerListSystem.Insert(0, 666);
            Assert.Equal(integerListSystem[1], testedList[1]);
            //in the middle
            testedList.Insert(4, 999);
            Assert.Equal(999, testedList[4]);
            integerListSystem.Insert(4, 999);
            Assert.Equal(integerListSystem[6], testedList[6]);

            //Append at the end
            integerListSystem.Insert(integerListSystem.Count, 1234);
            testedList.Insert(testedList.Count, 1234);

            Assert.Equal(integerListSystem.Count, testedList.Count);

            for (int i = 0; i < testedList.Count; i++){
                Assert.Equal(integerListSystem[i], testedList[i]);
            }
        }

        private static void IndexComparisonHelper<T>(params T[] data)
        {
            //Comparing to a list here just because it implements all the operations. 
            //No particular reason this colleciton was chosen.
            var systemList = new System.Collections.Generic.List<T>(data);

            var testedList = new SingleList<T>();

            foreach (var value in systemList)
            {
                testedList.Add(value);
            }
            //Index Test
            foreach (var value in systemList)
            {
                var correctIndex = systemList.IndexOf(value);
                var testedIndex = testedList.IndexOf(value);
                Assert.Equal(correctIndex, testedIndex);
                Assert.True(testedList.Contains(value)); 
            }
            //Removal Tests
            for(int i = 0; i < data.Length; i ++)
            {
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

        public void IndexOfString(params string[] data)
        {
            IndexComparisonHelper(data); 
        }

         /// <summary>
         /// To see if the list correctly compares vallue types
         /// </summary>
        [Theory]
        [InlineData(1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4)]
        public void IndexOfValueType(params int[] data)
        {
            IndexComparisonHelper(data);
        }

        /// <summary>
        /// To see if the list correctly compares reference types
        /// </summary>
        [Fact]
        public void IndexOfRefType()
        {
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

        //TODO: As Enumerable Tests! 
        [Fact]
        public void AsEnumerableTest()
        {

        }

    }
}
