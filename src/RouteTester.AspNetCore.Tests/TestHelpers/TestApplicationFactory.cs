using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Internal;

namespace RouteTester.AspNetCore.Tests.TestHelpers;

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
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllers()
                .AddApplicationPart(typeof(Program).Assembly);
            serviceCollection
                .AddRazorPages()
                .AddMvcOptions(o => o.Filters.Add(new RouteTesterPageFilter()))
                .AddApplicationPart(typeof(Program).Assembly);
            serviceCollection.AddRouteTester();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(x =>
            {
                x.MapControllers();
                x.MapRazorPages();
            });
        }
    }
}
