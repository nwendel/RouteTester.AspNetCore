using System.Collections.Generic;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal
{
    public class ActualActionInvokeInfo
    {
        public ActualActionInvokeInfo(
            MethodInfo actionMethodInfo,
            IReadOnlyDictionary<string, object?> arguments)
        {
            ActionMethodInfo = actionMethodInfo;
            Arguments = arguments;
        }

        public MethodInfo ActionMethodInfo { get; set; }

        public IReadOnlyDictionary<string, object?> Arguments { get; set; }
    }
}
