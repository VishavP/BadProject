using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ThirdParty;

namespace BadProject.Services.Interfaces
{
    public interface ICachingService<T>
    {
        Advertisement GetAdvertisementFromCache(string WebId);
        T GetCachingMechanism();
    }
}
