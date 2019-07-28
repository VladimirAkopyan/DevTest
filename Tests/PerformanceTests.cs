using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Task;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class PerformanceTests
    {
        //Large array of random numbers used for performance testing
        static readonly int[] BenchmarkData = new int[5000000];
        SingleList<int> testedList = new SingleList<int>();

        private readonly ITestOutputHelper output;

        public PerformanceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        static PerformanceTests()
        {
            Random random = new Random(12);
            for (int i = 0; i < BenchmarkData.Length; i++)
            {
                BenchmarkData[i] = random.Next(10000);
            }
        }

        /// <summary>
        /// Comparing perfomance against the system implementation of double linked list, as the closest thing
        /// </summary>
        [Fact]
        public void PerfTestInsert()
        {
            var systemList = new System.Collections.Generic.LinkedList<int>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var number in BenchmarkData)
            {
                systemList.AddLast(number);
            }
            stopwatch.Stop();
            var SystemListTime = stopwatch.ElapsedMilliseconds;
            var testedList = new SingleList<int>();
            stopwatch.Restart();
            foreach (var number in BenchmarkData)
            {
                testedList.Add(number);
            }
            stopwatch.Stop();
            output.WriteLine($"LinkedList {stopwatch.ElapsedMilliseconds}ms | System.LinkedList {SystemListTime}");
        }

        /// <summary>
        /// Comparing sequential access with List and LinkedList
        /// </summary>
        [Fact]
        public void PerfTestSequentialRead()
        {
            var systemList = new System.Collections.Generic.List<int>(BenchmarkData);
            var systemLinkedList = new System.Collections.Generic.LinkedList<int>(BenchmarkData);

            var testedLinkedList = new SingleList<int>();
            foreach (var number in BenchmarkData)
            {
                testedLinkedList.Add(number);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < BenchmarkData.Length; i++)
            {
                var it = systemList[i];
            }
            stopwatch.Stop();
            var systemListDuration = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            foreach (var item in systemLinkedList)
            {
                var it = item;
            }
            stopwatch.Stop();
            var systemLinkedListDuration = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            for (int i = 0; i < BenchmarkData.Length; i++)
            {
                var it = testedLinkedList[i];
            }
            stopwatch.Stop();
            var linkedListIterated = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            //Tested this for comparison, it's about three times faster 
            foreach (var item in testedLinkedList)
            {
                var it = item;
            }
            stopwatch.Stop();
            var linkedListEnumerated = stopwatch.ElapsedMilliseconds;
            output.WriteLine($"'Enumerator' {linkedListEnumerated} | Indexer | {linkedListIterated}| " +
                $"System.List {systemListDuration}| System.LinkedList {systemLinkedListDuration}");
        }

        /// <summary>
        /// Comparing random access with List, this will be bad 
        /// </summary>
        [Fact]
        public void PerfTestRandomRead()
        {
            var systemList = new System.Collections.Generic.List<int>();

            var testedList = new SingleList<int>();
            for (int i = 0; i < 50000; i++)
            {
                testedList.Add(BenchmarkData[i]);
                systemList.Add(BenchmarkData[i]);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < systemList.Count; i++)
            {
                var index = BenchmarkData[i % 50];
                var it = systemList[index];
            }
            stopwatch.Stop();
            var systemListTime = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            for (int i = 0; i < testedList.Count; i++)
            {
                var index = BenchmarkData[i % 50];
                var it = testedList[index];
            }
            stopwatch.Stop();
            output.WriteLine($"LinkedList {stopwatch.ElapsedTicks} t | System.List {systemListTime} t");
        }

    }
}
