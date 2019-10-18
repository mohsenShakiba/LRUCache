
using AdvancedCache;
using System;

namespace AdvancedCacheTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new AdvancedCacheOptions(3);
            var ac = new AdvancedCache.AdvancedCache(options);
            ac.AddEntry("1", 1);
            ac.AddEntry("2", 2);
            ac.AddEntry("3", 3);
            ac.AddEntry("4", 4);
            ac.AddEntry("5", 5);
            ac.AddEntry("6", 6);

            int res1 = ac.GetValue<int>("1");
            int res2 = ac.GetValue<int>("2");
            int res6 = ac.GetValue<int>("6");

        }
    }
}
