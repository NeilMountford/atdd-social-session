using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace AtddExercise.UnitTests.When_Adding_Numbers
{
    public class Given_No_Numbers : IAsyncLifetime, IClassFixture<MockingWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;

        public Given_No_Numbers(MockingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        public async Task InitializeAsync()
        {
            var numbers = "";
            _response = await _client.GetAsync($"/calculator/add/{numbers}");
        }

        [Fact]
        public void Then_The_Response_Indicates_Failure()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}