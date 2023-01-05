using System;
using MvcRouteTester.AspNetCore.Builders;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests.Builders
{
    public class RouteTesterMapsToRouteAssertTests
    {
        [Fact]
        public void ThrowsOnForParameterNullParameterName()
        {
            var tested = new RouteTesterMapsToRouteAssert(null!, null!, null!);

            Assert.Throws<ArgumentNullException>("name", () =>
                tested.ForParameter<string>(null!, p => { }));
        }

        [Fact]
        public void ThrowsOnForParameterNullAction()
        {
            var tested = new RouteTesterMapsToRouteAssert(null!, null!, null!);

            Assert.Throws<ArgumentNullException>("action", () =>
                tested.ForParameter<string>("parameter", null!));
        }
    }
}
