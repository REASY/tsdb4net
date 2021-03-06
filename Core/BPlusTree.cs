﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BPlusTree<K, V> : IBPlusTree<K, V> where K : IComparable<K>
    {
        private int _lastRootIndex = 0;

        public Node<K, V> Root { get; private set; }
        public int MaxDegree { get; private set; }
        public int Count { get; private set; }

        public BPlusTree(int maxDegree)
        {
            if (maxDegree < 3)
                throw new ArgumentOutOfRangeException("maxDegree", maxDegree, "must be >= 3");
            MaxDegree = maxDegree;
            Root = new Leaf<K, V>(MaxDegree - 1);
        }
        public void Insert(K key, V value)
        {
            Node<K, V> newNode = null;
            K pivotElement = default(K);
            Root.Insert(key, value, out newNode, out pivotElement);
            if (newNode != null)
            {
                if (Root is Leaf<K, V> || Root.KeyIndex < _lastRootIndex)
                {
                    var newRoot = new InternalNode<K, V>(MaxDegree - 1);
                    newRoot.AddKeyChild(pivotElement, Root);
                    newRoot.Children[1] = newNode;
                    Root = newRoot;
                }
                else
                    (Root as InternalNode<K, V>).AddKeyChild(key, newNode);

            }
            _lastRootIndex = Root.KeyIndex;
            Count++;
        }
        public bool TryFindExact(K key, out V value)
        {
            value = default(V);
            Leaf<K, V> leaf = GetLeafThatMayContainKey(key, Root);
            int index = SearchHelpers.LowerBound(leaf.Keys, leaf.KeyIndex + 1, key);
            if (index == -1 || leaf.Keys[index].CompareTo(key) != 0) return false;
            value = leaf.Values[index];
            return true;
        }
        public bool TryFindExactOrSmaller(K key, out V value)
        {
            value = default(V);
            Leaf<K, V> leaf = GetLeafThatMayContainKey(key, Root);
            int index = SearchHelpers.LowerBound(leaf.Keys, leaf.KeyIndex + 1, key);
            if (index == 0 && leaf.Keys[index].CompareTo(key) > 0) return false;
            if (index == -1) index = leaf.KeyIndex;
            value = leaf.Values[index];
            return true;
        }
        public List<V> FindRange(K begin, K end)
        {
            var result = new List<V>();
            Leaf<K, V> leaf = GetLeafThatMayContainKey(begin, Root);
            int index = SearchHelpers.LowerBound(leaf.Keys, leaf.KeyIndex + 1, begin);
            if (index == -1) index = leaf.KeyIndex;
            bool shouldStop = false;
            while (leaf != null)
            {
                for (; index <= leaf.KeyIndex; index++)
                {
                    var key = leaf.Keys[index];
                    if (key.CompareTo(begin) < 0) continue;
                    if (end.CompareTo(key) < 0) { shouldStop = true; break; }
                    result.Add(leaf.Values[index]);
                }
                if (shouldStop)
                    break;
                leaf = leaf.Next;
                index = 0;
            }
            return result;
        }
        public Leaf<K, V> GetMinLeaf()
        {
            if (Root is Leaf<K, V>)
                return Root as Leaf<K, V>;
            else
            {
                var node = Root;
                while (node is InternalNode<K, V>)
                {
                    node = (node as InternalNode<K, V>).Children[0];
                }
                return node as Leaf<K, V>;
            }
        }
        public Leaf<K, V> GetMaxLeaf()
        {
            if (Root is Leaf<K, V>)
                return Root as Leaf<K, V>;
            else
            {
                var node = Root;
                while (node is InternalNode<K, V>)
                {
                    node = (node as InternalNode<K, V>).Children[node.KeyIndex + 1];
                    Debug.Assert(node != null);
                }
                return node as Leaf<K, V>;
            }
        }
        public int GetHeight()
        {
            if (Root is Leaf<K, V>)
                return 1;
            else
            {
                int height = 1;
                var node = Root;
                while (node is InternalNode<K, V>)
                {
                    node = (node as InternalNode<K, V>).Children[0];
                    height++;
                }
                return height;
            }
        }
        internal void ShowAll()
        {
            Leaf<K, V> mostLeftLeaf = GetMinLeaf();
            Leaf<K, V> temp = mostLeftLeaf;
            while (temp != null)
            {
                for (int i = 0; i <= temp.KeyIndex; i++)
                {
                    Console.Write("{0} ", temp.Keys[i]);
                }
                temp = temp.Next;
            }
            Console.WriteLine();
        }
        private static Leaf<K, V> GetLeafThatMayContainKey(K key, Node<K, V> node)
        {
            while ((node = InternalNode<K, V>.ChooseSubtree(key, node)) is InternalNode<K, V>) { }
            var leaf = node as Leaf<K, V>;
            return leaf;
        }
    }
}
