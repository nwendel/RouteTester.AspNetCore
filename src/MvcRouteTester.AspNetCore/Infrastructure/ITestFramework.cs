namespace MvcRouteTester.AspNetCore.Infrastructure;

internal interface ITestFramework
{
    bool IsAvailable { get; }

    void Equal<T>(T? expected, T? actual);
}
