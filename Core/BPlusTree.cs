using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BPlusTree<K, V> where K : IComparable<K>
    {
        private Node<K, V> _root;

        public int MaxDegree { get; private set; }

        public BPlusTree(int maxDegree)
        {
            MaxDegree = maxDegree;
            _root = new Leaf<K, V>(MaxDegree - 1);
        }
        public void Insert(K k, V v)
        {
            Node<K, V> newNode = null;
            K pivotElement =default(K);
            _root.Insert(k, v, out newNode, out pivotElement);
            if (newNode != null)
            {
                var newRoot = new InternalNode<K, V>(MaxDegree - 1);
                newRoot.AddKeyChild(pivotElement, _root);
                newRoot.Children[1] = newNode;
                _root = newRoot;
            }
        }
        public Leaf<K, V> GetTheMostLeft()
        {
            if (_root is Leaf<K, V>)
                return _root as Leaf<K, V>;
            else
            {
                var node = _root;
                while (node is InternalNode<K, V>)
                {
                    node = (node as InternalNode<K, V>).Children[0];
                }
                return node as Leaf<K, V>;
            }
        }
        internal void ShowAll()
        {
            Leaf<K, V> mostLeftLeaf = null;
            if (_root is Leaf<K, V>)
                mostLeftLeaf = _root as Leaf<K, V>;
            else
            {
                Node<K, V> node = _root;
                while (!(node is Leaf<K, V>))
                {
                    node = (node as InternalNode<K, V>).Children[0];
                }
                mostLeftLeaf = node as Leaf<K, V>;
            }
            Leaf<K, V> temp = mostLeftLeaf;
            while(temp != null)
            {
                for(int i = 0; i <= temp.KeyIndex; i++)
                {
                    Console.Write("{0} ", temp.Keys[i]);
                }
                temp = temp.Next;
            }
            Console.WriteLine();
        }
    }
}
