using AdvancedCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdvancedCache
{
    public class LRUCacheStore : ICacheStore
    {
        // maximum size of the store
        // after which the data that is least used will be dropped
        private readonly LRUCollection<CacheEntry> cacheEntries;
        private readonly ReaderWriterLockSlim wrLock;
        private readonly AdvancedCacheOptions options;

        public LRUCacheStore(AdvancedCacheOptions options)
        {
            
            wrLock = new ReaderWriterLockSlim();
            cacheEntries = new LRUCollection<CacheEntry>(options.MaxSize);
            this.options = options;
        }

        public void AddEntry(CacheEntry cacheEntry)
        {
            wrLock.EnterWriteLock();
            try
            {
                cacheEntries.Add(cacheEntry);
            }
            finally
            {
                wrLock.ExitWriteLock();
            }
        }

        public void Clear()
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

        public int Count()
        {
            wrLock.EnterReadLock();
            try
            {
                return cacheEntries.Count();
            }
            finally
            {
                wrLock.ExitReadLock();
            }
        }

        public void Dispose()
        {
        }

        public CacheEntry GetEntry(string key)
        {
            wrLock.EnterUpgradeableReadLock();
            try
            {
                var identifier = new CacheEntryIdentifier(key, options);
                var cacheEntry = cacheEntries.Get(identifier);
                // check if null
                if (cacheEntry == null)
                {
                    return null;
                }
                // check if entry has expired
                if (cacheEntry.HasExpired)
                {
                    wrLock.EnterWriteLock();
                    try
                    {
                        cacheEntries.Remove(identifier);
                        return null;
                    }
                    finally
                    {
                        wrLock.ExitWriteLock();
                    }
                }
                else
                {
                    // update the valid time
                    cacheEntry.UpdateValidUntil();
                    return cacheEntry;
                }
            }
            finally
            {
                wrLock.ExitUpgradeableReadLock();
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
            wrLock.EnterWriteLock();
            try
            {
                var identifier = new CacheEntryIdentifier(key, options);
                cacheEntries.Remove(identifier);
            }
            finally
            {
                wrLock.ExitWriteLock();
            }
        }


    }
}
