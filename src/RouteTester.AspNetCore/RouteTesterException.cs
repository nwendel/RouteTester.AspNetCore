namespace RouteTester.AspNetCore;

public class RouteTesterException : Exception
{
    public RouteTesterException(string message)
        : base(message)
    {
    }
}
