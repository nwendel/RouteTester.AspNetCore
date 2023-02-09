namespace RouteTester.AspNetCore.Builders;

public interface IMapsToControllerActionBuilder : IFluentInterface
{
    IMapsToControllerActionBuilder ForParameter<T>(string name, Action<T?> action);
}
