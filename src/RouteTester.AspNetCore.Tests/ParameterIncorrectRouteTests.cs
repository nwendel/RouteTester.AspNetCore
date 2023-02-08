using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit.Sdk;

namespace RouteTester.AspNetCore.Tests;

public sealed class ParameterIncorrectRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task ThrowsOnMapsToIncorrectController()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                assert => assert.MapsToControllerAction<ParameterController>(a => a.SameName(default, default(int)))));
    }

    [Fact]
    public async Task ThrowsOnForParameterInvalidType()
    {
        await Assert.ThrowsAsync<ArgumentException>("action", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                assert => assert
                    .MapsToControllerAction<ParameterController>(a => a.SameName(default, default(string)))
                    .ForParameter<int>("parameter2", p => { })));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
