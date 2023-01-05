using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Infrastructure;

namespace MvcRouteTester.AspNetCore.Internal;

public sealed class MvcRouteTesterActionFilterAttribute : ActionFilterAttribute
{
    public MvcRouteTesterActionFilterAttribute()
    {
        // TODO: Or possible int.MaxValue?
        //       Not sure if it should run first or last...
        Order = int.MinValue;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        GuardAgainst.Null(context);

        var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

        var actionInvokeInfo = new ActualActionInvokeInfo(
            actionDescriptor.MethodInfo,
            new Dictionary<string, object?>(context.ActionArguments));

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
