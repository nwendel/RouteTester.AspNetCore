using RouteTester.AspNetCore.Tests.TestHelpers;
using Xunit.Sdk;

namespace RouteTester.AspNetCore.Tests;

public sealed class NotFoundRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task CanRouteNotFound()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/non-existant-route"),
            routeAssert => routeAssert.NotFound());
    }

    [Fact]
    public async Task ThrowsOnFound()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.NotFound()));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
