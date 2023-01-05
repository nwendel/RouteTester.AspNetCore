using System.Reflection;
using MvcRouteTester.AspNetCore.Infrastructure;

namespace MvcRouteTester.AspNetCore.Internal;

public class ActualActionInvokeInfo
{
    public ActualActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, object?> arguments)
    {
        GuardAgainst.Null(actionMethodInfo);
        GuardAgainst.Null(arguments);

        ActionMethodInfo = actionMethodInfo;
        Arguments = arguments;
    }

    public MethodInfo ActionMethodInfo { get; set; }

    public IReadOnlyDictionary<string, object?> Arguments { get; set; }
}
