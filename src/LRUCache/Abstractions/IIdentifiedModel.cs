using System;
using System.Collections.Generic;
using System.Text;

namespace LRUCache.Abstractions
{
    public interface IIdentifiedModel
    {
        CacheEntryIdentifier Identifier { get; }
    }
}
