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

        public AdvancedCache()
        {
            cacheStore = new HashMemoryCacheStore();
        }

        public void AddEntry(string key, object value, TimeSpan validUntil)
        {
            throw new NotImplementedException();
        }

        public void ClearEntries()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntry(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
