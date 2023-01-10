﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MvcRouteTester.AspNetCore.Internal;

public sealed class MvcRouteTesterActionFilterAttribute : ActionFilterAttribute
{
    public MvcRouteTesterActionFilterAttribute()
    {
        // TODO: Or possibly int.MaxValue?
        //       Not sure if it should run first or last...
        //       Maybe configurable? But question remains what should be default
        Order = int.MinValue;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        GuardAgainst.Null(context);

        var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

        var actionInvokeInfo = new ActualActionInvokeInfo(
            actionDescriptor.MethodInfo,
            context.ActionArguments.AsReadOnly());

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
