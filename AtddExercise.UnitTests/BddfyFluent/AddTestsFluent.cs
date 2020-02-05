using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit;

namespace AtddExercise.UnitTests.BddfyFluent
{
    public class AddTestsFluent : IClassFixture<MockingWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly MockingWebApplicationFactory<Startup> _factory;
        private string _numbers;
        private HttpResponseMessage _response;

        public AddTestsFluent(MockingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        
        [BddfyFact]
        public void TwoValidNumbersReturnsSuccessWithCorrectResult()
        {
            this.Given(_ => _.TwoValidNumbers())
                .When(_ => _.AddingNumbers())
                .Then(_ => TheResponseIndicatesSuccess())
                .And(_ => _.TheExpectedResultIsReturned())
                .BDDfy();
        }
        
        [BddfyFact]
        public void TwoValidNumbersAreSavedWithTheResult()
        {
            this.Given(_ => _.TwoValidNumbers())
                .When(_ => _.AddingNumbers())
                .Then(_ => TheNumbersAndResultAreSaved())
                .BDDfy();
        }

        private void TheNumbersAndResultAreSaved()
        {
            _factory.DataAccessMock.Verify(m => 
                m.SaveInputsAndResult(
                    It.Is<string>(v => v == "3,2"),
                    It.Is<string>(v => v == "5")
                ), Times.Once);
        }

        [BddfyFact]
        public void NoNumbersReturnsFailure()
        {
            this.Given(_ => _.NoNumbers())
                .When(_ => _.AddingNumbers())
                .Then(_ => TheResponseIndicatesFailure())
                .BDDfy();
        }

        private void TwoValidNumbers()
        {
            _numbers = "3,2";
        }

        private void NoNumbers()
        {
            _numbers = "";
        }

        private async Task AddingNumbers()
        {
            _response = await _client.GetAsync($"/calculator/add/{_numbers}");
        }

        private void TheResponseIndicatesSuccess()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private void TheResponseIndicatesFailure()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        private async Task TheExpectedResultIsReturned()
        {
            var body = await _response.Content.ReadAsStringAsync();
            body.ShouldBe("5");
        }
    }

    public class MockingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public Mock<IDataAccess> DataAccessMock { get; private set; }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var searchDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDataAccess));
                if (searchDescriptor != null)
                {
                    services.Remove(searchDescriptor);
                }

                DataAccessMock = new Mock<IDataAccess>();
                services.AddSingleton(DataAccessMock.Object);
            });
        }
    }
}