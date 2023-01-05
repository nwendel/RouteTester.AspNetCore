using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Infrastructure;
using TestWebApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests;

public class BasicRouteTests : IClassFixture<TestServerFixture>
{
    private readonly TestServer _server;

    public BasicRouteTests(TestServerFixture testServerFixture)
    {
        GuardAgainst.Null(testServerFixture);

        _server = testServerFixture.Server;
    }

    [Fact]
    public async Task CanGetSimpleAttributeRoute()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/simple-attribute-route"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute()));
    }

    [Fact]
    public async Task CanGetSimpleAttributeRouteAsync()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/simple-attribute-route-async"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync()));
    }

    [Fact]
    public async Task CanPostSimpleAttributeRoute()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request
                .WithMethod(HttpMethod.Post)
                .WithPathAndQuery("/simple-attribute-route-post"),
            routeAssert => routeAssert.MapsTo<PostController>(a => a.SimpleAttributeRoute()));
    }
}
