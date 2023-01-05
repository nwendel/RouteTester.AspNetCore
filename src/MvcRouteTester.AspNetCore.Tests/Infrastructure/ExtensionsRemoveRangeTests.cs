using MvcRouteTester.AspNetCore.Infrastructure;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests.Infrastructure
{
    public class ExtensionsRemoveRangeTests
    {
        [Fact]
        public void CanRemoveRange()
        {
            var tested = new List<string> { "one", "two", "three" };

            tested.RemoveRange(new[] { "one", "two" });

            Assert.Single(tested);
            Assert.Contains("three", tested);
        }

        [Fact]
        public void ThrowsOnRemoveRangeNullSelf()
        {
            List<string> tested = null!;

            Assert.Throws<ArgumentNullException>("self", () => tested.RemoveRange(new[] { "one" }));
        }

        [Fact]
        public void ThrowsOnRemoveRangeNullItems()
        {
            var tested = new List<string> { "one" };

            Assert.Throws<ArgumentNullException>("items", () => tested.RemoveRange(null!));
        }
    }
}
