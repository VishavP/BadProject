using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ThirdParty;

namespace BadProject.Interfaces
{
    public interface ICachingService
    {
        Advertisement GetAdvertisementFromCache(string WebId);
        MemoryCache GetCachingMechanism();
        void SetCacheValue(string key, object value, DateTimeOffset timeStamp);


    }
}
