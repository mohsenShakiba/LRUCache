using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    interface ICacheStore: IEnumerable
    {
        void AddEntry(CacheEntry cacheEntry);
        void ClearEntries();
        void RemoveEntry(string key);
        CacheEntry GetEntry(string key);
    }
}
