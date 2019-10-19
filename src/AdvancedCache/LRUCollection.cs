using AdvancedCache.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache
{
    /// <summary>
    /// a collection that performs add/remove/find operations in O(1)
    /// while also retains the order of items based on their usage
    /// </summary>
    public class LRUCollection<T> : IEnumerable<T> where T: IIdentifiedModel
    {
        private readonly int maxCount;
        private readonly LinkedList<T> cacheEntriesList;
        private readonly IDictionary<CacheEntryIdentifier, LinkedListNode<T>> cacheEntriesMap;

        public LRUCollection(int maxCount)
        {
            cacheEntriesList = new LinkedList<T>();
            cacheEntriesMap = new Dictionary<CacheEntryIdentifier, LinkedListNode<T>>();
            this.maxCount = maxCount;
        }

        public void Add(T item)
        {
            // if key exists, just remove it from list
            // because we want to add the fresh item to the start of the list
            if (cacheEntriesMap.TryGetValue(item.Identifier, out var existingValue))
            {
                cacheEntriesList.Remove(existingValue);
            }
            // if max size has been reached, remove the least used item
            else if (cacheEntriesMap.Count == maxCount)
            {
                RemoveLeastUsedItem();
            }
            // else add it to the dict and linked list
            var node = cacheEntriesList.AddFirst(item);
            cacheEntriesMap[item.Identifier] = node;
        }

        public T Get(CacheEntryIdentifier key)
        {
            // if key exists, just move it to the start the line
            if (cacheEntriesMap.TryGetValue(key, out var value))
            {
                MoveNodeToFront(value);
                return value.Value;
            }
            return default(T);
        }

        public int Count()
        {
            return cacheEntriesMap.Count;
        }

        public void Remove(CacheEntryIdentifier key)
        {
            if (cacheEntriesMap.TryGetValue(key, out var value))
            {
                cacheEntriesList.Remove(value);
                cacheEntriesMap.Remove(key);
            }
        }

        public void Clear()
        {
            cacheEntriesList.Clear();
            cacheEntriesMap.Clear();
        }

        /// <summary>
        /// this method will bring the node to the start of the linked list
        /// this will make the most used cache entries more accessible and faster to search
        /// </summary>
        /// <param name="node"></param>
        private void MoveNodeToFront(LinkedListNode<T> node)
        {
            if (node == null)
                return;
            cacheEntriesList.Remove(node);
            cacheEntriesList.AddFirst(node);
        }

        private void RemoveLeastUsedItem()
        {
            // get last item in linked list
            var lastItem = cacheEntriesList.Last;
            // remove it from link list
            cacheEntriesList.Remove(lastItem);
            // remove it from dictionary
            cacheEntriesMap.Remove(lastItem.Value.Identifier);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return cacheEntriesList.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
