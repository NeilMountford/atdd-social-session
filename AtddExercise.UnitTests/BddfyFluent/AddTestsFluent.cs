using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit;

namespace AtddExercise.UnitTests.BddfyFluent
{
    public class AddTestsFluent : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private string _numbers;
        private HttpResponseMessage _response;

        public AddTestsFluent(WebApplicationFactory<Startup> factory)
        {
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
}