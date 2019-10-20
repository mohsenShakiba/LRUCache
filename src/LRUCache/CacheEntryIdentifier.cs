using System;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{
    [Serializable]
    public struct CacheEntryIdentifier : IEquatable<CacheEntryIdentifier>
    {
        public int Id { get; }
        public string Key { get; }

        public CacheEntryIdentifier(string key, LRUCacheOptions options)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Key = key;
            Id = options.HashCodeGenerator(key);
        }

        public CacheEntryIdentifier(string key)
        {
            Key = key;
            Id = (new LRUCacheOptions()).HashCodeGenerator(key);
        }

        public bool Equals(CacheEntryIdentifier other)
        {
            return other.Id == Id;
        }
    }
}
