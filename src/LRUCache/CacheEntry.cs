using LRUCache.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{
    [Serializable]
    public class CacheEntry : IEquatable<CacheEntry>, IEqualityComparer<CacheEntry>, IIdentifiedModel
    {
        public CacheEntryIdentifier Identifier { get; }
        public object Value { get; }
        public DateTime ValidUntil { get; private set; }
        public TimeSpan ExpirationPeriod { get; }

        public CacheEntry(CacheEntryIdentifier identifier, object value, TimeSpan expirationPeriod)
        {
            Identifier = identifier;
            Value = value;
            this.ExpirationPeriod = expirationPeriod;
            ValidUntil = DateTime.Now.Add(expirationPeriod);
        }

        public static CacheEntry New(string key, object value, TimeSpan timeSpan, LRUCacheOptions options = null)
        {
            var indentifier = new CacheEntryIdentifier(key, options ?? new LRUCacheOptions());
            return new CacheEntry(indentifier, value, timeSpan);
        }

        public void UpdateValidUntil()
        {
            ValidUntil = DateTime.Now.Add(ExpirationPeriod);
        }

        public bool Equals(CacheEntry x, CacheEntry y)
        {
            if (x == null || y == null)
                return false;
            return x.GetHashCode() == y.GetHashCode();
        }

        public bool Equals(CacheEntry other)
        {
            if (other == null)
                return false;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public int GetHashCode(CacheEntry obj)
        {
            return Identifier.Id;
        }

        public bool HasExpired() => DateTime.Now > ValidUntil;

    }


}
