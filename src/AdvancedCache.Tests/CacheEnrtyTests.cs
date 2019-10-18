using System;
using System.Threading;
using Xunit;

namespace AdvancedCache.Tests
{
    public class CacheEnrtyTests
    {
        [Fact]
        public void DifferentKeys_EqualityFail()
        {
            var cacheEntryOne = new CacheEntry("key1");
            var cacheEntryTwo = new CacheEntry("key2");

            Assert.NotEqual(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.NotEqual(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void SameKeys_EqualitySuccess()
        {
            var cacheEntryOne = new CacheEntry("key1");
            var cacheEntryTwo = new CacheEntry("key1");

            Assert.Equal(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.Equal(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void SameKeyWithDifferentCasing_EqulitySuccess()
        {
            var cacheEntryOne = new CacheEntry("key1");
            var cacheEntryTwo = new CacheEntry("KEY1");

            Assert.Equal(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.Equal(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void AddTimeSpan_MustNotExpire()
        {
            var cacheEntry = new CacheEntry("KEY", "VALUE", TimeSpan.FromSeconds(1));
            Assert.False(cacheEntry.HasExpired);
        }

        [Fact]
        public void AddTimeSpanAndWait_MustExpire()
        {
            var cacheEntry = new CacheEntry("KEY", "VALUE", TimeSpan.FromSeconds(1));

            Thread.Sleep(1500);

            Assert.True(cacheEntry.HasExpired);
        }

        [Fact]
        public void AddTimeSpanAndUpdate_MustNotExpire()
        {
            var cacheEntry = new CacheEntry("KEY", "VALUE", TimeSpan.FromSeconds(1));

            Thread.Sleep(1500);

            cacheEntry.UpdateValidUntil();

            Assert.False(cacheEntry.HasExpired);
        }

    }
}
