﻿using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;

namespace MvcRouteTester.AspNetCore;

public static class RouteAssert
{
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
}
