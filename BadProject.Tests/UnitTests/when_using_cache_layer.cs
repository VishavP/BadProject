namespace BadProject.Tests.UnitTests
{
    [TestFixture]
    public class when_using_cache_layer
    {
        private CachingService _cachingService;
        private MemoryCache _cacheMechanism;
        private string id;
        
        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid().ToString();
            _cacheMechanism = new MemoryCache("test");
            _cachingService = new CachingService(_cacheMechanism);
        }

        [Test]
        public void it_should_not_throw_errors()
        {
            Assert.DoesNotThrow(()=>_cachingService.GetAdvertisementFromCache(id));
        }

        [Test]
        public void it_should_set_the_object()
        {
           Assert.DoesNotThrow(()=> _cachingService.SetCacheValue(id, new object{ }, DateTimeOffset.Now));
        }

        [Test]
        public void it_should_get_the_object()
        {
            _cachingService.SetCacheValue(id, new object { }, DateTimeOffset.Now);
            Assert.DoesNotThrow(()=> _cachingService.GetAdvertisementFromCache(id));
        }
    }
}