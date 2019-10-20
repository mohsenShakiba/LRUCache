
using LRUCache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LRUCacheSample
{
    class Program
    {

        private static LRUCache.LRUCache cache;

        static void Main(string[] args)
        {
            var options = new LRUCacheOptions(1000);
            //options.DataPersist = new FileBasedCacheStore("test.bin");
            cache = new LRUCache.LRUCache(options);

            var list = new List<Thread>();

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 10; i++)
            {
                var t = new Thread(BenchMarkTest);
                t.Start();

                list.Add(t);
            }

            foreach(var t in list)
            {
                t.Join();
            }

            sw.Stop();

            Console.WriteLine("ended in {0}", sw.ElapsedMilliseconds);

            Console.ReadLine();
        }

        public static void BenchMarkTest()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100_000; i++)
            {
                var next = rnd.Next(0, 1000);
                //var next = rnd.Next(0, 1_000_000);
                cache.Add(next.ToString(), next);
            }
        }
    }
}
