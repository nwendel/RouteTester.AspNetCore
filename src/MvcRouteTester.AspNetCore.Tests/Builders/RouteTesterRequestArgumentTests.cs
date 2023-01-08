using MvcRouteTester.AspNetCore.Builders;

namespace MvcRouteTester.AspNetCore.Tests.Builders;

public class RouteTesterRequestArgumentTests
{
    [Fact]
    public void ThrowsOnWithMethodNullMethod()
    {
        using var tested = new RouteTesterRequest();

        Assert.Throws<ArgumentNullException>("method", () => tested.WithMethod(null!));
    }

    [Fact]
    public void ThrowsOnWithPathAndQueryNullPathAndQuery()
    {
        using var tested = new RouteTesterRequest();

        Assert.Throws<ArgumentNullException>("pathAndQuery", () => tested.WithPathAndQuery(null!));
    }

    [Fact]
    public void ThrowsOnWithFormContentNull()
    {
        using var tested = new RouteTesterRequest();

        Assert.Throws<ArgumentNullException>("content", () => tested.WithFormContent(null!));
    }

    [Fact]
    public void ThrowsOnWithJsonContentNull()
    {
        using var tested = new RouteTesterRequest();

        Assert.Throws<ArgumentNullException>("content", () => tested.WithJsonContent(null!));
    }
}
