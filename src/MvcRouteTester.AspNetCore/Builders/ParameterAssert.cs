namespace MvcRouteTester.AspNetCore.Builders;

public class ParameterAssert
{
    public ParameterAssert(string name, Action<object?> action)
    {
        GuardAgainst.NullOrWhiteSpace(name);

        Name = name;
        Action = action;
    }

    public string Name { get; }

    public Action<object?> Action { get; }
}
