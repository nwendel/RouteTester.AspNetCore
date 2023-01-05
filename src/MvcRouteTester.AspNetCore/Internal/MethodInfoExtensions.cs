using System.Reflection;
using System.Text;
using MvcRouteTester.AspNetCore.Infrastructure;

namespace MvcRouteTester.AspNetCore.Internal;

public static class MethodInfoExtensions
{
    public static string GetActionText(this MethodInfo self, Func<Type, string> getTypeName)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Null(getTypeName);

        var builder = new StringBuilder();
        builder.Append(getTypeName(self.ReflectedType!));
        builder.Append('.');
        builder.Append(self.Name);
        builder.Append('(');
        builder.Append(string.Join(",", self
            .GetParameters()
            .Select(p => getTypeName(p.ParameterType))));
        builder.Append(')');
        return builder.ToString();
    }
}
