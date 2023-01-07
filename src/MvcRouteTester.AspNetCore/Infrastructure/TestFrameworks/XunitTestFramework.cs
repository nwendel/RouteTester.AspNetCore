using System.Reflection;
using System.Runtime.ExceptionServices;

namespace MvcRouteTester.AspNetCore.Infrastructure.TestFrameworks;

internal class XunitTestFramework : ITestFramework
{
    private readonly Assembly? _xunitAssembly;
    private readonly Type? _assertType;
    private readonly MethodInfo? _equalMethodInfo;

    public XunitTestFramework()
    {
        _xunitAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.GetName().Name == "xunit.assert")
            .OrderByDescending(x => x.GetName().Version)
            .FirstOrDefault();

        _assertType = _xunitAssembly?.GetType("Xunit.Assert");

        _equalMethodInfo = _assertType?.GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Single(x => x.Name == "Equal" &&
                x.GetParameters().Length == 2 &&
                x.ContainsGenericParameters &&
                x.GetGenericArguments().Length == 1 &&
                x.GetParameters().All(p => p.ParameterType == x.GetGenericArguments()[0]));
    }

    public bool IsAvailable => _xunitAssembly != null;

    public void Equal<T>(T? expected, T? actual)
    {
        if (_xunitAssembly == null)
        {
            throw new InvalidOperationException("TestFramework assembly not found");
        }

        if (_assertType == null)
        {
            throw new InvalidOperationException("TestFramework Assert type not found");
        }

        if (_equalMethodInfo == null)
        {
            throw new InvalidOperationException("TestFramework Equal method not found");
        }

        try
        {
            var equalMethodInfo = _equalMethodInfo.MakeGenericMethod(typeof(T));
            equalMethodInfo.Invoke(null, new object?[] { expected, actual });
        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException == null)
            {
                throw;
            }

            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
        }
    }
}
