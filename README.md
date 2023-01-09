# MvcRouteTester.AspNetCore [![Build status](https://ci.appveyor.com/api/projects/status/sot37agt946gbm93?svg=true)](https://ci.appveyor.com/project/nwendel/mvcroutetester-aspnetcore)

### NuGet Package

```
Install-Package MvcRouteTester.AspNetCore
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
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SomeRoute()));
    }

    [Fact]
    public async Task CanRouteWithArguments()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/some-other-route?parameter=value"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SomeOtherRoute("value")));
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
            serviceCollection.AddMvcRouteTester();
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
