Sponsored by [PersianSaze](https://persiansaze.com/).

# LRU Cache 
A cache service written in .NET Core implemented using `Least Recently Used` policy.

## Usage
an example of using the service:
```
var cache = new LRUCache();
cache.Add("Key", "Value", TimeSpan.FromSeconds(10));
var value = cache.GetValue("Key"); // returns "Value"
var count = cache.Count(); // returns 1
cache.Remove("Key");
```

## APIs
the LRUCache provides the following APIs:
- Add(string key, object value, optional TimeSpan expiry)

adds an entry with the given key and value and an optional expiry period after which the entry will be removed.
if the expiry is ommited, then `TimeSpan.FromDays(1000)` will be used instead.
if the key already exists, the value will be replaced and the operatio is considered a `hit` which means the entry will
 be moved to the start of the line.
- Remove(string key)

removes the entry if it exists and ignores if the key cannot be found.
- GetValue<T>(string key, T value defaultValue)

returns the value for the given key, if the key doesn't exists then the default value for `T` will be returned.
the default value can optionally be provided.
- TryGetValue<T>(string key, out T value) => bool

sets the value in the `out` paramter returning true if the key is found and false in case the key cannot be found.
- Clear()

clears entries in the store.
- Count() => int


returns the number of entries in the store.

## Options
a `LRUCacheOptions` is provided to customize the behaviour of the `LRUCache`.
```
var options = new LRUCacheOptions();
options.MaxSize = 1000; // default is int.MaxValue
options.HashCodeGenerator = (key) => key.ToLower().GetHashCode();
options.DataPersist = new FileBasedCacheStore("path_to_file"); 
```
- MaxSize: the maximum size of store after wich the entries will be replaced based on LRU policy.
- HashCodeGenerator: a function that accepts a string as the key of string and must return an integer representing the hash code.
the default value uses the hash code of the lower-cased key.
- DataPersist: an implementation of IDataPersist which if provided will be used to store and restore cache entries on system restart.

## Thread Safety
by default the LRUCache is thread-safe and the implementation uses ReaderWriterLockSlim which means multiple threads
can access the cache while only one thread can perform `Add`, `Remove` or `Clear` API.

## LRU Policy
LRU policy implies that the items added to the cache will be deleted based on the least-recently-used manner. this means that
 an items that is used least will be replaces with newer items while items that are used frequently will be kept in the cache.
this implementation however will also bring into account the `TimeSpan` passed in the `Add` method.
