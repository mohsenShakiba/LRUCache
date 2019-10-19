using AdvancedCache.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AdvancedCache
{
    public class FileBasedCacheStore : IDataPersist
    {

        private readonly string path;

        public FileBasedCacheStore(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            this.path = path;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CacheEntry> RestoreItems()
        {
            var cacheEntries = new List<CacheEntry>();
            if (!File.Exists(path))
            {
                return cacheEntries;
            }
            using (var fileHandler = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var result = formatter.Deserialize(fileHandler);
                Console.WriteLine("response from serialization is {0}", JsonConvert.SerializeObject(result));
                return result as IEnumerable<CacheEntry>;
            }
        }

        public void StoreItems(IEnumerable<CacheEntry> cacheEntries)
        {
            using (var fileHandler = File.Open(path, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fileHandler, cacheEntries);
            }
        }
    }
}
