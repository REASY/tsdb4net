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
            long decreasingOrderTime = 0;
            long increasingOrderTime = 0;
            long randomOrderTime = 0;
            int nRepeat = 10;
            int maxDegree = 51;
            Console.WriteLine("MaxDegree: {0}\r\n", maxDegree);
            for (int i = 1; i <= nRepeat; i++)
            {
                Console.WriteLine("Test {0}", i);
                Console.WriteLine("*******************************************************************");
                decreasingOrderTime += TestInDecreasingOrder(maxDegree);
                increasingOrderTime += TestInIncreasingOrder(maxDegree);
                randomOrderTime += TestInRandomOrder(maxDegree);
                Console.WriteLine("*******************************************************************\r\n");
            }
            Console.WriteLine("Average times:");
            Console.WriteLine("Decreasing order: {0} ms", (double)decreasingOrderTime / nRepeat);
            Console.WriteLine("Increasing order: {0} ms", (double)increasingOrderTime / nRepeat);
            Console.WriteLine("Random order: {0} ms", (double)randomOrderTime / nRepeat);

            Console.WriteLine("*******************************************************************");
            Console.WriteLine("L: {0}", l.Count);
            Console.ReadLine();
        }
        private static void CompareLinearAndBinarySearch()
        {
            for (int arraySize = 1; arraySize < 25; arraySize++)
            {
                Console.WriteLine("CompareLinearAndBinarySearch. ArraySize: {0}", arraySize);
                Console.WriteLine("*******************************************************************");
                int[] keys = new int[arraySize];
                for (int i = 0; i < arraySize; i++)
                    keys[i] = i + 1;
                Stopwatch sw = Stopwatch.StartNew();
                long k = 0;
                for (int i = 0; i < 1000000; i++)
                    k += SearchHelpers.UpperBoundLinear(keys, arraySize, arraySize);
                sw.Stop();
                Console.WriteLine("K: {0}. UpperBoundLinear: {1} ms", k, sw.ElapsedMilliseconds);

                sw.Restart();
                k = 0;
                for (int i = 0; i < 1000000; i++)
                    k += SearchHelpers.UpperBoundLinear4(keys, arraySize, arraySize);
                sw.Stop();
                Console.WriteLine("K: {0}. UpperBoundLinear4: {1} ms", k, sw.ElapsedMilliseconds);

                sw.Restart();
                k = 0;
                for (int i = 0; i < 1000000; i++)
                    k += SearchHelpers.UpperBoundLinear8(keys, arraySize, arraySize);
                sw.Stop();
                Console.WriteLine("K: {0}. UpperBoundLinear8: {1} ms", k, sw.ElapsedMilliseconds);

                sw.Restart();
                k = 0;
                for (int i = 0; i < 1000000; i++)
                    k += SearchHelpers.UpperBoundBinary(keys, arraySize, arraySize);
                sw.Stop();
                Console.WriteLine("K: {0}. UpperBoundBinary: {1} ms", k, sw.ElapsedMilliseconds);

                Console.WriteLine("*******************************************************************\r\n");
            }
        }
        private static long TestInDecreasingOrder(int maxDegree)
        {
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(maxDegree);
            for (int i = NUMBER_OF_INSERTION; i >= 1; i--)
            {
                bPlusTree.Insert(i, new Measurement());
            }
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Decreasing order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            Console.WriteLine("Height: {0} ", bPlusTree.GetHeight());
            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = NUMBER_OF_INSERTION; i >= 1; i--)
            {
                allWrittenNumbers.Add(i);
            }
            CheckCorrectness(bPlusTree, allWrittenNumbers);
            return sw.ElapsedMilliseconds;
        }
        private static long TestInIncreasingOrder(int maxDegree)
        {
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(maxDegree);
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                bPlusTree.Insert(i, new Measurement());
            }
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Increasing order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            Console.WriteLine("Height: {0} ", bPlusTree.GetHeight());

            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                allWrittenNumbers.Add(i);
            }
            CheckCorrectness(bPlusTree, allWrittenNumbers);
            return sw.ElapsedMilliseconds;
        }
        private static long TestInRandomOrder(int maxDegree)
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
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(maxDegree);
            foreach (var value in allWrittenNumbers)
            {
                bPlusTree.Insert(value, new Measurement());
            }
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Random order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            Console.WriteLine("Height: {0} ", bPlusTree.GetHeight());
            CheckCorrectness(bPlusTree, allWrittenNumbers);
            return sw.ElapsedMilliseconds;
        }
        private static void TestInRandomOrder2()
        {
            Random rnd = new Random(Environment.TickCount);

            HashSet<long> allWrittenNumbers = new HashSet<long>();
            for (int i = 1; i <= 87; i++)
            {
                int value = rnd.Next();
                while (allWrittenNumbers.Contains(value))
                    value = rnd.Next();
                allWrittenNumbers.Add(value);
            }
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(12);
            foreach (var value in allWrittenNumbers)
                bPlusTree.Insert(value, new Measurement());
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Random order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            Console.WriteLine("Height: {0} ", bPlusTree.GetHeight());
            CheckCorrectness(bPlusTree, allWrittenNumbers);
        }
        private static void TestInRandomOrder2(int maxDegree, long[] original)
        {
            Random rnd = new Random(Environment.TickCount);

            var copy = original.ToArray();
            Array.Sort(copy);
            long id = 1;
            Dictionary<long, long> dict = new Dictionary<long, long>();
            foreach (var item in copy)
            {
                dict[item] = id;
                id++;
            }
            foreach (var value in original)
            {
                Console.WriteLine(dict[value]);
            }
            long memBefore = GC.GetTotalMemory(false);
            Stopwatch sw = Stopwatch.StartNew();
            BPlusTree<long, Measurement> bPlusTree = new BPlusTree<long, Measurement>(maxDegree);
            foreach (var value in original)
            {
                bPlusTree.Insert(value, new Measurement());
                Console.WriteLine("Total number of elements: {0}", bPlusTree.GetCnt());
            }
            Helpers.CheckNodes(bPlusTree.Root);
            sw.Stop();
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Random order. Insert {0} elements: {1} ms", NUMBER_OF_INSERTION, sw.ElapsedMilliseconds);
            Console.WriteLine("Memory: {0} MBytes", (memAfter - memBefore) / 1024 / 1024);
            Console.WriteLine("Height: {0} ", bPlusTree.GetHeight());
            CheckCorrectness(bPlusTree, original);
        }
        private static void CheckCorrectness<K, V>(BPlusTree<K, V> bPlusTree, IEnumerable<K> allWrittenNumbers) where K :IComparable<K>
        {
            bool areNodesOk = Helpers.CheckNodes(bPlusTree.Root);
            var leaf = bPlusTree.GetTheMostLeft();
            var keysInBTree = new HashSet<K>();
            while (leaf != null)
            {
                for (int i = 0; i <= leaf.KeyIndex; i++)
                {
                    keysInBTree.Add(leaf.Keys[i]);
                }
                leaf = leaf.Next;
            }
            int uniqCnt = allWrittenNumbers.Except(keysInBTree).Count();
            Trace.Assert(0 == uniqCnt);

            if (uniqCnt == 0 && areNodesOk)
                Console.WriteLine("CheckCorrectness: OK");
            else
                Console.WriteLine("CheckCorrectness: FAIL");

        }
    }
}
