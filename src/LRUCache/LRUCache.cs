using LRUCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{
    public class LRUCache : Abstractions.ILRUCache
    {

        private ICacheStore cacheStore;
        private LRUCacheOptions options;

        public LRUCache(LRUCacheOptions options = null, ICacheStore cacheStore = null)
        {
            this.options = options ?? new LRUCacheOptions();
            this.cacheStore = cacheStore ?? new LRUCacheStore(this.options);
        }

        public void Add(string key, object value, TimeSpan? validUntil = null)
        {
            var expiration = validUntil ?? TimeSpan.FromDays(1000);
            var indentifier = new CacheEntryIdentifier(key, options);
            var cacheEntry = new CacheEntry(indentifier, value, expiration);
            cacheStore.AddEntry(cacheEntry);
        }

        public void Clear()
        {
            cacheStore.Clear();
        }

        public int Count()
        {
            return cacheStore.Count();
        }

        public void Dispose()
        {
            cacheStore.Dispose();
        }

        public T GetValue<T>(string key, T defaultValue = default)
        {
            var entry = cacheStore.GetEntry(key);
            if (entry == null)
                return default;
            return (T)entry.Value;
        }

        public void Remove(string key)
        {
            cacheStore.RemoveEntry(key);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var result = GetValue<T>(key);
            value = result;
            return result != default;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (cacheStore as IEnumerable).GetEnumerator();
        }
    }
}
