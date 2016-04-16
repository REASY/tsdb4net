using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Leaf<K, V> : Node<K, V> where K: IComparable<K>
    {
        private readonly V[] _values;

        public V[] Values { get { return _values; } }
        public Leaf<K, V> Next { get; private set; }

        internal Leaf(int keyIndex, K[] keys, V[] values)
        {
            KeyIndex = keyIndex;
            Keys = keys;
            _values = values;
            Next = null;
        }
        public Leaf(int keysCount)
        {
            KeyIndex = -1;
            Keys = new K[keysCount];
            _values = new V[keysCount];
            Next = null;
        }
        public override void Insert(K key, V value, out Node<K, V> node, out K pivotElement)
        {
            node = null;
            pivotElement = default(K);
            if (KeyIndex >= Keys.Length - 1)
            {
                Split(key, out node, out pivotElement);
                if (key.CompareTo(pivotElement) < 0)
                    this.AddKeyValue(key, value);
                else
                    (node as Leaf<K, V>).AddKeyValue(key, value);
            }
            else
                this.AddKeyValue(key, value);
        }
        public void Split(K key, out Node<K, V> rightNode, out K midElement)
        {
            midElement = default(K);
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            K[] rightKeys = null;
            V[] rightValues = null;
            Helpers.SplitLeaf(this.Keys, this.Values, key, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
            var leafNode = new Leaf<K, V>(rightKeyIndex, rightKeys, rightValues);
            var next = Next;
            Next = leafNode;
            leafNode.Next = next;
            rightNode = leafNode;
            KeyIndex = leftKeyIndex;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Leaf. KeyIndex: " + KeyIndex.ToString() + ". Keys: [");
            for (int i = 0; i <= KeyIndex; i++)
            {
                sb.Append(Keys[i]);
                if (i != KeyIndex)
                    sb.Append(" ");
            }
            sb.Append("]");
            return sb.ToString();
        }
        internal void AddKeyValue(K key, V value)
        {
            Debug.Assert(KeyIndex < Keys.Length - 1);
            if (KeyIndex == -1)
            {
                KeyIndex++;
                Keys[KeyIndex] = key;
                Values[KeyIndex] = value;
            }
            else
            {
                int insertIndex = SearchHelpers.UpperBound(Keys, KeyIndex + 1, key);
                if (insertIndex == -1) insertIndex = KeyIndex + 1;
                if (insertIndex <= KeyIndex)
                {
                    for (int i = KeyIndex + 1; i > insertIndex; i--)
                    {
                        Keys[i] = Keys[i - 1];
                        Values[i] = Values[i - 1];
                    }
                }
                Keys[insertIndex] = key;
                Values[insertIndex] = value;
                if (insertIndex <= KeyIndex) KeyIndex++;
                else KeyIndex = insertIndex;
            }
        }
    }
}
