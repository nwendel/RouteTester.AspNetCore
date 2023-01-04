using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcRouteTester.AspNetCore.Internal.Wrappers;

public class ControllerActionInvokerCacheWrapper
{
    public (ControllerActionInvokerCacheEntryWrapper cacheEntry, IFilterMetadata[] filters) GetCachedResult(ControllerContext controllerContext)
    {
        return (null!, null!);
    }
}
