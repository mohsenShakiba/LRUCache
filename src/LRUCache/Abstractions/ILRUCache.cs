using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LRUCache.Abstractions
{
    public interface ILRUCache: IEnumerable, IDisposable
    {
        void Add(string key, object value, TimeSpan? validUntil);
        void Remove(string key);
        void Clear();
        bool TryGetValue<T>(string key, out T value);
        int Count();
        T GetValue<T>(string key, T defaultValue);
    }
}
