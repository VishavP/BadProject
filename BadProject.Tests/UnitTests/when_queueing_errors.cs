namespace BadProject.Tests.UnitTests
{
    [TestFixture]
    public class Given_we_need_to_manipulate_errors
    {
        private ErrorProvider _errorProvider;

        [SetUp]
        public void OnSetup()
        {
            _errorProvider = new ErrorProvider();
        }

        [Test]
        public void when_adding_error_it_must_not_throw_an_exception()
        {
            Assert.DoesNotThrow(() => _errorProvider.AddError(new Error("error", DateTimeOffset.Now)));
        }

        [Test]
        public void when_retrieving_errors_by_date()
        {
            Error error = new Error("error", DateTimeOffset.Now);
            _errorProvider.AddError(error);
            var results = _errorProvider.GetErrorsByMinDate(DateTime.Now.AddHours(-1));
            Assert.True(results.Contains(error));
        }

        [Test]
        public void when_retrieving_errors()
        {
            Queue<Error> results = null;
            Assert.DoesNotThrow(() => results = _errorProvider.GetErrors());
        }

        [Test]
        public void when_clearing_errors()
        {
            Assert.DoesNotThrow(() => _errorProvider.ClearErrors());
        }

        [TearDown]
        public void OnTearDown()
        {
            _errorProvider.ClearErrors();
        }
    }
}
