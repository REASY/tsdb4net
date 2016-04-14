using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class LeafTests
    {
        [TestMethod]
        public void Insert_WithoutCreatingNewNode()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(2);
            Node<int, int> node;
            int pivotElement;
            leaf.Insert(1, 1, out node, out pivotElement);
            Assert.AreEqual(null, node);
            Assert.AreEqual(0, leaf.KeyIndex);

            leaf.Insert(2, 1, out node, out pivotElement);
            Assert.AreEqual(null, node);
            Assert.AreEqual(1, leaf.KeyIndex);

            Assert.AreEqual(1, leaf.Keys[0]);
            Assert.AreEqual(2, leaf.Keys[1]);
        }
        [TestMethod]
        public void Insert_MaxDegree3_CreateNewNode()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(2);
            Node<int, int> node;
            int pivotElement;
            leaf.Insert(1, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(0, leaf.KeyIndex);

            leaf.Insert(2, 1, out node, out pivotElement);
            Assert.IsNull( node);
            Assert.AreEqual(1, leaf.KeyIndex);

            leaf.Insert(3, 1, out node, out pivotElement);
            Assert.IsNotNull(node);
            Assert.AreEqual(2, pivotElement);
            Assert.AreEqual(1, node.KeyIndex);
            Assert.AreEqual(2, node.Keys[0]);
            Assert.AreEqual(3, node.Keys[1]);

            Assert.AreEqual(0, leaf.KeyIndex);
            Assert.AreEqual(1, leaf.Keys[0]);
            Assert.AreEqual(leaf.Next, node);
        }
        [TestMethod]
        public void Insert_MaxDegree7_CreateNewNode()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(6);
            Node<int, int> node;
            int pivotElement;
            leaf.Insert(6, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(0, leaf.KeyIndex);

            leaf.Insert(5, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(1, leaf.KeyIndex);

            leaf.Insert(4, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(2, leaf.KeyIndex);

            leaf.Insert(3, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(3, leaf.KeyIndex);

            leaf.Insert(2, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(4, leaf.KeyIndex);

            leaf.Insert(1, 1, out node, out pivotElement);
            Assert.IsNull(node);
            Assert.AreEqual(5, leaf.KeyIndex);

            leaf.Insert(0, 1, out node, out pivotElement);
            Assert.IsNotNull(node);
            Assert.AreEqual(3, pivotElement);
            Assert.AreEqual(2, leaf.KeyIndex);
            Assert.AreEqual(0, leaf.Keys[0]);
            Assert.AreEqual(1, leaf.Keys[1]);
            Assert.AreEqual(2, leaf.Keys[2]);

            Assert.AreEqual(3, node.KeyIndex);
            Assert.AreEqual(3, node.Keys[0]);
            Assert.AreEqual(4, node.Keys[1]);
            Assert.AreEqual(5, node.Keys[2]);
            Assert.AreEqual(6, node.Keys[3]);

            Assert.AreEqual(leaf.Next, node);
        }
        [TestMethod]
        public void AddKeyValue_KeysAreInOrder()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(4);
            int[] keys = new int[] { 1, 2, 3, 4 };
            int idx = 0;
            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 0);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 1);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 2);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 3);

            var keysCopy = keys.ToArray();
            Array.Sort(keysCopy);
            for (int i = 0; i < keys.Length; i++)
                Assert.AreEqual(keysCopy[i], leaf.Keys[i]);
        }
        [TestMethod]
        public void AddKeyValue_KeysAreNotInOrder()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(4);
            int[] keys = new int[] { 4, 3, 2, 1 };
            int idx = 0;
            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 0);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 1);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 2);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 3);

            var keysCopy = keys.ToArray();
            Array.Sort(keysCopy);
            for (int i = 0; i < keys.Length; i++)
                Assert.AreEqual(keysCopy[i], leaf.Keys[i]);
        }
        [TestMethod]
        public void AddKeyValue_KeysAreTheSame()
        {
            Leaf<int, int> leaf = new Leaf<int, int>(4);
            int[] keys = new int[] { 1, 1, 1, 1 };
            int idx = 0;
            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 0);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 1);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 2);

            leaf.AddKeyValue(keys[idx++], 1);
            Assert.AreEqual(leaf.KeyIndex, 3);

            var keysCopy = keys.ToArray();
            Array.Sort(keysCopy);
            for (int i = 0; i < keys.Length; i++)
                Assert.AreEqual(keysCopy[i], leaf.Keys[i]);
        }
        [TestMethod]
        public void AddKeyValue_MassTest()
        {
            Action<Leaf<int, int>, int[]> DoCheck = (leaf, keys) =>
            {
                for (int idx = 0; idx < keys.Length; idx++)
                {
                    leaf.AddKeyValue(keys[idx], 1);
                    Assert.AreEqual(leaf.KeyIndex, idx);
                }
                var keysCopy = keys.ToArray();
                Array.Sort(keysCopy);
                for (int i = 0; i < keys.Length; i++)
                    Assert.AreEqual(keysCopy[i], leaf.Keys[i]);
            };
            for (int leafSize = 2; leafSize <= 1000; leafSize++)
            {
                {
                    Leaf<int, int> leaf = new Leaf<int, int>(leafSize);
                    int[] keys = GetOrderedKeys(leafSize);
                    DoCheck(leaf, keys);
                }
                {
                    Leaf<int, int> leaf = new Leaf<int, int>(leafSize);
                    int[] keys = GetKeys(leafSize);
                    DoCheck(leaf, keys);
                }
            }
        }
        private int[] GetOrderedKeys(int size)
        {
            int[] keys = new int[size];
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0; i < size; i++)
                keys[i] = rnd.Next(1, int.MaxValue);
            Array.Sort(keys);
            return keys;
        }
        private int[] GetKeys(int size)
        {
            int[] keys = new int[size];
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0; i < size; i++)
                keys[i] = rnd.Next(1, int.MaxValue);
            Array.Sort(keys);
            return keys;
        }
    }
}
