using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class ParameterIncorrectRouteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public ParameterIncorrectRouteTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task ThrowsOnMapsToIncorrectController()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                    routeAssert => routeAssert.MapsTo<ParameterController>(a => a.SameName(default(string), default(int)))));
        }

        [Fact]
        public async Task ThrowsOnForParameterInvalidType()
        {
            await Assert.ThrowsAsync<ArgumentException>("T", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/same-name-with-string"),
                    routeAssert => routeAssert
                        .MapsTo<ParameterController>(a => a.SameName(default(string), default(string)))
                        .ForParameter<int>("parameter2", p => { })));
        }
    }
}
