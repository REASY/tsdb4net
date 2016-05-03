using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class BPlusTreeLock<K, V> : IBPlusTree<K, V> where K : IComparable<K>
    {
        private readonly BPlusTree<K, V> _bPlusTree;

        public int Count { get { return _bPlusTree.Count; } }
        public int MaxDegree { get { return _bPlusTree.MaxDegree; } }
        public Node<K, V> Root { get { return _bPlusTree.Root; } }

        public BPlusTreeLock(int maxDegree)
        {
            _bPlusTree = new BPlusTree<K, V>(maxDegree);
        }
        public int GetHeight()
        {
            lock (_bPlusTree)
                return _bPlusTree.GetHeight();
        }
        public Leaf<K, V> GetMaxLeaf()
        {
            lock (_bPlusTree)
                return _bPlusTree.GetMaxLeaf();
        }
        public Leaf<K, V> GetMinLeaf()
        {
            lock (_bPlusTree)
                return _bPlusTree.GetMinLeaf();
        }
        public void Insert(K key, V value)
        {
            lock (_bPlusTree)
                _bPlusTree.Insert(key, value);
        }
        public bool TryFindExact(K key, out V value)
        {
            lock (_bPlusTree)
                return _bPlusTree.TryFindExact(key, out value);
        }
        public bool TryFindExactOrSmaller(K key, out V value)
        {
            lock (_bPlusTree)
                return _bPlusTree.TryFindExactOrSmaller(key, out value);
        }
        public List<V> FindRange(K begin, K end)
        {
            lock (_bPlusTree)
                return _bPlusTree.FindRange(begin, end);
        }
    }
}
