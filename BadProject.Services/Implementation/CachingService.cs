using BadProject.Services.Interfaces;
using System;
using System.Runtime.Caching;
using ThirdParty;

namespace BadProject.Services.Implementation
{
    public class CachingService : ICachingService<MemoryCache>
    {
        private MemoryCache _memoryCache;
        public CachingService(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public Advertisement GetAdvertisementFromCache(string id)
        {
            return (Advertisement)_memoryCache.Get($"AdvKey_{id}");
        }

        public MemoryCache GetCachingMechanism() {
            return _memoryCache;
        }

        public void SetCacheValue(object CacheValue, Advertisement advertisement, DateTimeOffset dateTimeOffset)
        {
            GetCachingMechanism().Set($"AdvKey_{CacheValue}", advertisement, DateTimeOffset.Now.AddMinutes(5));
        }
    }
}
