using Microsoft.AspNetCore.Mvc.Filters;

namespace RouteTester.AspNetCore.Internal;

public class RouteTesterPageFilter : IAsyncPageFilter
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        => Task.CompletedTask;

    public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        GuardAgainst.Null(context);

        var model = context.HandlerInstance;

        /*
        var actionDescriptor = context.ActionDescriptor;
        var m = actionDescriptor.ModelTypeInfo;
        var t = actionDescriptor.PageTypeInfo;
        */

        var key = Guid.NewGuid().ToString();

        var contentResult = new ContentResult
        {
            Content = key,
        };

        context.Result = contentResult;

        return Task.CompletedTask;
    }
}
