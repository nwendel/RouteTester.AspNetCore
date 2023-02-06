using System.Reflection;

namespace RouteTester.AspNetCore.Internal;

public abstract class ActionInvokeInfo<T>
{
    protected ActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, T> arguments)
    {
        GuardAgainst.Null(actionMethodInfo);
        GuardAgainst.Null(arguments);

        ActionMethodInfo = actionMethodInfo;
        Arguments = arguments;
    }

    public MethodInfo ActionMethodInfo { get; }

    public IReadOnlyDictionary<string, T> Arguments { get; }
}
