using System.Linq.Expressions;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal;

public class RouteExpressionParser
{
    private readonly MethodInfo _anyMethod = typeof(Args).GetMethod(nameof(Args.Any)) ?? throw new UnreachabelCodeException();

    public ExpectedActionInvokeInfo Parse(LambdaExpression actionCallExpression)
    {
        GuardAgainst.Null(actionCallExpression);

        var methodCallExpression = GetInstanceMethodCallExpression(actionCallExpression);
        var methodInfo = methodCallExpression.Method;

        var argumentAsserts = methodCallExpression.Arguments
            .Select((argument, ix) =>
            {
                var parameterName = methodInfo.GetParameters()[ix].Name;
                if (parameterName == null)
                {
                    throw new InvalidOperationException("No paramter name");
                }

                if (argument is MethodCallExpression argumentMethodCallExpression && IsArgsAnyMethod(argumentMethodCallExpression))
                {
                    return (parameterName, ArgumentAssertKind.Any, null);
                }

                var value = Expression.Lambda(argument).Compile().DynamicInvoke();
                return (parameterName, ArgumentAssertKind.Value, value);
            })
            .ToDictionary(k => k.parameterName, v => new ExpectedArgumentAssert(v.Value, v.value));

        var result = new ExpectedActionInvokeInfo(
            methodInfo,
            argumentAsserts);
        return result;
    }

    private static MethodCallExpression GetInstanceMethodCallExpression(LambdaExpression actionCallExpression)
    {
        if (actionCallExpression.Body is not MethodCallExpression methodCallExpression)
        {
            throw new ArgumentException("Not a method call expression", nameof(actionCallExpression));
        }

        var objectInstance = methodCallExpression.Object;
        if (objectInstance == null)
        {
            throw new ArgumentException("Not an instance method call expression", nameof(actionCallExpression));
        }

        return methodCallExpression;
    }

    private bool IsArgsAnyMethod(MethodCallExpression argumentExpression)
    {
        var anyMethod = _anyMethod.MakeGenericMethod(argumentExpression.Method.ReturnType);
        return argumentExpression.Method == anyMethod;
    }
}
