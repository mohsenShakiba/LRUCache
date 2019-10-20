using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LRUCache.Abstractions
{
    public interface ICacheStore: IEnumerable, IDisposable
    {
        void AddEntry(CacheEntry cacheEntry);
        void Clear();
        void RemoveEntry(string key);
        int Count();
        CacheEntry GetEntry(string key);
    }
}
