using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;

namespace RouteTester.AspNetCore.Tests;

public sealed class RouteAssertForArgumentTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task ThrowsOnNullServer()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("server", () =>
            RouteAssert.ForAsync(
                null!,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));
    }

    [Fact]
    public async Task ThrowsOnNullRequestBuilder()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("requestBuilder", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                null!,
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));
    }

    [Fact]
    public async Task ThrowsOnNullRouteAssertBuilder()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("routeAssertBuilder", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                null!));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
