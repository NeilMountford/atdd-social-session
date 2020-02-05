using System.Linq;
using AtddExercise.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AtddExercise.UnitTests
{
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