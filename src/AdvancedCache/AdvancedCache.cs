using AdvancedCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache
{
    class AdvancedCache : IAdvancedCache
    {

        private ICacheStore cacheStore;
        private AdvancedCacheOptions options;

        public AdvancedCache()
        {
            options = new AdvancedCacheOptions();
            cacheStore = new LRUCacheStore(options.MaxSize);
        }

        public AdvancedCache(AdvancedCacheOptions options, ICacheStore cacheStore)
        {
            this.options = options;
            this.cacheStore = cacheStore;
        }

        public void AddEntry(string key, object value, TimeSpan validUntil)
        {
            var cacheEntry = new CacheEntry(key, value, validUntil);
            cacheStore.AddEntry(cacheEntry);
        }

        public void ClearEntries()
        {
            cacheStore.Clear();
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

        public void RemoveEntry(string key)
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
