using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal;

public class ExpectedActionInvokeInfo : ActionInvokeInfo<ExpectedArgumentAssert>
{
    public ExpectedActionInvokeInfo(
        MethodInfo actionMethodInfo,
        IReadOnlyDictionary<string, ExpectedArgumentAssert> arguments)
        : base(actionMethodInfo, arguments)
    {
    }
}
