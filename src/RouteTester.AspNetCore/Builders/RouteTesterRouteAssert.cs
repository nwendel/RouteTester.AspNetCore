using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace RouteTester.AspNetCore.Builders;

public class RouteTesterRouteAssert :
    IRouteAssertBuilder,
    IRouteAssert
{
    private readonly IServiceProvider _serviceProvider;

    private IRouteAssert? _routeAssert;

    public RouteTesterRouteAssert(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMapsToControllerActionBuilder MapsToControllerAction<TController>(Expression<Func<TController, IActionResult>> actionCallExpression)
        where TController : ControllerBase
    {
        return MapsToControllerActionCore(actionCallExpression);
    }

    public IMapsToControllerActionBuilder MapsToControllerAction<TController>(Expression<Func<TController, Task<IActionResult>>> actionCallExpression)
        where TController : ControllerBase
    {
        return MapsToControllerActionCore(actionCallExpression);
    }

    public IMapsToControllerActionBuilder MapsToPageModel<TPageModel>()
        where TPageModel : PageModel
    {
        return null!;
    }

    public void NotFound()
    {
        var builder = _serviceProvider.GetRequiredService<NotFoundRouteAssert>();
        _routeAssert = builder;
    }

    public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        if (_routeAssert == null)
        {
            throw new InvalidOperationException("No route assert");
        }

        await _routeAssert.AssertExpectedAsync(responseMessage);
    }

    private IMapsToControllerActionBuilder MapsToControllerActionCore(LambdaExpression actionCallExpression)
    {
        var builder = _serviceProvider.GetRequiredService<MapsToControllerActionRouteAssert>();
        builder.ParseActionCallExpression(actionCallExpression);
        _routeAssert = builder;
        return builder;
    }
}
