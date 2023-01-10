namespace MvcRouteTester.AspNetCore.Internal;

public class ExpectedArgumentAssert
{
    public ExpectedArgumentAssert(
        ArgumentAssertKind kind,
        object? value)
    {
        GuardAgainst.Undefined(kind);

        Kind = kind;
        Value = value;
    }

    public ArgumentAssertKind Kind { get; }

    public object? Value { get; }
}
