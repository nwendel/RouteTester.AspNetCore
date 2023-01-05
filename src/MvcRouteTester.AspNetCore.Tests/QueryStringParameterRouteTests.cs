using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class QueryStringParameterRouteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public QueryStringParameterRouteTests(TestServerFixture testServerFixture)
        {
            if (testServerFixture == null)
            {
                throw new ArgumentNullException(nameof(testServerFixture));
            }

            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task CanRouteWithParameter()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("value")));
        }

        [Fact]
        public async Task ThrowsOnRouteWithParameterWrongValue()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
               RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                    routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("wrong-value"))));
        }

        [Fact]
        public async Task CanRouteWithoutParameter()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(null!)));
        }

        [Fact]
        public async Task CanRouteWithParameterMatchingAny()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>())));
        }

        [Fact]
        public async Task CanRouteWithParameterForParameterExpression()
        {
            await RouteAssert.ForAsync(
                _server,
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
                    _server,
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
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=wrong-value"),
                    routeAssert => routeAssert
                        .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                        .ForParameter<string>("wrong-parameter", p =>
                        {
                            Assert.Equal("value", p);
                        })));
        }
    }
}
