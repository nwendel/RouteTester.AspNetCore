using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;

namespace MvcRouteTester.AspNetCore;

public static class RouteAssert
{
    [Obsolete("Temporary")]
    public static void For(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
    {
        Task.Run(async () => await ForAsync(server, requestBuilder, routeAssertBuilder))
            .GetAwaiter().GetResult();
    }

    public static void For(IWebApplicationFactory factory, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
    {
        Task.Run(async () => await ForAsync(factory, requestBuilder, routeAssertBuilder))
            .GetAwaiter().GetResult();
    }

    [Obsolete("Temporary")]
    public static async Task ForAsync(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
    {
        GuardAgainst.Null(server);
        GuardAgainst.Null(requestBuilder);
        GuardAgainst.Null(routeAssertBuilder);

        var serviceProvider = server.Host.Services;
        var request = serviceProvider.GetRequiredService<RouteTesterRequest>();
        var routeAssert = serviceProvider.GetRequiredService<RouteTesterRouteAssert>();

        requestBuilder(request);
        routeAssertBuilder(routeAssert);

        var responseMessage = await request.ExecuteAsync(server);
        await routeAssert.AssertExpectedAsync(responseMessage);
    }

    public static async Task ForAsync(IWebApplicationFactory factory, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
    {
        GuardAgainst.Null(factory);
        GuardAgainst.Null(requestBuilder);
        GuardAgainst.Null(routeAssertBuilder);

        var serviceProvider = factory.Services;
        var request = serviceProvider.GetRequiredService<RouteTesterRequest>();
        var routeAssert = serviceProvider.GetRequiredService<RouteTesterRouteAssert>();

        requestBuilder(request);
        routeAssertBuilder(routeAssert);

        var responseMessage = await request.ExecuteAsync(factory);
        await routeAssert.AssertExpectedAsync(responseMessage);
    }
}
