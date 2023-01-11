namespace MvcRouteTester.AspNetCore.Infrastructure.TestFrameworks;

internal class UnknownTestFramework : ITestFramework
{
    public bool IsAvailable => true;

    public void Equal<T>(T? expected, T? actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new AssertException($"Assert.Equal failed. Expected {expected}, was {actual}");
        }
    }
}
