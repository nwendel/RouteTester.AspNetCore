using System.Reflection;
using System.Runtime.ExceptionServices;

namespace MvcRouteTester.AspNetCore.Infrastructure;

internal class XunitTestFramework : ITestFramework
{
    public bool IsAvailable => AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName().Name == "xunit.assert");

    public void Equal<T>(T? expected, T? actual)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == "xunit.assert");
        if (assembly == null)
        {
            throw new InvalidOperationException("TestFramework assembly not found");
        }

        var t = assembly.GetType("Xunit.Assert");
        if (t == null)
        {
            throw new InvalidOperationException("TestFramework Assert type not found");
        }

        var m = t.GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Single(x => x.Name == "Equal" &&
                        x.GetParameters().Length == 2 &&
                        x.ContainsGenericParameters &&
                        x.GetGenericArguments().Length == 1 &&
                        x.GetParameters().All(p => p.ParameterType == x.GetGenericArguments().Single()));
        var gm = m.MakeGenericMethod(typeof(T));
        try
        {
            gm.Invoke(null, new object?[] { expected, actual });
        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException == null)
            {
                throw;
            }

            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
        }

        /*
                if (!EqualityComparer<T>.Default.Equals(expected, actual))
                {
                    // TODO: Possibly replace this with a call to "Assert.Equal<T>(T expected, T actual)"
                    //       But requires tricky reflection so leave it like this for now
                    var type = assembly.GetType("Xunit.Sdk.EqualException");
                    if (type == null)
                    {
                        throw new InvalidOperationException("TestFramework exception type not found");
                    }

                    var ex = (Exception)Activator.CreateInstance(type, args: new[] { (object?)expected, (object?)actual })!;
                    throw ex;
                }*/
    }
}
