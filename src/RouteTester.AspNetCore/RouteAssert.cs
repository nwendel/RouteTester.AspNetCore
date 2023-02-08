using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RouteTester.AspNetCore.Builders;

namespace RouteTester.AspNetCore;

public static class RouteAssert
{
    public static async Task ForAsync(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
    {
        GuardAgainst.Null(server);
        GuardAgainst.Null(requestBuilder);
        GuardAgainst.Null(routeAssertBuilder);

        var serviceProvider = server.Host.Services;
        var request = serviceProvider.GetRequiredService<Builders.RequestBuilder>();
        var routeAssert = serviceProvider.GetRequiredService<RouteAssertBuilder>();

        requestBuilder(request);
        routeAssertBuilder(routeAssert);

        var responseMessage = await request.ExecuteAsync(server);
        await routeAssert.AssertExpectedAsync(responseMessage);
    }
}
