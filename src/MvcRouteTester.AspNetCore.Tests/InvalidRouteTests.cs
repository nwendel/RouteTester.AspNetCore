using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class InvalidRouteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public InvalidRouteTests(TestServerFixture testServerFixture)
        {
            if (testServerFixture == null)
            {
                throw new ArgumentNullException(nameof(testServerFixture));
            }

            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task ThrowsOnMapsToStaticMethod()
        {
            await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/invalid/static"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => InvalidController.Static())));
        }

        [Fact]
        public async Task ThrowsOnMapsToNonMethod()
        {
            await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/invalid/static"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => (IActionResult)null!)));
        }

        [Fact]
        public async Task ThrowsOnMapsToNonActionMethod()
        {
            await Assert.ThrowsAsync<ArgumentException>("actionCallExpression", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/invalid/non-action"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => a.NonAction())));
        }
    }
}
