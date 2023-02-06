namespace RouteTester.AspNetCore.Infrastructure.TestFrameworks;

public sealed class AssertException : Exception
{
    public AssertException(string message)
        : base(message)
    {
    }
}
