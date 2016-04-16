using Core;
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
            var n = ChooseSubtree(key, this);
            Node<K, V> newChildNode = null;
            K newChildPivotElement = default(K);
            n.Insert(key, value, out newChildNode, out newChildPivotElement);
            if (newChildNode != null)
            {
                // Will we get overflow?
                if (this.KeyIndex >= Keys.Length - 1)
                    this.Split(newChildPivotElement, newChildNode, out node, out k);
                else
                    this.AddKeyChild(newChildPivotElement, newChildNode);
            }
        }
        private void Split(K key,  Node<K, V> newChildNode, out Node<K, V> rightNode, out K midElement)
        {
            // TODO Do split without addition buffer
            K[] keyBuf = new K[Keys.Length + 1];
            var childBuf = new Node<K, V>[Keys.Length + 2];
            bool isKeyInserted = false;
            K newChildMinKey = newChildNode.Keys[0];
            bool isNewChildNodeInserted = false;
            int k = 0;
            for (int i = 0, j = 0; i < keyBuf.Length; i++, k++)
            {
                if (!isNewChildNodeInserted && newChildMinKey.CompareTo(Children[i].Keys[0]) < 0)
                {
                    childBuf[k] = newChildNode; isNewChildNodeInserted = true;
                    childBuf[k + 1] = Children[i];
                    k++;
                }
                else
                    childBuf[k] = Children[i];
                
                if (j < Keys.Length && key.CompareTo(Keys[j]) < 0 && !isKeyInserted)
                {
                    keyBuf[i] = key;
                    isKeyInserted = true;
                }
                else
                {
                    if (j < Keys.Length)
                        keyBuf[i] = Keys[j];
                    j++;
                }                
            }
            if (k != childBuf.Length)
            {
                if (isNewChildNodeInserted)
                    childBuf[k] = Children[Children.Length - 1];
                else
                    childBuf[k] = newChildNode;
            }

            if (isKeyInserted)
                keyBuf[keyBuf.Length - 1] = Keys[Keys.Length - 1];
            else
                keyBuf[keyBuf.Length - 1] = key;

            int midIndex = keyBuf.Length / 2;
            midElement = keyBuf[midIndex];
            K[] rightKeys = new K[Keys.Length];
            Node<K, V>[] rightNodes = new Node<K, V>[Keys.Length + 1];
            int rightIndex = 0;
            for (int i = midIndex + 1; i < keyBuf.Length; i++, rightIndex++)
            {
                rightKeys[rightIndex] = keyBuf[i];
            }
            for (int i = midIndex + 1, j = 0; i < childBuf.Length; i++, j++)
            {
                rightNodes[j] = childBuf[i];
            }
            rightNode = new InternalNode<K, V>(rightIndex - 1, rightKeys, rightNodes);

            int leftIndex = 0;
            for (int i = 0; i < Keys.Length; i++)
            {
                if (i < midIndex)
                {
                    Keys[leftIndex] = keyBuf[i];
                    leftIndex++;
                }
                else
                    Keys[leftIndex] = default(K);  // TODO Fix it after debugging
            }
            KeyIndex = leftIndex - 1;
            for (int i = 0; i < Children.Length; i++)
            {
                if (i <= midIndex)
                    Children[i] = childBuf[i];
                else
                    Children[i] = default(Node<K, V>);  // TODO Fix it after debugging
            }
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
        internal void AddChild(Node<K, V> node)
        {
            int childLen = 0;
            K minKey = node.Keys[0];
            int insertIndex = -1;
            bool isFound = false;
            for (int i = 0; i < Children.Length; i++)
            {
                if (Children[i] == null)
                {
                    childLen = i;
                    break;
                }
                if (!isFound && minKey.CompareTo(Children[i].Keys[0]) < 0) { insertIndex = i; isFound = true; }
            }
            if (insertIndex == -1) insertIndex = childLen;
            for (int i = childLen; i > insertIndex; i--)
            {
                Children[i] = Children[i - 1];
            }
            Children[insertIndex] = node;
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
                int insertIndex = SearchHelpers.UpperBound(Keys, KeyIndex + 1, key);
                if (insertIndex == -1) insertIndex = KeyIndex + 1;
                // Do we need to move keys and children?
                if (insertIndex <= KeyIndex)
                {
                    // Move to the right all keys starting with insertIndex
                    for (int i = KeyIndex + 1; i > insertIndex; i--)
                        Keys[i] = Keys[i - 1];
                    KeyIndex++;
                }
                else KeyIndex = insertIndex;
                Keys[insertIndex] = key;
                AddChild(node);
            }

        }
        public static Node<K, V> ChooseSubtree(K key, Node<K, V> node)
        {
            if (node is Leaf<K, V>) return node;
            else
            {
                var n = node as InternalNode<K, V>;
                int index = SearchHelpers.UpperBound(n.Keys, n.KeyIndex + 1, key);
                if (index == -1) index = n.KeyIndex + 1;
                node = n.Children[index];
                return node;
            }
        }

    }
}
