using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace MvcRouteTester.AspNetCore.Tests.TestHelpers;

internal sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseTestServer();

        builder.ConfigureServices(x =>
        {
            x.AddMvcRouteTester();
        });

        base.ConfigureWebHost(builder);
    }
}
