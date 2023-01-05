using System.Reflection;
using MvcRouteTester.AspNetCore.Infrastructure;

namespace MvcRouteTester.AspNetCore.Internal;

public class ExpectedActionInvokeInfo : ActualActionInvokeInfo
{
    public ExpectedActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, object?> arguments,
        IReadOnlyDictionary<string, ArgumentAssertKind> argumentAssertKinds)
        : base(actionMethodInfo, arguments)
    {
        GuardAgainst.Null(argumentAssertKinds);

        ArgumentAssertKinds = argumentAssertKinds;
    }

    public IReadOnlyDictionary<string, ArgumentAssertKind> ArgumentAssertKinds { get; set; }
}
