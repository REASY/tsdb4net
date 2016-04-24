using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    interface IBPlusTree<K, V> where K : IComparable<K>
    {
        void Insert(K key, V value);
        bool TryFindExact(K key, out V value);
        bool TryFindExactOrSmaller(K key, out V value);
        Leaf<K, V> GetMinLeaf();
        Leaf<K, V> GetMaxLeaf();
        int GetHeight();

    }
}
