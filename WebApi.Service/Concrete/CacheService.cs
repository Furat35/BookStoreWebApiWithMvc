using Microsoft.Extensions.Caching.Memory;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void TryAdd(string key, T value)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(60),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });
        }

        public T TryGet(string key)
        {
            _cache.TryGetValue(key, out T entities);
            return entities;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
