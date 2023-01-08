using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class ParameterIncorrectRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public ParameterIncorrectRouteTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task ThrowsOnMapsToIncorrectController()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.SameName(default!, default(int)))));
    }

    [Fact]
    public async Task ThrowsOnForParameterInvalidType()
    {
        await Assert.ThrowsAsync<ArgumentException>("action", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                routeAssert => routeAssert
                    .MapsTo<ParameterController>(a => a.SameName(default!, default(string)!))
                    .ForParameter<int>("parameter2", p => { })));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
