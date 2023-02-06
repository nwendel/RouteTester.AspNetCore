using System.Reflection;

namespace RouteTester.AspNetCore.Internal;

public static class MethodInfoExtensions
{
    public static string GetActionText(this MethodInfo self)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Condition(self.ReflectedType == null, "No ReflectedType", nameof(self));

        var parameters = string.Join(",", self.GetParameters().Select(x => x.ParameterType));
        var actionText = $"{self.ReflectedType.Name}.{self.Name}({parameters})";
        return actionText;
    }
}
