using AdvancedCache.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache
{
    public class CacheEntry : IEquatable<CacheEntry>, IEqualityComparer<CacheEntry>, IIdentifiedModel
    {
        public CacheEntryIdentifier Identifier { get; }
        public object Value { get; }
        public DateTime ValidUntil { get; private set; }
        public bool HasExpired => DateTime.Now > ValidUntil;

        private readonly TimeSpan expirationPeriod;

        public CacheEntry(CacheEntryIdentifier identifier, object value, TimeSpan expirationPeriod)
        {
            Identifier = identifier;
            Value = value;
            this.expirationPeriod = expirationPeriod;
            ValidUntil = DateTime.Now.Add(expirationPeriod);
        }

        public static CacheEntry New(string key, object value, TimeSpan timeSpan, AdvancedCacheOptions options = null)
        {
            var indentifier = new CacheEntryIdentifier(key, options ?? new AdvancedCacheOptions());
            return new CacheEntry(indentifier, value, timeSpan);
        }

        public void UpdateValidUntil()
        {
            ValidUntil = DateTime.Now.Add(expirationPeriod);
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
    }

    
}
