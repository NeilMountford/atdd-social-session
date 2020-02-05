using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit;

namespace AtddExercise.UnitTests.BddfyReflective
{
    public class NoNumbers : IClassFixture<MockingWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private string _numbers;
        private HttpResponseMessage _response;

        public NoNumbers(MockingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [BddfyFact]
        public void NoNumbersReturnsFailure()
        {
            this.BDDfy();
        }
        
        private void GivenNoNumbers()
        {
            _numbers = "";
        }

        private async Task WhenAddingNumbers()
        {
            _response = await _client.GetAsync($"/calculator/add/{_numbers}");
        }

        private void ThenTheResponseIndicatesFailure()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}