using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache.Abstractions
{
    interface IIdentifiedModel
    {
        string Key { get; }
    }
}
