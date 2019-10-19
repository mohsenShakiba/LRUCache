using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    public interface IIdentifiedModel
    {
        CacheEntryIdentifier Identifier { get; }
    }
}
