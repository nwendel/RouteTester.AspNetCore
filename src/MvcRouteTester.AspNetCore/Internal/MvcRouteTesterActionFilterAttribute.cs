using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MvcRouteTester.AspNetCore.Internal;

public sealed class MvcRouteTesterActionFilterAttribute : ActionFilterAttribute
{
    public MvcRouteTesterActionFilterAttribute()
    {
        Order = int.MinValue;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

        var actionInvokeInfo = new ActualActionInvokeInfo
        {
            ActionMethodInfo = actionDescriptor.MethodInfo,
            Arguments = new Dictionary<string, object>(context.ActionArguments),
        };

        var actionInvokeInfoCache = context.HttpContext.RequestServices.GetRequiredService<ActualActionInvokeInfoCache>();

        var key = Guid.NewGuid().ToString();
        actionInvokeInfoCache.Add(key, actionInvokeInfo);

        var contentResult = new ContentResult
        {
            Content = key,
        };

        context.Result = contentResult;
    }
}
