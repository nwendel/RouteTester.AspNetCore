using System.Reflection;

namespace RouteTester.AspNetCore.Internal;

public class ActualActionInvokeInfo : ActionInvokeInfo<object?>
{
    public ActualActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, object?> arguments)
        : base(actionMethodInfo, arguments)
    {
    }
}
