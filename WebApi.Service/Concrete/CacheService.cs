using Microsoft.Extensions.Caching.Memory;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public sealed class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add(string key, object value)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(60),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });
        }

        public object Get(string key)
        {

            if (_cache.TryGetValue(key, out object result))
                return result;

            return null;
        }

        public void Remove(string key)
        {
            if (Exists(key))
                _cache.Remove(key);
        }

        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out object value);
        }
    }
}
