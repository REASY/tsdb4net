using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class BPlusTreeTests
    {
        private readonly int NUMBER_OF_INSERTION = 10000;

        [TestMethod]
        public void Insert_IncreasingOrder()
        {
            for (int maxDegree = 3; maxDegree <= 101; maxDegree++)
            {
                BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(maxDegree);
                var itemsToInsert = GetIncreasingCollection(NUMBER_OF_INSERTION);

                foreach (var item in itemsToInsert)
                {
                    var k = (long)item;
                    var v = (long)item;
                    bPlusTree.Insert(k, v);
                }
                Assert.IsTrue(Helpers.CheckNodes(bPlusTree.Root));
                Assert.AreEqual(NUMBER_OF_INSERTION, bPlusTree.Count);
                CollectionAssert.AreEqual(itemsToInsert, DumpKeysOnLeafNodes(bPlusTree));
            }
        }
        [TestMethod]
        public void Insert_DecreasingOrder()
        {
            for (int maxDegree = 3; maxDegree <= 101; maxDegree++)
            {
                BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(maxDegree);
                var itemsToInsert = GetDecreasingCollection(NUMBER_OF_INSERTION);

                foreach (var item in itemsToInsert)
                {
                    var k = (long)item;
                    var v = (long)item;
                    bPlusTree.Insert(k, v);
                }
                itemsToInsert.Sort();
                Assert.IsTrue(Helpers.CheckNodes(bPlusTree.Root));
                Assert.AreEqual(NUMBER_OF_INSERTION, bPlusTree.Count);
                CollectionAssert.AreEquivalent(itemsToInsert, DumpKeysOnLeafNodes(bPlusTree));
            }
        }
        [TestMethod]
        public void Insert_RandomOrder()
        {
            for (int maxDegree = 3; maxDegree <= 101; maxDegree++)
            {
                BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(maxDegree);
                var itemsToInsert = GetRandomCollection(NUMBER_OF_INSERTION);

                foreach (var item in itemsToInsert)
                {
                    var k = (long)item;
                    var v = (long)item;
                    bPlusTree.Insert(k, v);
                }
                itemsToInsert.Sort();
                Assert.IsTrue(Helpers.CheckNodes(bPlusTree.Root));
                Assert.AreEqual(NUMBER_OF_INSERTION, bPlusTree.Count);
                CollectionAssert.AreEquivalent(itemsToInsert, DumpKeysOnLeafNodes(bPlusTree));
            }
        }
        [TestMethod]
        public void TryFindExact_ContainsKey()
        {
            BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(3);
            bPlusTree.Insert(1, 1);
            bPlusTree.Insert(2, 2);
            bPlusTree.Insert(3, 3);
            bPlusTree.Insert(4, 4);
            bPlusTree.Insert(5, 5);
            bPlusTree.Insert(6, 6);

            long value;
            Assert.IsTrue(bPlusTree.TryFindExact(1, out value));
            Assert.AreEqual(1, value);
            Assert.IsTrue(bPlusTree.TryFindExact(2, out value));
            Assert.AreEqual(2, value);
            Assert.IsTrue(bPlusTree.TryFindExact(3, out value));
            Assert.AreEqual(3, value);
            Assert.IsTrue(bPlusTree.TryFindExact(4, out value));
            Assert.AreEqual(4, value);
            Assert.IsTrue(bPlusTree.TryFindExact(5, out value));
            Assert.AreEqual(5, value);
            Assert.IsTrue(bPlusTree.TryFindExact(6, out value));
            Assert.AreEqual(6, value);
        }
        [TestMethod]
        public void TryFindExact_DoesntContainKey()
        {
            BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(3);
            bPlusTree.Insert(1, 1);
            bPlusTree.Insert(3, 3);
            bPlusTree.Insert(5, 5);
            bPlusTree.Insert(7, 7);
            bPlusTree.Insert(9, 9);
            bPlusTree.Insert(11, 11);

            long value;
            Assert.IsFalse(bPlusTree.TryFindExact(0, out value));
            Assert.IsFalse(bPlusTree.TryFindExact(2, out value));
            Assert.IsFalse(bPlusTree.TryFindExact(4, out value));
            Assert.IsFalse(bPlusTree.TryFindExact(6, out value));
            Assert.IsFalse(bPlusTree.TryFindExact(8, out value));
            Assert.IsFalse(bPlusTree.TryFindExact(10, out value));
        }
        [TestMethod]
        public void TryFindExact_Massive_ContainsKey()
        {
            for (int maxDegree = 3; maxDegree <= 101; maxDegree++)
            {
                BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(maxDegree);
                var itemsToInsert = GetIncreasingCollection(NUMBER_OF_INSERTION);
                foreach (var item in itemsToInsert)
                {
                    long k = item;
                    long v = item;
                    bPlusTree.Insert(k, v);
                }

                foreach (var item in itemsToInsert)
                {
                    long value;
                    long k = item;
                    Assert.IsTrue(bPlusTree.TryFindExact(k, out value));
                    Assert.AreEqual(item, value);
                }
            }
        }
        [TestMethod]
        public void FindRange_Test1()
        {
            BPlusTree<long, long> bPlusTree = new BPlusTree<long, long>(3);

            bPlusTree.Insert(5, 5);
            bPlusTree.Insert(7, 7);
            bPlusTree.Insert(9, 9);
            bPlusTree.Insert(11, 11);
            bPlusTree.Insert(13, 13);
            bPlusTree.Insert(15, 15);
            bPlusTree.Insert(17, 17);
            bPlusTree.Insert(19, 19);

            CollectionAssert.AreEqual(bPlusTree.FindRange(10, 12), new List<long>() { 11 });
            CollectionAssert.AreEqual(bPlusTree.FindRange(5, 19), new List<long>() { 5, 7, 9, 11, 13, 15, 17, 19 });
            CollectionAssert.AreEqual(bPlusTree.FindRange(5, 5), new List<long>() { 5 });
            CollectionAssert.AreEqual(bPlusTree.FindRange(5, 7), new List<long>() { 5, 7 });
            CollectionAssert.AreEqual(bPlusTree.FindRange(11, 17), new List<long>() { 11, 13, 15, 17 });
            CollectionAssert.AreEqual(bPlusTree.FindRange(3, 4), new List<long>() { });
        }
        private List<long> GetIncreasingCollection(int size)
        {
            var collection = new List<long>(size);
            for (int i = 0; i < size; i++)
                collection.Add(i + 1);
            return collection;
        }
        private List<long> GetDecreasingCollection(int size)
        {
            var collection = new List<long>(size);
            for (int i = 0; i < size; i++)
                collection.Add(size - i);
            return collection;
        }
        private List<long> GetRandomCollection(int size)
        {
            Random rnd = new Random(Environment.TickCount);
            HashSet<long> rndCollection = new HashSet<long>();
            for (int i = 1; i <= NUMBER_OF_INSERTION; i++)
            {
                int value = rnd.Next();
                while (rndCollection.Contains(value))
                    value = rnd.Next();
                rndCollection.Add(value);
            }
            return rndCollection.ToList();
        }
        internal List<long> DumpKeysOnLeafNodes(BPlusTree<long, long> bPlusTree)
        {
            var allKeys = new List<long>();
            var mostLeftLeaf = bPlusTree.GetMinLeaf();
            var temp = mostLeftLeaf;
            while (temp != null)
            {
                for (int i = 0; i <= temp.KeyIndex; i++)
                {
                    allKeys.Add(temp.Keys[i]);
                }
                temp = temp.Next;
            }
            return allKeys;
        }
    }
}
