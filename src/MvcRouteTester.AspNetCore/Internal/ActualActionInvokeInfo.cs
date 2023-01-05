using System.Collections.Generic;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal
{
    public class ActualActionInvokeInfo
    {
        public MethodInfo ActionMethodInfo { get; set; }

        public IReadOnlyDictionary<string, object> Arguments { get; set; }
    }
}
