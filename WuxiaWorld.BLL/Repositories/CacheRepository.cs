namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Interfaces;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;

    public class CacheRepository : ICacheRepository {

        private readonly IMemoryCache _cache;

        public CacheRepository(IMemoryCache cache) {

            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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

        public async Task RemoveAsync(string keyValue) {

            await Task.Run(() => {
                _cache.Remove(keyValue);
            });
        }

        public async Task UpsertAsync(string apiEndpointKeyValue, int newId, object newRecord,
            CancellationToken ctToken) {

            await CreateAsync($"{apiEndpointKeyValue}/{newId}", newRecord, new CancellationChangeToken(ctToken))
                .ConfigureAwait(false);

            var objectList = new List<object>();

            if (GetCache(apiEndpointKeyValue) is List<object> cacheResult) {

                objectList.AddRange(cacheResult);
            }

            objectList.Add(newRecord);

            await RemoveAsync(apiEndpointKeyValue);

            await CreateAsync(apiEndpointKeyValue, objectList, new CancellationChangeToken(ctToken))
                .ConfigureAwait(false);
        }
    }

}