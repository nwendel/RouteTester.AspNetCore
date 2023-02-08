using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Pages;

namespace RouteTester.AspNetCore.Tests;

public sealed class RazorPageTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task CanGetSimpleRazorPage()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/Simple"),
            assert => assert.MapsToRazorPage<SimplePageModel>());
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
