using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MvcRouteTester.AspNetCore.Infrastructure;

namespace MvcRouteTester.AspNetCore.Internal
{
    public class RouteExpressionParser
    {
        private readonly MethodInfo _anyMethod = typeof(Args).GetMethod(nameof(Args.Any))!;

        public ExpectedActionInvokeInfo Parse(LambdaExpression actionCallExpression)
        {
            GuardAgainst.Null(actionCallExpression);

            var methodCallExpression = GetInstanceMethodCallExpression(actionCallExpression);
            var methodInfo = methodCallExpression.Method;

            var arguments = methodCallExpression.Arguments
                .Select(x => Expression.Lambda(x).Compile().DynamicInvoke())
                .ToArray();
            var argumentAssertKinds = methodCallExpression.Arguments
                .Select(x =>
                {
                    if (x is not MethodCallExpression call)
                    {
                        return ArgumentAssertKind.Value;
                    }

                    var anyMethod = _anyMethod.MakeGenericMethod(call.Method.ReturnType);
                    return call.Method == anyMethod
                        ? ArgumentAssertKind.Any
                        : ArgumentAssertKind.Value;
                })
                .ToArray();

            var result = new ExpectedActionInvokeInfo(
                methodInfo,
                methodInfo.GetParameters().Select(x => x.Name!).Zip(arguments).ToDictionary(k => k.First, v => v.Second),
                methodInfo.GetParameters().Select(x => x.Name!).Zip(argumentAssertKinds).ToDictionary(k => k.First, v => v.Second));
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
    }
}
