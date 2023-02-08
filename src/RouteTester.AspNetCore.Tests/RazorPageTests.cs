using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Pages;

namespace RouteTester.AspNetCore.Tests;

public sealed class RazorPageTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact(Skip = "Not implemented yet")]
    public async Task Asdf()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/Simple"),
            routeAssert => routeAssert.MapsToPageModel<SimplePageModel>());
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
