using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class NotFoundRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public NotFoundRouteTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task CanRouteNotFound()
    {
        await RouteAssert.ForAsync(
            _factory,
            request => request.WithPathAndQuery("/non-existant-route"),
            routeAssert => routeAssert.NotFound());
    }

    [Fact]
    public async Task ThrowsOnFound()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.NotFound()));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
