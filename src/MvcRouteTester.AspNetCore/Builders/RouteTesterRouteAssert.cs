using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MvcRouteTester.AspNetCore.Builders
{
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

        public IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, IActionResult>> actionCallExpression)
            where TController : ControllerBase
        {
            return MapsToCore(actionCallExpression);
        }

        public IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, Task<IActionResult>>> actionCallExpression)
            where TController : ControllerBase
        {
            return MapsToCore(actionCallExpression);
        }

        public void NotFound()
        {
            var builder = _serviceProvider.GetRequiredService<RouteTesterNotFoundRouteAssert>();
            _routeAssert = builder;
        }

        public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
        {
            await _routeAssert!.AssertExpectedAsync(responseMessage);
        }

        private IRouteAssertMapsToBuilder MapsToCore(LambdaExpression actionCallExpression)
        {
            var builder = _serviceProvider.GetRequiredService<RouteTesterMapsToRouteAssert>();
            builder.ParseActionCallExpression(actionCallExpression);
            _routeAssert = builder;
            return builder;
        }
    }
}
