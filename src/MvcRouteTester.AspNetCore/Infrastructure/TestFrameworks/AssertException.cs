namespace MvcRouteTester.AspNetCore.Infrastructure.TestFrameworks;

public sealed class AssertException : Exception
{
    public AssertException(string message)
        : base(message)
    {
    }
}
