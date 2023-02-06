using System.Reflection;

namespace RouteTester.AspNetCore.Internal;

public class ExpectedActionInvokeInfo : ActionInvokeInfo<ExpectedArgumentAssert>
{
    public ExpectedActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, ExpectedArgumentAssert> arguments)
        : base(actionMethodInfo, arguments)
    {
    }
}
