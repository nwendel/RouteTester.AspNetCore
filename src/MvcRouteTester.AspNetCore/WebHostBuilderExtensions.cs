using Microsoft.AspNetCore.Hosting;

namespace MvcRouteTester.AspNetCore;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder UseTestStartup<TTestStartup, TStartup>(this IWebHostBuilder self)
        where TTestStartup : class
        where TStartup : class
    {
        var applicationKey = typeof(TStartup).Assembly.FullName;

        return self
            .UseStartup<TTestStartup>()
            .UseSetting(WebHostDefaults.ApplicationKey, applicationKey);
    }
}
