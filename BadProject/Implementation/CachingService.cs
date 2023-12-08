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
            return (Advertisement)GetCachingMechanism().Get($"AdvKey_{id}");
        }

        public MemoryCache GetCachingMechanism() 
        {
            return this._memoryCache;
        }

        public void SetCacheValue(string key, object value, DateTimeOffset timeStamp)
        {
            this.GetCachingMechanism().Set($"AdvKey_{key}", value, timeStamp);
        }
    }
}
