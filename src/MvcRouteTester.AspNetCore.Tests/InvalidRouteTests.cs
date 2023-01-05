using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Infrastructure;
using TestWebApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests;

public class InvalidRouteTests : IClassFixture<TestServerFixture>
{
    private readonly TestServer _server;

    public InvalidRouteTests(TestServerFixture testServerFixture)
    {
        GuardAgainst.Null(testServerFixture);

        _server = testServerFixture.Server;
    }

    [Fact]
    public async Task ThrowsOnMapsToStaticMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/invalid/static"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => InvalidController.Static())));
    }

    [Fact]
    public async Task ThrowsOnMapsToNonMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/invalid/static"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => (IActionResult)null!)));
    }

    [Fact]
    public async Task ThrowsOnMapsToNonActionMethod()
    {
        await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
            RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/invalid/non-action"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => a.NonAction())));
    }
}
