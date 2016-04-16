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
        private readonly int NUMBER_OF_INSERTION = 100000;

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
            var mostLeftLeaf = bPlusTree.GetTheMostLeft();
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
