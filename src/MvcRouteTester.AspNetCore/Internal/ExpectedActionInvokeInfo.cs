using System.Collections.Generic;

namespace MvcRouteTester.AspNetCore.Internal
{
    public class ExpectedActionInvokeInfo : ActualActionInvokeInfo
    {
        public IReadOnlyDictionary<string, ArgumentAssertKind> ArgumentAssertKinds { get; set; }
    }
}
