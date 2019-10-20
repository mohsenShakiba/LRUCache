using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LRUCache.Tests
{
    public class LRUCollectionTests
    {

        [Fact]
        public void AddMoreItems_MaxCountReach()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            // add 6 items to collection
            for (int i = 0; i < 6; i++)
            {
                lruCollection.Add(CacheEntry.New(i.ToString(), i.ToString(), TimeSpan.FromSeconds(100)));
            }

            // check count of collection
            Assert.True(lruCollection.Count() == 5);

            // check item one must have been remove
            Assert.Null(lruCollection.Get(new CacheEntryIdentifier("0")));
            // the fifth item must be present
            Assert.NotNull(lruCollection.Get(new CacheEntryIdentifier("5")));
        }

        /// <summary>
        /// in this test we check if we frequnetly access an object, it won't be replaced by other objects
        /// </summary>
        [Fact]
        public void GetItem_MustMoveToStart()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            // add 5 items to collection
            for (int i = 0; i < 5; i++)
            {
                lruCollection.Add(CacheEntry.New(i.ToString(), i.ToString(), TimeSpan.FromSeconds(100)));
            }

            // accessing 0,2,4
            lruCollection.Get(new CacheEntryIdentifier("0"));
            lruCollection.Get(new CacheEntryIdentifier("2"));
            lruCollection.Get(new CacheEntryIdentifier("4"));

            // adding 2 new items
            lruCollection.Add(CacheEntry.New("5", "5", TimeSpan.FromSeconds(100)));
            lruCollection.Add(CacheEntry.New("6", "7", TimeSpan.FromSeconds(100)));

            // now the items 1,3 must be replaced
            Assert.Null(lruCollection.Get(new CacheEntryIdentifier("1")));
            Assert.Null(lruCollection.Get(new CacheEntryIdentifier("3")));
        }

        [Fact]
        public void IfAddedItemWithSameKey_NewItemMustBeReplaced()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            var entryOne = CacheEntry.New("1", "1.1", TimeSpan.FromSeconds(100));
            var entryTwo = CacheEntry.New("1", "1.2", TimeSpan.FromSeconds(100));

            lruCollection.Add(entryOne);
            lruCollection.Add(entryTwo);

            Assert.Equal(1, lruCollection.Count());
            Assert.Equal(lruCollection.Get(new CacheEntryIdentifier("1")).Value, entryTwo.Value);
        }

        [Fact]
        public void AddedItemsWithSameKeyButDifferentCasing_NewItemMustBeReplaced()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            var entryOne = CacheEntry.New("key", "1.1", TimeSpan.FromSeconds(100));
            var entryTwo = CacheEntry.New("KEY", "1.2", TimeSpan.FromSeconds(100));

            lruCollection.Add(entryOne);
            lruCollection.Add(entryTwo);

            Assert.Equal(1, lruCollection.Count());
            Assert.Equal(lruCollection.Get(new CacheEntryIdentifier("key")).Value, entryTwo.Value);
        }

        [Fact]
        public void ClearingCollection_AllItemsMustBeRemoved()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            var entry = CacheEntry.New("1", "1", TimeSpan.FromSeconds(100));

            lruCollection.Add(entry);

            lruCollection.Clear();

            Assert.True(lruCollection.Count() == 0);
            Assert.Null(lruCollection.Get(new CacheEntryIdentifier("1")));
        }

        [Fact]
        public void RemovingItem_ItemNoLongerExists()
        {
            var lruCollection = new LRUCollection<CacheEntry>(5);

            var entry = CacheEntry.New("1", "1", TimeSpan.FromSeconds(100));

            lruCollection.Add(entry);

            lruCollection.Remove(new CacheEntryIdentifier("1"));

            Assert.True(lruCollection.Count() == 0);
            Assert.Null(lruCollection.Get(new CacheEntryIdentifier("1")));
        }

    }
}
