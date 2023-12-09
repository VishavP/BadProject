namespace BadProject.Tests.UnitTests
{
    [TestFixture]
    public class when_retrieving_adverts
    {
        private AdvertisementService _advertisementService;
        private NoSqlAdvProvider _noSqlAdvProvider;
        private CachingService _cachingService;
        private ErrorProvider _errorProvider;

        [SetUp]
        public void OnSetup()
        {
            _noSqlAdvProvider = new NoSqlAdvProvider();
            _cachingService = new CachingService(new MemoryCache("test"));
            _errorProvider = new ErrorProvider();
            _advertisementService = new AdvertisementService(_noSqlAdvProvider, _cachingService, _errorProvider);
        }

        [Test]
        public void when_adding_error_it_must_not_throw_an_exception()
        {
            Advertisement advertisement = new Advertisement();
            Assert.DoesNotThrow(() => advertisement = _advertisementService.GetAdvertisement(Guid.NewGuid().ToString()));
            Assert.IsNotNull(advertisement);
        }

        [TearDown]
        public void OnTearDown()
        {
            _advertisementService = null;
        }
    }
}
