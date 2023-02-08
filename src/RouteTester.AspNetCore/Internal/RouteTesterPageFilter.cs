using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace RouteTester.AspNetCore.Internal;

public class RouteTesterPageFilter : IAsyncPageFilter
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        => Task.CompletedTask;

    public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        GuardAgainst.Null(context);

        var model = context.HandlerInstance;

        var actualPageModelCache = context.HttpContext.RequestServices.GetRequiredService<ActualPageModelCache>();
        var key = Guid.NewGuid().ToString();
        actualPageModelCache.Add(key, model);

        var contentResult = new ContentResult
        {
            Content = key,
        };

        context.Result = contentResult;

        return Task.CompletedTask;
    }
}
