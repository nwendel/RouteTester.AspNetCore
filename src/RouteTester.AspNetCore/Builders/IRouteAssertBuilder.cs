using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RouteTester.AspNetCore.Builders;

public interface IRouteAssertBuilder
{
    IMapsToControllerActionBuilder MapsToControllerAction<TController>(Expression<Func<TController, IActionResult>> actionCallExpression)
        where TController : ControllerBase;

    IMapsToControllerActionBuilder MapsToControllerAction<TController>(Expression<Func<TController, Task<IActionResult>>> actionCallExpression)
        where TController : ControllerBase;

    void MapsToPageModel<TPageModel>()
        where TPageModel : PageModel;

    void NotFound();
}
