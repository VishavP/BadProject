using BadProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ThirdParty;

namespace BadProject.Implementation
{
    public class CachingService : ICachingService
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
    }
}
