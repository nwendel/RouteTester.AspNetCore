using Microsoft.Extensions.DependencyInjection;
using RouteTester.AspNetCore.Builders;
using RouteTester.AspNetCore.Internal;

namespace RouteTester.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static void AddRouteTester(this IServiceCollection serviceCollection)
    {
        if (serviceCollection.All(x => x.ServiceType != typeof(IActionInvokerFactory)))
        {
            throw new RouteTesterException("AddRouteTester() must be called after AddMvc()");
        }

        // Controllers
        serviceCollection.AddTransient<RouteTesterRequest>();
        serviceCollection.AddTransient<RouteTesterRouteAssert>();
        serviceCollection.AddTransient<RouteTesterMapsToRouteAssert>();
        serviceCollection.AddTransient<RouteTesterNotFoundRouteAssert>();
        serviceCollection.AddSingleton<ActualActionInvokeInfoCache>();

        // RazorPages
        serviceCollection.Configure<MvcOptions>(o => o.Filters.Add<RouteTesterActionFilterAttribute>());
    }
}
