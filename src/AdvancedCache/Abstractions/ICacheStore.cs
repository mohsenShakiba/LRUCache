using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    interface ICacheStore: IEnumerable
    {
        void AddEntry(CacheEntry cacheEntry);
        void Clear();
        void RemoveEntry(string key);
        int Count();
        CacheEntry GetEntry(string key);
    }
}
