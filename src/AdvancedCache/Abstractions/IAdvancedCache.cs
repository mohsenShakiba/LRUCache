using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    public interface IAdvancedCache: IEnumerable<object>, IDisposable
    {
        void AddEntry(string key, object value, TimeSpan validUntil);
        bool RemoveEntry(string key);
        void ClearEntries();
        bool TryGetValue<T>(string key, out T value);
        T GetValue<T>(string key, T defaultValue);
    }
}
