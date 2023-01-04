using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcRouteTester.AspNetCore.Internal.Wrappers;

public class ControllerBinderDelegateWrapper
{
    public Task Invoke(ControllerContext controllerContext, object controller, Dictionary<string, object> arguments)
    {
        return Task.CompletedTask;
    }
}
