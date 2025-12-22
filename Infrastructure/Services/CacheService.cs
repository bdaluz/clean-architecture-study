using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public CacheService(IDistributedCache cache) { 
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10),
            };
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedData)) { 
                return default;
            }

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        public async Task RemoveAsync(string key) {
            await _cache.RemoveAsync(key);
        }
        
    }
}
