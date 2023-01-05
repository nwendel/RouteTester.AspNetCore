using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Infrastructure;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class NotFoundRouteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public NotFoundRouteTests(TestServerFixture testServerFixture)
        {
            GuardAgainst.Null(testServerFixture);

            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task CanRouteNotFound()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/non-existant-route"),
                routeAssert => routeAssert.NotFound());
        }

        [Fact]
        public async Task ThrowsOnFound()
        {
            await Assert.ThrowsAsync<FalseException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.NotFound()));
        }
    }
}
