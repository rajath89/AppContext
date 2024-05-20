using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public sealed class SimpleCache<T>
    {
        private readonly Dictionary<string, CacheItem<T>> _cache;
        private readonly object _lock = new object();

        // Singleton instance
        private static readonly Lazy<SimpleCache<T>> _instance = new Lazy<SimpleCache<T>>(() => new SimpleCache<T>());

        // Private constructor to prevent external instantiation
        private SimpleCache()
        {
            _cache = new Dictionary<string, CacheItem<T>>();
        }

        // Public property to get the singleton instance
        public static SimpleCache<T> Instance => _instance.Value;

        public void Set(string key, T value, TimeSpan duration)
        {

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "CacheKey cannot be null.");
            }

            var expirationTime = DateTime.UtcNow.Add(duration);
            var cacheItem = new CacheItem<T>(value, expirationTime);

            lock (_lock)
            {
                _cache[key] = cacheItem;
            }
        }

        public T Get(string key)
        {

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "CacheKey cannot be null.");
            }

            lock (_lock)
            {
                if (_cache.TryGetValue(key, out CacheItem<T> cacheItem))
                {
                    if (cacheItem.ExpirationTime > DateTime.UtcNow)
                    {
                        return cacheItem.Value;
                    }
                    else
                    {
                        // Item expired, remove it from cache
                        _cache.Remove(key);
                    }
                }
            }
            return default(T); // Cache miss or expired item
        }

        private class CacheItem<TValue>
        {
            public TValue Value { get; }
            public DateTime ExpirationTime { get; }

            public CacheItem(TValue value, DateTime expirationTime)
            {
                Value = value;
                ExpirationTime = expirationTime;
            }
        }

        public void ClearByType<TClearType>() where TClearType : T
        {
            lock (_lock)
            {
                List<string> keysToRemove = new List<string>();
                foreach (var keyValuePair in _cache)
                {
                    if (keyValuePair.Value.Value is TClearType)
                    {
                        keysToRemove.Add(keyValuePair.Key);
                    }
                }

                foreach (string key in keysToRemove)
                {
                    _cache.Remove(key);
                }
            }
        }

        public void ClearAll()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }
    }
}
