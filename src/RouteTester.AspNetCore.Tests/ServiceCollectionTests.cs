using Microsoft.Extensions.DependencyInjection;

namespace RouteTester.AspNetCore.Tests;

public class ServiceCollectionTests
{
    [Fact]
    public void ThrowsOnAddRouteTeserBeforeAddMvc()
    {
        var tested = new ServiceCollection();

        Assert.Throws<RouteTesterException>(() => tested.AddRouteTester());
    }
}
