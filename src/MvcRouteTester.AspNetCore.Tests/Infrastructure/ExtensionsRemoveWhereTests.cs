using System;
using System.Collections.Generic;
using MvcRouteTester.AspNetCore.Infrastructure;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests.Infrastructure
{
    public class ExtensionsRemoveWhereTests
    {
        [Fact]
        public void CanRemoveWhere()
        {
            var tested = new List<string> { "one", "two" };

            tested.RemoveWhere(x => x == "one");

            Assert.Single(tested);
            Assert.Contains("two", tested);
        }

        [Fact]
        public void ThrowsOnRemoveWhereNullSelf()
        {
            List<string> tested = null!;

            Assert.Throws<ArgumentNullException>("self", () => tested.RemoveWhere(x => x == "one"));
        }

        [Fact]
        public void ThrowsOnRemoveWhereNullItems()
        {
            var tested = new List<string> { "one" };

            Assert.Throws<ArgumentNullException>("predicate", () => tested.RemoveWhere(null!));
        }
    }
}
