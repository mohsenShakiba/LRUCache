using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace LRUCache.Tests
{
    public class LRUCacheStoreTests
    {

        [Fact]
        public void ItemGet_ExpiredItemMustBeRemoved()
        {
            var options = new LRUCacheOptions();
            var store = new LRUCacheStore(options);

            var entry = CacheEntry.New("key", "value", TimeSpan.FromMilliseconds(500));

            store.AddEntry(entry);

            Thread.Sleep(1000);

            Assert.Null(store.GetEntry("key"));
        }

        [Fact]
        public void ItemGet_ExpiryMustBeUpdated()
        {
            var options = new LRUCacheOptions();
            var store = new LRUCacheStore(options);

            var entry = CacheEntry.New("key", "value", TimeSpan.FromMilliseconds(1500));

            store.AddEntry(entry);

            Thread.Sleep(1000);

            // entry must be updated to last another 1500 milliseconds
            Assert.NotNull(store.GetEntry("key"));

            Thread.Sleep(1000);

            // updated entry must still exist
            Assert.NotNull(store.GetEntry("key"));

        }

    }
}
