using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BPlusTree<K, V> where K : IComparable<K>
    {
        private int _lastRootIndex = 0;

        public Node<K, V> Root {get; private set;}
        public int MaxDegree { get; private set; }
        public int Count { get; private set; }
        public int Height { get; private set; }

        public BPlusTree(int maxDegree)
        {
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
                    Height++;
                }                
                else
                    (Root as InternalNode<K, V>).AddKeyChild(key, newNode);

            }
            _lastRootIndex = Root.KeyIndex;
            Count++;
        }
        public bool TryFind(K k, out V value)
        {
            value = default(V);
            var node = Root;
            while ((node = InternalNode<K, V>.ChooseSubtree(k, node)) is InternalNode<K, V>) { }
            // TODO Impl when write LowerBound
            return true;
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
        internal ICollection<K> DumpKeysOnLeafNodes()
        {
            var allKeys = new List<K>();
            Leaf<K, V> mostLeftLeaf = GetMinLeaf();
            Leaf<K, V> temp = mostLeftLeaf;
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
    }
}
