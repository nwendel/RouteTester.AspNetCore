# RouteTester.AspNetCore ![Build](https://github.com/nwendel/routetester.aspnetcore/actions/workflows/build.yml/badge.svg) [![Coverage](https://codecov.io/gh/nwendel/routetester.aspnetcore/branch/main/graph/badge.svg?token=BMNOSIWUMV)]

### NuGet Package

```
Install-Package RouteTester.AspNetCore
```

### Example
```csharp
public class Example
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task CanRoute()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/some-route"),
            assert => assert.MapsTo<HomeController>(a => a.SomeRoute()));
    }

    [Fact]
    public async Task CanRouteWithArguments()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/some-other-route?parameter=value"),
            assert => assert.MapsTo<HomeController>(a => a.SomeOtherRoute("value")));
    }
}

public class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override IWebHostBuilder? CreateWebHostBuilder()
    {
        var builder = new WebHostBuilder();
        builder.UseStartup<TestStartup>();

        return builder;
    }

    private sealed class TestStartup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMvc()
                .AddApplicationPart(typeof(Program).Assembly);
            serviceCollection.MvcRouteTester();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(x =>
            {
                x.MapControllers();
            });
        }
    }
}
```
