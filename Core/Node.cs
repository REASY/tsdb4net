using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class Node <K, V>
    {
        public int KeyIndex { get; protected set; }

        public K[] Keys { get; protected set; }
        public abstract void Split(K key, out Node<K, V> rightNode, out K midElement);
        public abstract void Insert(K key, V value, out Node<K, V> node, out K k);
        public abstract override string ToString();
    }
}
