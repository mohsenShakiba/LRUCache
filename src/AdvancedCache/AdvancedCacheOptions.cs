using AdvancedCache.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCache
{
    public class AdvancedCacheOptions
    {

        /// <summary>
        /// size after which the cache entries will be dropped in a LIFO manner
        /// default is int.MaxValue
        /// </summary>
        public int MaxSize { get; }

        /// <summary>
        /// a hash code generator that must return an integer for a string key
        /// default is the string.GetHashCode 
        /// </summary>
        public Func<string, int> HashCodeGenerator { get; }

        public IDataPersist DataPersist { get; }

        public AdvancedCacheOptions()
        {
            MaxSize = int.MaxValue;
            HashCodeGenerator = (key) =>  key.ToLower().GetHashCode();
            DataPersist = null;
        }

        public AdvancedCacheOptions(int maxSize, Func<string, int> hashCodeGenerator, IDataPersist dataPersist)
        {
            MaxSize = maxSize;
            HashCodeGenerator = hashCodeGenerator;
            DataPersist = dataPersist;
        }
    }
}
