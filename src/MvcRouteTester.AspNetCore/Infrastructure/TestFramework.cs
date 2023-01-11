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
                // TODO: Is Single correct here?
                _detected = _frameworks.SingleOrDefault(x => x.IsAvailable);
                _detected ??= new UnknownTestFramework();
            }

            return _detected;
        }
    }

    public static void Equal<T>(T expected, T actual)
    {
        Detected.Equal(expected, actual);
    }
}
