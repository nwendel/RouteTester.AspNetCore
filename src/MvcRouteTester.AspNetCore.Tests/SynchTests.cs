using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class SynchTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public SynchTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public void CanGetSimpleAttributeRoute()
    {
        RouteAssert.For(
            _factory,
            request => request.WithPathAndQuery("/simple-attribute-route"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute()));
    }

    [Fact]
    public void ThrowsOnMapsToIncorrectController()
    {
        Assert.Throws<EqualException>(() =>
            RouteAssert.For(
                _factory,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<InvalidController>(a => a.Default())));
    }

    public void Dispose()
    {
        _factory?.Dispose();
    }
}
