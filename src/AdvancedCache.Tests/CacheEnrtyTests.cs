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
            var cacheEntryOne = CacheEntry.New("key1", "1", TimeSpan.FromSeconds(1));
            var cacheEntryTwo = CacheEntry.New("key2", "2", TimeSpan.FromSeconds(1));

            Assert.NotEqual(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.NotEqual(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void SameKeys_EqualitySuccess()
        {
            var cacheEntryOne = CacheEntry.New("key1", "1", TimeSpan.FromSeconds(1));
            var cacheEntryTwo = CacheEntry.New("key1", "2", TimeSpan.FromSeconds(1));

            Assert.Equal(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.Equal(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void SameKeyWithDifferentCasing_EqulitySuccess()
        {
            var cacheEntryOne = CacheEntry.New("key1", "1", TimeSpan.FromSeconds(1));
            var cacheEntryTwo = CacheEntry.New("KEY1", "1", TimeSpan.FromSeconds(1));

            Assert.Equal(cacheEntryOne.GetHashCode(), cacheEntryTwo.GetHashCode());
            Assert.Equal(cacheEntryOne, cacheEntryTwo);
        }

        [Fact]
        public void AddTimeSpan_MustNotExpire()
        {
            var cacheEntry = CacheEntry.New("KEY", "VALUE", TimeSpan.FromSeconds(1));
            Assert.False(cacheEntry.HasExpired);
        }

        [Fact]
        public void AddTimeSpanAndWait_MustExpire()
        {
            var cacheEntry = CacheEntry.New("KEY", "VALUE", TimeSpan.FromSeconds(1));

            Thread.Sleep(1500);

            Assert.True(cacheEntry.HasExpired);
        }

        [Fact]
        public void AddTimeSpanAndUpdate_MustNotExpire()
        {
            var cacheEntry = CacheEntry.New("KEY", "VALUE", TimeSpan.FromSeconds(1));

            Thread.Sleep(1500);

            cacheEntry.UpdateValidUntil();

            Assert.False(cacheEntry.HasExpired);
        }

    }
}
