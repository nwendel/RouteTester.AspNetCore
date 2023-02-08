namespace RouteTester.AspNetCore.Builders;

public interface IMapsToControllerActionBuilder
{
    IMapsToControllerActionBuilder ForParameter<T>(string name, Action<T?> action);
}
