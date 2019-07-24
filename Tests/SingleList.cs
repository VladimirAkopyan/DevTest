using System;
using Xunit;

using Task;

namespace Tests
{
    public class SingleList
    {
        /// <summary>
        /// Basic Test to prove we can add items to the list and get what we'd expect
        /// </summary>
        /// <param name="values"></param>
        [Theory]
        [InlineData (1, 2, 3, 4, 6, 7, 8, -10, -20, 50, 4)]
        public void AddAndCheckValues(params int[] values)
        {

            var testedList = new SingleList<int>(); 

            foreach (var value in values)
            {
                testedList.Add(value);
            }
            Assert.Equal(testedList.Count, values.Length);

            int i = 0;
            foreach(var number in testedList)
            {
                int it = number;                
                Assert.Equal(number, values[i]);
                i++;
            }

            //var first = testedList[0];
            //var second = testedList[1]; 

        }

        /*
        /// <summary>
        /// To see if the list correctly compares ReferenceTypes
        /// </summary>
        /// <param name="values"></param>
        [Fact]
        public void IndexOfForReferenceType(params long[] values)
        {

        }

        /// <summary>
        /// To see if the list correctly compares strings, because strings are special 
        /// and should be compared by value despite being a reference type
        /// </summary>
        /// <param name="values"></param>
        [Fact]
        public void IndexOfForString(params long[] values)
        {

        }

        /// <summary>
        /// To see if the list correctly compares ValueTypes by value
        /// </summary>
        /// <param name="values"></param>
        [Fact]
        public void IndexOfForValueType(params long[] values)
        {

        }

        TODO: TEst ForEach: s

        */
    }
}
