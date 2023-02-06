using Microsoft.AspNetCore.Mvc;
using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;

namespace RouteTester.AspNetCore.Tests;

public sealed class InvalidRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task ThrowsOnMapsToStaticMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/invalid/static"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => InvalidController.Static())));
    }

    [Fact]
    public async Task ThrowsOnMapsToNonMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/invalid/static"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => (IActionResult)null!)));
    }

    [Fact]
    public async Task ThrowsOnMapsToNonActionMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/invalid/non-action"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => a.NonAction())));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
