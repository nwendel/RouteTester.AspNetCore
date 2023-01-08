using TestApplication.Controllers;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class QueryStringParameterRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public QueryStringParameterRouteTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task CanRouteWithParameter()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
            routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("value")));
    }

    [Fact]
    public async Task ThrowsOnRouteWithParameterWrongValue()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
           RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("wrong-value"))));
    }

    [Fact]
    public async Task CanRouteWithoutParameter()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/parameter/query-string-parameter"),
            routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(null!)));
    }

    [Fact]
    public async Task CanRouteWithParameterMatchingAny()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
            routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>())));
    }

    [Fact]
    public async Task CanRouteWithParameterForParameterExpression()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
            routeAssert => routeAssert
                .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                .ForParameter<string>("parameter", p =>
                {
                    Assert.True(p == "value");
                }));
    }

    [Fact]
    public async Task ThrowsOnRouteWithParameterForParameterExpressionWrongValue()
    {
        await Assert.ThrowsAsync<EqualException>(() =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=wrong-value"),
                routeAssert => routeAssert
                    .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                    .ForParameter<string>("parameter", p =>
                    {
                        Assert.Equal("value", p);
                    })));
    }

    [Fact]
    public async Task ThrowsOnRouteWithParameterForParameterWrongParameterName()
    {
        await Assert.ThrowsAsync<ArgumentException>("name", () =>
            RouteAssert.ForAsync(
                _factory.Server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=wrong-value"),
                routeAssert => routeAssert
                    .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                    .ForParameter<string>("wrong-parameter", p =>
                    {
                        Assert.Equal("value", p);
                    })));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
