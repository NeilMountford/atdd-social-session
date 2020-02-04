using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtddExercise.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace AtddExercise.UnitTests.YaBasic
{
    public class AddTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AddTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Given_Two_Valid_Numbers_Separated_By_A_Comma_When_Adding_The_Numbers_Then_The_Expected_Result_Is_Returned_And_The_Response_Indicates_Success()
        {
            var numbers = "3,2";
            var response = await _client.GetAsync($"/calculator/add/{numbers}");
            
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var body = await response.Content.ReadAsStringAsync();
            body.ShouldBe("5");
        }
    }
}