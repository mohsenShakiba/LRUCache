using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    /// <summary>
    /// and interface that is used to store and restore cache entries
    /// </summary>
    public interface IDataPersist
    {
        void StoreItems(IEnumerable<CacheEntry> cacheEntries);
        IEnumerable<CacheEntry> RestoreItems();
    }
}
