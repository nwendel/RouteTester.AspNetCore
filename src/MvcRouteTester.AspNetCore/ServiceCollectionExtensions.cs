using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static void AddMvcRouteTester(this IServiceCollection serviceCollection)
    {
        if (serviceCollection.All(x => x.ServiceType != typeof(IActionInvokerFactory)))
        {
            throw new MvcRouteTesterException("AddMvcRouteTester() must be called after AddMvc()");
        }

        // Register services needed by MvcRouteTester
        serviceCollection.AddTransient<RouteTesterRequest>();
        serviceCollection.AddTransient<RouteTesterRouteAssert>();
        serviceCollection.AddTransient<RouteTesterMapsToRouteAssert>();
        serviceCollection.AddTransient<RouteTesterNotFoundRouteAssert>();
        serviceCollection.AddSingleton<ActualActionInvokeInfoCache>();
        serviceCollection.AddSingleton<RouteExpressionParser>();

        serviceCollection.Configure<MvcOptions>(o => o.Filters.Add<MvcRouteTesterActionFilterAttribute>());
    }
}
