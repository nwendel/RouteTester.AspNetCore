namespace MvcRouteTester.AspNetCore.Infrastructure;

internal static class TestFramework
{
    private static readonly ITestFramework[] _frameworks = new[]
    {
        new XunitTestFramework(),
    };

    private static ITestFramework? _detected;

    public static ITestFramework Detected
    {
        get
        {
            if (_detected == null)
            {
                _detected = _frameworks.SingleOrDefault(x => x.IsAvailable);

                if (_detected == null)
                {
                    throw new InvalidOperationException("No TestFramework detected");
                }
            }

            return _detected;
        }
    }

    public static void Equal<T>(T expected, T actual)
    {
        Detected.Equal(expected, actual);
    }
}
