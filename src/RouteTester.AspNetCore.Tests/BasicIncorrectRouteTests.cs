using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit.Sdk;

namespace RouteTester.AspNetCore.Tests;

public sealed class BasicIncorrectRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task ThrowsOnMapsToIncorrectController()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsToControllerAction<InvalidController>(a => a.Default())));
    }

    [Fact]
    public async Task ThrowsOnMapsToIncorrectActionMethodName()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsToControllerAction<HomeController>(a => a.SimpleAttributeRouteAsync())));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
