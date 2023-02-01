using TestApplication.Pages;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class RazorPageTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task Asdf()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/Simple"),
            routeAssert => routeAssert.MapsTo<SimplePageModel>());
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
