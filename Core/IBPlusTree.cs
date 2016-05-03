using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    interface IBPlusTree<K, V> where K : IComparable<K>
    {
        Node<K, V> Root { get; }
        int MaxDegree { get; }
        int Count { get; }
        void Insert(K key, V value);
        bool TryFindExact(K key, out V value);
        bool TryFindExactOrSmaller(K key, out V value);
        List<V> FindRange(K begin, K end);
        Leaf<K, V> GetMinLeaf();
        Leaf<K, V> GetMaxLeaf();
        int GetHeight();

    }
}
