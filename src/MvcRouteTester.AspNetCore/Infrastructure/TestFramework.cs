using MvcRouteTester.AspNetCore.Infrastructure.TestFrameworks;

namespace MvcRouteTester.AspNetCore.Infrastructure;

internal static class TestFramework
{
    private static readonly ITestFramework[] _frameworks = new[]
    {
        new XunitTestFramework(),
    };

    private static ITestFramework? _detected;

    private static ITestFramework Detected
    {
        get
        {
            if (_detected == null)
            {
                _detected = _frameworks.SingleOrDefault(x => x.IsAvailable);
                _detected ??= new NoTestFramework();
            }

            return _detected;
        }
    }

    public static void Equal<T>(T expected, T actual)
    {
        Detected.Equal(expected, actual);
    }
}
