using System;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;
using Xunit.Sdk;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class SynchTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public SynchTests(TestServerFixture testServerFixture)
        {
            if (testServerFixture == null)
            {
                throw new ArgumentNullException(nameof(testServerFixture));
            }

            _server = testServerFixture.Server;
        }

        [Fact]
        public void CanGetSimpleAttributeRoute()
        {
            RouteAssert.For(
                _server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute()));
        }

        [Fact]
        public void ThrowsOnMapsToIncorrectController()
        {
            Assert.Throws<EqualException>(() =>
                RouteAssert.For(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => a.Default())));
        }
    }
}
