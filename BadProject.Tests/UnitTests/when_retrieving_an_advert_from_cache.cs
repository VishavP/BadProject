using BadProject.Implementation;
using BadProject.Interfaces;
using Moq;
using System.Runtime.Caching;

namespace BadProject.Tests.UnitTests
{
    public class when_retrieving_an_advert_from_cache
    {
        private Mock<ICachingService> _cachingService;
        private MemoryCache _cacheMechanism;
        
        [SetUp]
        public void Setup()
        {
            _cachingService = new Mock<ICachingService>();
            _cacheMechanism = _cachingService.Object.GetCachingMechanism();
        }

        [Test]
        public void it_should_not_throw_errors()
        {
            Assert.DoesNotThrow(()=>_cachingService.Object.GetCachingMechanism());
        }

        [Test]
        public void it_should_return_the_object()
        {
            _cachingService.Object.SetCacheValue("key", new object{ }, DateTimeOffset.Now);
            object o = _cachingService.Object.GetCachingMechanism().Get("key");
            Assert.IsNotNull(o);
        }
    }
}