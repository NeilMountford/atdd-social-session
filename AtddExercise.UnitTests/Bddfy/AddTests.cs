using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit;

namespace AtddExercise.UnitTests.Bddfy
{
    public class AddTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private string _numbers;
        private HttpResponseMessage _response;

        public AddTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [BddfyFact]
        public void Two_Valid_Numbers_Returns_Success_With_Correct_Result()
        {
            this.Given(_ => _.TwoValidNumbers())
                .When(_ => _.AddingNumbers())
                .Then(_ => TheResponseIndicatesSuccess())
                .And(_ => _.TheExpectedResultIsReturned())
                .BDDfy();
        }

        private void TwoValidNumbers()
        {
            _numbers = "3,2";
        }

        private async Task AddingNumbers()
        {
            _response = await _client.GetAsync($"/calculator/add/{_numbers}");
        }

        private void TheResponseIndicatesSuccess()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private async Task TheExpectedResultIsReturned()
        {
            var body = await _response.Content.ReadAsStringAsync();
            body.ShouldBe("5");
        }
    }
}