using AdvancedCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdvancedCache
{
    class HashMemoryCacheStore : ICacheStore
    {

        private readonly ReaderWriterLockSlim wrLock = new ReaderWriterLockSlim();
        private readonly Hashtable hashtable = new Hashtable();

        public void AddEntry(CacheEntry cacheEntry)
        {
            if (cacheEntry == null)
                return;
            wrLock.EnterWriteLock();
            try
            {
                hashtable.Add(cacheEntry, cacheEntry);
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
                hashtable.Clear();
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
                return hashtable[key] as CacheEntry;
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
                return hashtable.GetEnumerator();
            }
            finally
            {
                wrLock.ExitReadLock();
            }
        }

        public void RemoveEntry(string key)
        {
            wrLock.EnterWriteLock();
            try
            {
                hashtable.Remove(key);
            }
            finally
            {
                wrLock.ExitWriteLock();
            }
        }
    }
}
