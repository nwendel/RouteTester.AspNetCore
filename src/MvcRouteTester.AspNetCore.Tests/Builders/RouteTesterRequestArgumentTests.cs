using System;
using MvcRouteTester.AspNetCore.Builders;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests.Builders
{
    public class RouteTesterRequestArgumentTests
    {
        [Fact]
        public void ThrowsOnWithMethodNullMethod()
        {
            var tested = new RouteTesterRequest();

            Assert.Throws<ArgumentNullException>("method", () => tested.WithMethod(null));
        }

        [Fact]
        public void ThrowsOnWithPathAndQueryNullPathAndQuery()
        {
            var tested = new RouteTesterRequest();

            Assert.Throws<ArgumentNullException>("pathAndQuery", () => tested.WithPathAndQuery(null));
        }

        [Fact]
        public void ThrowsOnWithPathAndQueryNullFormData()
        {
            var tested = new RouteTesterRequest();

            Assert.Throws<ArgumentNullException>("formData", () => tested.WithFormData(null));
        }
    }
}
