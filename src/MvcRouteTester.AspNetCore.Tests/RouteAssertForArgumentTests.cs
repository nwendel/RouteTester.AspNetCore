using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class RouteAssertForArgumentTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public RouteAssertForArgumentTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task ThrowsOnNullServer()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("server", () =>
                RouteAssert.ForAsync(
                    null,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));
        }

        [Fact]
        public async Task ThrowsOnNullRequestBuilder()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("requestBuilder", () =>
                RouteAssert.ForAsync(
                    _server,
                    null,
                    routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));
        }

        [Fact]
        public async Task ThrowsOnNullRouteAssertBuilder()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("routeAssertBuilder", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    null));
        }
    }
}
