using System.Collections.Generic;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal
{
    public class ExpectedActionInvokeInfo : ActualActionInvokeInfo
    {
        public ExpectedActionInvokeInfo(
            MethodInfo actionMethodInfo,
            IReadOnlyDictionary<string, object?> arguments,
            IReadOnlyDictionary<string, ArgumentAssertKind> argumentAssertKinds)
            : base(actionMethodInfo, arguments)
        {
            ArgumentAssertKinds = argumentAssertKinds;
        }

        public IReadOnlyDictionary<string, ArgumentAssertKind> ArgumentAssertKinds { get; set; }
    }
}
