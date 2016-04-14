using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class InternalNode<K, V> : Node<K, V> where K : IComparable<K>
    {
        private Node<K, V>[] _children;

        public Node<K, V>[] Children { get { return _children; } }

        public InternalNode(int keysCount)
        {
            KeyIndex = -1;
            Keys = new K[keysCount];
            _children = new Node<K, V>[keysCount + 1];
        }

        public InternalNode(int keyIndex, K[] keys, Node<K, V>[] children)
        {
            KeyIndex = keyIndex;
            Keys = keys;
            _children = children;
        }
        public override void Insert(K key, V value, out Node<K, V> node, out K k)
        {
            node = null;
            k = default(K);
            var n = Helpers.ChooseSubtree(key, this);
            Node<K, V> newChildNode = null;
            K newChildPivotElement = default(K);
            n.Insert(key, value, out newChildNode, out newChildPivotElement);
            if (newChildNode != null)
            {
                // Will we get overflow?
                if (KeyIndex >= Keys.Length - 1)
                {
                    Node<K, V> newNode = null;
                    K newNodePivotElement = default(K);
                    Split(key, out newNode, out newNodePivotElement);
                    if (newChildPivotElement.CompareTo(newNodePivotElement) < 0)
                        this.AddKeyChild(newChildPivotElement, newChildNode);
                    else
                        (newNode as InternalNode<K, V>).AddKeyChild(newChildPivotElement, newChildNode);
                    node = newNode;
                    k = newNodePivotElement;
                }
                else
                {
                    this.AddKeyChild(newChildPivotElement, newChildNode);
                }
            }
        }

        public override void Split(K key, out Node<K, V> rightNode, out K midElement)
        {
            rightNode = null;
            midElement = default(K);
            int leftKeyIndex = 0;
            int rightKeyIndex = 0;
            int childIndex = 0;
            K[] rightKeys = null;
            Node<K, V>[] rightValues = null;

            Helpers.SplitNode(Keys, Children, key, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out childIndex, out midElement);
            rightNode = new InternalNode<K, V>(rightKeyIndex, rightKeys, rightValues);
            KeyIndex = leftKeyIndex;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("InternalNode. KeyIndex: " + KeyIndex.ToString() + ". Keys: [");
            for (int i = 0; i <= KeyIndex; i++)
            {
                sb.Append(Keys[i]);
                if (i != KeyIndex)
                    sb.Append(" ");
            }
            sb.Append("]");
            return sb.ToString();
        }
        internal void AddKeyChild(K key, Node<K, V> node)
        {
            Debug.Assert(KeyIndex < Keys.Length);
            if (KeyIndex == -1)
            {
                KeyIndex++;
                Keys[KeyIndex] = key;
                if (Children[KeyIndex] != default(Node<K, V>))
                    Children[KeyIndex + 1] = node;
                else
                    Children[KeyIndex] = node;
            }
            else
            {
                // Find the index where to do insertion
                int insertIndex = Helpers.UpperBound(Keys, KeyIndex, key);
                // Do we need to move keys and children?
                if (insertIndex <= KeyIndex)
                {
                    // Move to the right all keys starting with insertIndex
                    // Move to the right all greater or equal children (insertIndex + 1)
                    for (int i = KeyIndex + 1; i > insertIndex; i--)
                    {
                        Keys[i] = Keys[i - 1];
                        Children[i + 1] = Children[i];
                    }
                    KeyIndex++;
                }
                else KeyIndex = insertIndex;
                Keys[insertIndex] = key;
                Children[insertIndex + 1] = node;
            }

        }


    }
}
