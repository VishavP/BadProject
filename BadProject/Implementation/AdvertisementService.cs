using BadProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using ThirdParty;

namespace BadProject.Implementation
{
    public class AdvertisementService : IAdvertisementService
    {
        private MemoryCache cache = new MemoryCache(""); //inject this
        private Queue<DateTime> errors = new Queue<DateTime>(); //inject this
        private static object lockObj; 
        private NoSqlAdvProvider _NoSqlAdvProvider; 

        public AdvertisementService(NoSqlAdvProvider noSqlAdvProvider)
        {
            this._NoSqlAdvProvider = noSqlAdvProvider;
        }
        // **************************************************************************************************
        // Loads Advertisement information by id
        // from cache or if not possible uses the "mainProvider" or if not possible uses the "backupProvider"
        // **************************************************************************************************
        // Detailed Logic:
        // 
        // 1. Tries to use cache (and retuns the data or goes to STEP2)
        //
        // 2. If the cache is empty it uses the NoSqlDataProvider (mainProvider), 
        //    in case of an error it retries it as many times as needed based on AppSettings
        //    (returns the data if possible or goes to STEP3)
        //
        // 3. If it can't retrive the data or the ErrorCount in the last hour is more than 10, 
        //    it uses the SqlDataProvider (backupProvider)
        public Advertisement GetAdvertisement(string id)
        {
            Advertisement advertisement = null;

            lock (lockObj)
            {
                // Use Cache if available
                advertisement = (Advertisement)cache.Get($"AdvKey_{id}");

                // Count HTTP error timestamps in the last hour
                while (errors.Count > 20)
                {
                    errors.Dequeue();
                }
                int errorCount = errors.Where(err=>err > DateTime.Now.AddHours(-1)).Count();
                // If Cache is empty and ErrorCount<10 then use HTTP provider
                if ((advertisement == null) && (errorCount < 10))
                {
                    int retry = 0;
                    do
                    {
                        retry++;
                        try
                        {
                            advertisement = _NoSqlAdvProvider.GetAdv(id);
                        }
                        catch
                        {
                            Thread.Sleep(1000);
                            errors.Enqueue(DateTime.Now); // Store HTTP error timestamp              
                        }
                    } while ((advertisement == null) && (retry < int.Parse(ConfigurationManager.AppSettings["RetryCount"])));


                    if (advertisement != null)
                    {
                        cache.Set($"AdvKey_{id}", advertisement, DateTimeOffset.Now.AddMinutes(5));
                    }
                }


                // if needed try to use Backup provider
                if (advertisement == null)
                {
                    advertisement = SQLAdvProvider.GetAdv(id);

                    if (advertisement != null)
                    {
                        cache.Set($"AdvKey_{id}", advertisement, DateTimeOffset.Now.AddMinutes(5));
                    }
                }
            }
            return advertisement;
        }
    }
}
