using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcRouteTester.AspNetCore.Internal;

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

        throw new NotImplementedException();
    }
}
