using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MvcRouteTester.AspNetCore.Tests.TestHelpers;

internal sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override IWebHostBuilder? CreateWebHostBuilder()
    {
        var builder = new WebHostBuilder();
        builder.UseStartup<TestStartup>();

        return builder;
    }

#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    private sealed class TestStartup
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Temporary")]
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMvc()
                .AddApplicationPart(typeof(Program).Assembly);
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
