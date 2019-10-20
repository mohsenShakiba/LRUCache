using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace LRUCache.Tests
{
    public class FileBaseCacheStoreTests
    {

        private string DummyPath(string fileName)
        {
            return Path.Combine(Path.GetTempPath(), fileName);
        }

        [Fact]
        public void NullPath_MustThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileBasedCacheStore(null);
            });
        }

        [Fact]
        public void ValidPathEmptyResult_MustReturnValue()
        {
            var path = DummyPath("test");
            var fileBasedCacheStore = new FileBasedCacheStore(path);
            var cacheEntry = CacheEntry.New("KEY", "VALUE", TimeSpan.FromSeconds(1));
            fileBasedCacheStore.StoreItems(new CacheEntry[1] { cacheEntry });
            var restoredItems = fileBasedCacheStore.RestoreItems();
            var cacheEntryRetrievedFromDist = restoredItems.First();

            Assert.NotNull(restoredItems);
            Assert.True(restoredItems is IEnumerable<CacheEntry>);
            Assert.True(restoredItems.Count() == 1);

            Assert.Equal(cacheEntry, cacheEntryRetrievedFromDist);
            Assert.Equal(cacheEntry.Identifier.Key, cacheEntryRetrievedFromDist.Identifier.Key);
            Assert.Equal(cacheEntry.Value, cacheEntryRetrievedFromDist.Value);
            Assert.Equal(cacheEntry.ExpirationPeriod, cacheEntryRetrievedFromDist.ExpirationPeriod);
        }

    }
}
