using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestWebApplication;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class TestServerFixture
    {
        public TestServerFixture()
        {
            Server = new TestServer(new WebHostBuilder().UseTestStartup<TestStartup, Startup>());
        }

        public TestServer Server { get; }

        public class TestStartup
        {
            public void ConfigureServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddMvc();
                serviceCollection.AddMvcRouteTester();
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();

                app.UseEndpoints(x =>
                {
                    x.MapControllers();
                });
            }
        }
    }
}
