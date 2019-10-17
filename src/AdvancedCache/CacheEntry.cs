using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache
{
    public class CacheEntry : IEquatable<CacheEntry>, IEqualityComparer<CacheEntry>
    {

        public string Key { get; }
        public object Value { get; }
        public DateTime ValidUntil { get; private set; }
        public bool HasExpired => DateTime.Now > ValidUntil;

        private readonly TimeSpan expirationPeriod;

        public CacheEntry(string key, object value, TimeSpan expirationPeriod)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            Key = key;
            Value = value;
            this.expirationPeriod = expirationPeriod;
            ValidUntil = DateTime.Now.Add(expirationPeriod);
        }

        public CacheEntry(string key): this(key, null, TimeSpan.Zero)
        {
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

        public int GetHashCode(CacheEntry obj)
        {
            return Key
                .ToLower()
                .GetHashCode();
        }
    }
}
