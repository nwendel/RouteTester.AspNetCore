using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal;

public class ActualActionInvokeInfo : ActionInvokeInfo<object?>
{
    public ActualActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, object?> arguments)
        : base(actionMethodInfo, arguments)
    {
    }
}
