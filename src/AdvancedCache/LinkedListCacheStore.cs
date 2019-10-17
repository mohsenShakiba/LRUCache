using AdvancedCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdvancedCache
{
    class LinkedListCacheStore : ICacheStore
    {

        private readonly LinkedList<CacheEntry> cacheEntries;
        private readonly ReaderWriterLockSlim wrLock = new ReaderWriterLockSlim();

        public LinkedListCacheStore()
        {
            cacheEntries = new LinkedList<CacheEntry>();
        }

        public void AddEntry(CacheEntry cacheEntry)
        {
            wrLock.EnterWriteLock();
            try
            {
                cacheEntries.AddFirst(new LinkedListNode<CacheEntry>(cacheEntry));
            }
            finally
            {
                wrLock.ExitWriteLock();
            }
        }

        public void ClearEntries()
        {
            wrLock.EnterWriteLock();
            try
            {
                cacheEntries.Clear();
            }
            finally
            {
                wrLock.ExitWriteLock();
            }
        }

        public CacheEntry GetEntry(string key)
        {
            wrLock.EnterReadLock();
            try
            {
                var node = FindNodeByKey(key);
                return node?.Value;
            }
            finally
            {
                wrLock.ExitReadLock();
            }
        }

        public IEnumerator GetEnumerator()
        {
            wrLock.EnterReadLock();
            try
            {
                return cacheEntries.GetEnumerator();
            }
            finally
            {
                wrLock.ExitReadLock();
            }
        }

        public void RemoveEntry(string key)
        {
            var node = FindNodeByKey(key);
            if (node == null)
                return;
            cacheEntries.Remove(node);
        }

        private LinkedListNode<CacheEntry> FindNodeByKey(string key)
        {
            var cachEntry = new CacheEntry(key);
            var node = cacheEntries.Find(cachEntry);
            return node;
        }

        /// <summary>
        /// this method will bring the node to the start of the linked list
        /// this will make the most used cache entries more accessible and faster to search
        /// </summary>
        /// <param name="node"></param>
        private void MoveNodeToFront(LinkedListNode<CacheEntry> node)
        {
            if (node == null)
                return;
            cacheEntries.Remove(node);
            cacheEntries.AddFirst(node);
        }

    }
}
