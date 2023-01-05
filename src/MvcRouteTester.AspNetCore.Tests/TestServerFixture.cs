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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Temporary")]
        public class TestStartup
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Temporary")]
            public void ConfigureServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddMvc();
                serviceCollection.AddMvcRouteTester();
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Temporary")]
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
