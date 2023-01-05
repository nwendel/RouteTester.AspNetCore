using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;

namespace MvcRouteTester.AspNetCore
{
    public static class RouteAssert
    {
        public static void For(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
        {
            Task.Run(async () => await ForAsync(server, requestBuilder, routeAssertBuilder))
                .GetAwaiter().GetResult();
        }

        public static async Task ForAsync(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            if (requestBuilder == null)
            {
                throw new ArgumentNullException(nameof(requestBuilder));
            }

            if (routeAssertBuilder == null)
            {
                throw new ArgumentNullException(nameof(routeAssertBuilder));
            }

            var serviceProvider = server.Host.Services;
            var request = serviceProvider.GetRequiredService<RouteTesterRequest>();
            var routeAssert = serviceProvider.GetRequiredService<RouteTesterRouteAssert>();

            requestBuilder(request);
            routeAssertBuilder(routeAssert);

            var responseMessage = await request.ExecuteAsync(server);
            await routeAssert.AssertExpectedAsync(responseMessage);
        }
    }
}
