namespace MvcRouteTester.AspNetCore.Infrastructure.TestFrameworks;

internal class NoTestFramework : ITestFramework
{
    public bool IsAvailable => true;

    public void Equal<T>(T? expected, T? actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new AssertException($"TestFramework.Equals failed. Expected {expected}, was {actual}");
        }
    }

    [SuppressMessage("Design", "CA1064:Exceptions should be public", Justification = "Should never be caught")]
    [SuppressMessage("Critical Code Smell", "S3871:Exception types should be \"public\"", Justification = "Should never be caught")]
    internal sealed class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}
