namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Threading.Tasks;

    using Interfaces;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;

    public class CacheRepository : ICacheRepository {

        private readonly IMemoryCache _cache;

        public CacheRepository(IMemoryCache cache) {

            _cache = cache;
        }

        public object GetCache(string keyValue) {

            return _cache.Get(keyValue);
        }

        public async Task<object> CreateAsync(string keyValue, object data, IChangeToken cancellationToken) {

            if (data != null) {

                return await _cache.GetOrCreateAsync(keyValue,
                    cache => {
                        cache.SlidingExpiration = TimeSpan.FromMinutes(120);

                        return Task.FromResult(data);
                    });
            }

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string keyValue) {

            _cache.Remove(keyValue);

            _cache.Remove($"{keyValue}/");

            return Task.CompletedTask;
        }
    }

}