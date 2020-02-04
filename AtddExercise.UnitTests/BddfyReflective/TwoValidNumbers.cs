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
    public class TwoValidNumbers : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private string _numbers;
        private HttpResponseMessage _response;

        public TwoValidNumbers(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [BddfyFact]
        public void TwoValidNumbersReturnsSuccessWithCorrectResult()
        {
            this.BDDfy();
        }
        
        private void GivenTwoValidNumbers()
        {
            _numbers = "3,2";
        }

        private async Task WhenAddingNumbers()
        {
            _response = await _client.GetAsync($"/calculator/add/{_numbers}");
        }

        private void ThenTheResponseIndicatesSuccess()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private async Task AndTheExpectedResultIsReturned()
        {
            var body = await _response.Content.ReadAsStringAsync();
            body.ShouldBe("5");
        }
    }
}