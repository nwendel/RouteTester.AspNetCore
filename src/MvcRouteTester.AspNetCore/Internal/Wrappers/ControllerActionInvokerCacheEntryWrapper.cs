using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal.Wrappers;

public class ControllerActionInvokerCacheEntryWrapper
{
    private static readonly PropertyInfo _objectMethodExecutorPropertyInfo = typeof(ControllerActionInvokerCacheEntry).GetProperty("ObjectMethodExecutor", BindingFlags.NonPublic | BindingFlags.Instance);

    private readonly object _instance = null!;

    public ControllerBinderDelegateWrapper ControllerBinderDelegate { get; internal set; }

    public object GetObjectMethodExecutor()
    {
        var value = _objectMethodExecutorPropertyInfo.GetValue(_instance);
        return new ObjectMethodExecutor(value);
    }
}
