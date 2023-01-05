using System.Linq.Expressions;

namespace MvcRouteTester.AspNetCore.Builders;

public interface IRouteAssertBuilder
{
    IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, IActionResult>> actionCallExpression)
        where TController : ControllerBase;

    IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, Task<IActionResult>>> actionCallExpression)
        where TController : ControllerBase;

    void NotFound();
}
