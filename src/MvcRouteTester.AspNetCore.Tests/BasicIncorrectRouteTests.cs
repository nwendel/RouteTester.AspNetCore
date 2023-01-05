using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class BasicIncorrectRouteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public BasicIncorrectRouteTests(TestServerFixture testServerFixture)
        {
            if (testServerFixture == null)
            {
                throw new ArgumentNullException(nameof(testServerFixture));
            }

            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task ThrowsOnMapsToIncorrectController()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => a.Default())));
        }

        [Fact]
        public async Task ThrowsOnMapsToIncorrectActionMethodName()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync())));
        }
    }
}
