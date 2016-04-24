using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class BPlusTreeRW<K, V> : IBPlusTree<K, V> where K : IComparable<K>
    {
        private readonly BPlusTree<K, V> _bPlusTree;
        private readonly ReaderWriterLockSlim _rwLock;

        public BPlusTreeRW(int maxDegree)
        {
            _bPlusTree = new BPlusTree<K, V>(maxDegree);
            _rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }
        public int GetHeight()
        {
            _rwLock.EnterReadLock();
            try { return _bPlusTree.GetHeight(); }
            finally { _rwLock.ExitReadLock(); }
        }
        public Leaf<K, V> GetMaxLeaf()
        {
            _rwLock.EnterReadLock();
            try { return _bPlusTree.GetMaxLeaf(); }
            finally { _rwLock.ExitReadLock(); }
        }
        public Leaf<K, V> GetMinLeaf()
        {
            _rwLock.EnterReadLock();
            try { return _bPlusTree.GetMinLeaf(); }
            finally { _rwLock.ExitReadLock(); }
        }
        public void Insert(K key, V value)
        {
            _rwLock.EnterWriteLock();
            try { _bPlusTree.Insert(key, value); }
            finally { _rwLock.ExitWriteLock(); }
        }
        public bool TryFindExact(K key, out V value)
        {
            _rwLock.EnterReadLock();
            try { return _bPlusTree.TryFindExact(key, out value); }
            finally { _rwLock.ExitReadLock(); }
        }
        public bool TryFindExactOrSmaller(K key, out V value)
        {
            _rwLock.EnterReadLock();
            try { return _bPlusTree.TryFindExactOrSmaller(key, out value); }
            finally { _rwLock.ExitReadLock(); }
        }
    }
}
