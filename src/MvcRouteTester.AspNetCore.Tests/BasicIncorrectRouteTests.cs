using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class BasicIncorrectRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public BasicIncorrectRouteTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task ThrowsOnMapsToIncorrectController()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => a.Default())));
    }

    [Fact]
    public async Task ThrowsOnMapsToIncorrectActionMethodName()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync())));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
