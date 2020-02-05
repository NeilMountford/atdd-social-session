using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace AtddExercise.UnitTests.When_Adding_Numbers
{
    public class Given_Two_Valid_Numbers : IAsyncLifetime, IClassFixture<MockingWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;

        public Given_Two_Valid_Numbers(MockingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        public async Task InitializeAsync()
        {
            var numbers = "3,2";
            _response = await _client.GetAsync($"/calculator/add/{numbers}");
        }

        [Fact]
        public void Then_The_Response_Indicates_Success()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Then_The_Expected_Result_Is_Returned()
        {
            var body = await _response.Content.ReadAsStringAsync();
            body.ShouldBe("5");
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}