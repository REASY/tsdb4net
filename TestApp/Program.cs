using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        private const int NUMBER_OF_INSERTION = 1000000;

        struct Measurement
        {
            long Id;
            long Time;
            double Value;
            int Tag;
            int User;
        }

        static void Main(string[] args)
        {
            List<int> l = new List<int>(1000000);
            for (int i = 0; i < 10000; i++)
                l.Add(i);
            CompareLinearAndBinarySearch();
            Console.WriteLine();
            Console.WriteLine("Testing BPlusTree");
            Console.WriteLine("*******************************************************************");
            TestInDecreasingOrder();
            TestInIncreasingOrder();
            TestInRandomOrder();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("L: {0}", l.Count);
            Console.ReadLine();
        }
        private static void CompareLinearAndBinarySearch()
        {
            int arraySize = 61;
            Console.WriteLine("CompareLinearAndBinarySearch. ArraySize: {0}", arraySize);
            Console.WriteLine("*******************************************************************");
            int[] keys = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
                keys[i] = i + 1;
            Stopwatch sw = Stopwatch.StartNew();
            long k = 0;
            for (int i = 0; i < 1000000; i++)
                k += Helpers.UpperBoundLinear(keys, arraySize - 1, arraySize);
            sw.Stop();
            Console.WriteLine("K: {0}. UpperBoundLinear: {1} ms", k, sw.ElapsedMilliseconds);

            sw.Restart();
            k = 0;
            for (int i = 0; i < 1000000; i++)
                k += Helpers.UpperBoundLinear4(keys, arraySize - 1, arraySize);
            sw.Stop();
            Console.WriteLine("K: {0}. UpperBoundLinear4: {1} ms", k, sw.ElapsedMilliseconds);

            sw.Restart();
            k = 0;
            for (int i = 0; i < 1000000; i++)
                k += Helpers.UpperBoundLinear8(keys, arraySize - 1, arraySize);
            sw.Stop();
            Console.WriteLine("K: {0}. UpperBoundLinear8: {1} ms", k, sw.ElapsedMilliseconds);

            sw.Restart();
            k = 0;
            for (int i = 0; i < 1000000; i++)
                k += Helpers.UpperBoundBinary(keys, arraySize - 1, arraySize);
            sw.Stop();
            Console.WriteLine("K: {0}. UpperBoundBinary: {1} ms", k, sw.ElapsedMilliseconds);

            Console.WriteLine("*******************************************************************");
        }
        private static void TestInDecreasingOrder()
        {
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(51);
            for (int i = NUMBER_OF_INSERTION; i >= 1; i--)
            {
                bPlusTree.Insert(i, new Measurement());
            }
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Decreasing order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = NUMBER_OF_INSERTION; i >= 1; i--)
            {
                allWrittenNumbers.Add(i);
            }
            CheckCorrectness(bPlusTree, allWrittenNumbers);
        }
        private static void TestInIncreasingOrder()
        {
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(51);
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                bPlusTree.Insert(i, new Measurement());
            }
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Increasing order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);

            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                allWrittenNumbers.Add(i);
            }
            CheckCorrectness(bPlusTree, allWrittenNumbers);
        }
        private static void TestInRandomOrder()
        {
            Random rnd = new Random(Environment.TickCount);
            
            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                int value = rnd.Next();
                while (allWrittenNumbers.Contains(value))
                    value = rnd.Next();
                allWrittenNumbers.Add(value);
            }
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(51);
            foreach(var value in allWrittenNumbers)
                bPlusTree.Insert(value, new Measurement());
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Random order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            CheckCorrectness(bPlusTree, allWrittenNumbers);
        }
        private static void CheckCorrectness(BPlusTree<long, Measurement> bPlusTree, HashSet<long> allWrittenNumbers)
        {
            var leaf = bPlusTree.GetTheMostLeft();
            HashSet<long> keysInBTree = new HashSet<long>();
            while (leaf != null)
            {
                for (int i = 0; i < leaf.KeyIndex; i++)
                {
                    keysInBTree.Add(leaf.Keys[i]);
                }
                keysInBTree.Add(leaf.Keys[leaf.KeyIndex]);
                leaf = leaf.Next;
            }
            allWrittenNumbers.ExceptWith(keysInBTree);
            Trace.Assert(0 == allWrittenNumbers.Count);
            if (allWrittenNumbers.Count == 0)
                Console.WriteLine("CheckCorrectness: OK");
            else
                Console.WriteLine("CheckCorrectness: FAIL");
        }
    }
}
