﻿using BadProject.DataModels;
using BadProject.Interfaces;
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
    public class AdvertisementService 
    {
        private static readonly object lockObj = new object();
        private NoSqlAdvProvider SqlProvider;
        private IErrorProvider _ErrorProvider;
        private ICachingService<MemoryCache> _cachingService;
        private int _maxRetryCount;

        public AdvertisementService(NoSqlAdvProvider noSqlAdvProvider, ICachingService<MemoryCache> cachingService, IErrorProvider errorProvider)
        {
            this.SqlProvider = noSqlAdvProvider;
            this._cachingService = cachingService;
            this._ErrorProvider = errorProvider;
            this._maxRetryCount = Convert.ToInt32(ConfigurationManager.AppSettings["RetryCount"]);
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
            if (id != null)
            {
                Advertisement advertisement = null;
                Monitor.Enter(lockObj);
                advertisement = _cachingService.GetAdvertisementFromCache(id);
                IEnumerable<Error> errors = _ErrorProvider.GetErrorsByMinDate(DateTime.Now.AddHours(-1));

                if ((advertisement == null) || (errors.Count() > 10)) 
                {
                    int retry = 0;
                    do
                    {
                        try
                        {
                            retry++;
                            advertisement = SqlProvider.GetAdv(id);
                            if (advertisement != null)
                            {
                                _cachingService.GetCachingMechanism().Set($"AdvKey_{id}", advertisement, DateTimeOffset.Now.AddMinutes(5));
                            }
                            else
                            {
                                advertisement = SQLAdvProvider.GetAdv(id);
                                if (advertisement != null)
                                {
                                    _cachingService.SetCacheValue($"AdvKey_{id}", advertisement, DateTimeOffset.Now.AddMinutes(5));
                                }
                            }
                        }
                        catch (Exception error)
                        {
                            Thread.Sleep(1000);
                            _ErrorProvider.AddError(new Error(error.Message, DateTime.Now)); //or rethrow it
                            Monitor.Exit(lockObj);
                        }
                    } while ((advertisement == null));
                }
                Monitor.Exit(lockObj);
                return advertisement;
            }
            else
            {
                Error error = new Error("Invalid id", DateTimeOffset.Now);
                _ErrorProvider.AddError(error);
                throw new Exception(error.Message);
            }
        }
    }
}
